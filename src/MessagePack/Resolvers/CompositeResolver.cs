using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MessagePack.Resolvers
{
    public sealed class CompositeResolver : IFormatterResolver, IUntypedFormatterResolver
    {
        public static readonly CompositeResolver Instance = new CompositeResolver();

        static bool isFreezed = false;
        static IMessagePackFormatter[] formatters = new IMessagePackFormatter[0];
        static IFormatterResolver[] resolvers = new IFormatterResolver[0];

        static Dictionary<Type, IMessagePackFormatter> Cache = new Dictionary<Type, IMessagePackFormatter>();

        CompositeResolver()
        {
        }

        public static void Register(params IFormatterResolver[] resolvers)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.resolvers = resolvers;
        }

        public static void Register(params IMessagePackFormatter[] formatters)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.formatters = formatters;
        }

        public static void Register(IMessagePackFormatter[] formatters, IFormatterResolver[] resolvers)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.resolvers = resolvers;
            CompositeResolver.formatters = formatters;
        }

        public static void RegisterAndSetAsDefault(params IFormatterResolver[] resolvers)
        {
            Register(resolvers);
            MessagePack.MessagePackSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static void RegisterAndSetAsDefault(params IMessagePackFormatter[] formatters)
        {
            Register(formatters);
            MessagePack.MessagePackSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static void RegisterAndSetAsDefault(IMessagePackFormatter[] formatters, IFormatterResolver[] resolvers)
        {
            Register(formatters);
            Register(resolvers);
            MessagePack.MessagePackSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        public IMessagePackUntypedFormatter GetFormatter(Type type)
        {
            return GetFormatterStatic(type);
        }

        public IMessagePackUntypedFormatter GetFormatterStatic(Type type)
        {
            if (Cache.TryGetValue(type, out var formatter))
            {
                if (formatter is IMessagePackUntypedFormatter)
                {
                    return (IMessagePackUntypedFormatter) formatter;
                }
            }

            formatter = GetFormatterFromRegisteredFormatters(type);
            if (formatter == null)
            {
                formatter = GetFormatterFromResolvers(type);
            }

            if (formatter != null)
            {
                Cache[type] = formatter;
            }

            if (formatter is IMessagePackUntypedFormatter)
            {
                return (IMessagePackUntypedFormatter) formatter;
            }

            return null;
        }

        public IMessagePackFormatter GetFormatterFromRegisteredFormatters(Type type)
        {
            foreach (var item in formatters)
            {
                foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                {
                    var ti = implInterface.GetTypeInfo();
                    if (ti.IsGenericType && ti.GenericTypeArguments[0] == type)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public IMessagePackFormatter GetFormatterFromResolvers(Type type)
        {
            foreach (var resolver in resolvers)
            {
                if (resolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var formatter = untypedFormatterResolver.GetFormatter(type);
                    if (formatter != null)
                    {
                        return formatter;
                    }
                }
            }

            return null;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                isFreezed = true;

                if (Cache.TryGetValue(typeof(T), out var cachedFormatter))
                {
                    formatter = (IMessagePackFormatter<T>) cachedFormatter;
                    return;
                }

                foreach (var item in formatters)
                {
                    foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                    {
                        var ti = implInterface.GetTypeInfo();
                        if (ti.IsGenericType && ti.GenericTypeArguments[0] == typeof(T))
                        {
                            formatter = (IMessagePackFormatter<T>)item;
                            Cache[typeof(T)] = formatter;
                            return;
                        }
                    }
                }

                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        Cache[typeof(T)] = formatter;
                        return;
                    }
                }
            }
        }
    }
}
