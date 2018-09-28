#if NETSTANDARD
using MessagePack.Formatters;

namespace MessagePack.Resolvers
{
    /// <summary>
    /// Embed c# type names for `object` typed fields/collection items
    /// Preserve c# DateTime timezone
    /// </summary>
    public sealed class TypelessContractlessStandardResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new TypelessContractlessStandardResolver();

        static readonly IFormatterResolver[] resolvers = new[]
        {
            NativeDateTimeResolver.Instance, // Native c# DateTime format, preserving timezone
            BuiltinResolver.Instance, // Try Builtin
            AttributeFormatterResolver.Instance, // Try use [MessagePackFormatter]

            TypelessObjectResolver.Instance
        };

        TypelessContractlessStandardResolver()
        {
        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }
}
#endif