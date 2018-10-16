using MessagePack.Formatters;
using MessagePack.Internal;
using MessagePack.Resolvers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BuiltinResolverGetFormatterHelper = MessagePack.Internal.BuiltinResolverGetFormatterHelper;

namespace MessagePack.Resolvers
{
    public sealed class BuiltinResolver : IFormatterResolver, IUntypedFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new BuiltinResolver();

        BuiltinResolver()
        {

        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        public static bool TypeHasBuiltinFormatter(Type type)
        {
            return BuiltinResolverGetFormatterHelper.HasFormatterForType(type);
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                // Reduce IL2CPP code generate size(don't write long code in <T>)
                formatter = (IMessagePackFormatter<T>)BuiltinResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }

        public IMessagePackUntypedFormatter GetFormatter(Type type)
        {
            return (IMessagePackUntypedFormatter)BuiltinResolverGetFormatterHelper.GetFormatter(type);
        }
    }
}

namespace MessagePack.Internal
{
    internal static class BuiltinResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
        {
            // Primitive
            {typeof(Int16), UntypedFormatterWrapper.Create(Int16Formatter.Instance)},
            {typeof(Int32), UntypedFormatterWrapper.Create(Int32Formatter.Instance)},
            {typeof(Int64), UntypedFormatterWrapper.Create(Int64Formatter.Instance)},
            {typeof(UInt16), UntypedFormatterWrapper.Create(UInt16Formatter.Instance)},
            {typeof(UInt32), UntypedFormatterWrapper.Create(UInt32Formatter.Instance)},
            {typeof(UInt64), UntypedFormatterWrapper.Create(UInt64Formatter.Instance)},
            {typeof(Single), UntypedFormatterWrapper.Create(SingleFormatter.Instance)},
            {typeof(Double), UntypedFormatterWrapper.Create(DoubleFormatter.Instance)},
            {typeof(bool), UntypedFormatterWrapper.Create(BooleanFormatter.Instance)},
            {typeof(byte), UntypedFormatterWrapper.Create(ByteFormatter.Instance)},
            {typeof(sbyte), UntypedFormatterWrapper.Create(SByteFormatter.Instance)},
            {typeof(DateTime), UntypedFormatterWrapper.Create(DateTimeFormatter.Instance)},
            {typeof(char), UntypedFormatterWrapper.Create(CharFormatter.Instance)},
            
            // Nulllable Primitive
            {typeof(Nullable<Int16>), UntypedFormatterWrapper.Create(NullableInt16Formatter.Instance)},
            {typeof(Nullable<Int32>), UntypedFormatterWrapper.Create(NullableInt32Formatter.Instance)},
            {typeof(Nullable<Int64>), UntypedFormatterWrapper.Create(NullableInt64Formatter.Instance)},
            {typeof(Nullable<UInt16>), UntypedFormatterWrapper.Create(NullableUInt16Formatter.Instance)},
            {typeof(Nullable<UInt32>), UntypedFormatterWrapper.Create(NullableUInt32Formatter.Instance)},
            {typeof(Nullable<UInt64>), UntypedFormatterWrapper.Create(NullableUInt64Formatter.Instance)},
            {typeof(Nullable<Single>), UntypedFormatterWrapper.Create(NullableSingleFormatter.Instance)},
            {typeof(Nullable<Double>), UntypedFormatterWrapper.Create(NullableDoubleFormatter.Instance)},
            {typeof(Nullable<bool>), UntypedFormatterWrapper.Create(NullableBooleanFormatter.Instance)},
            {typeof(Nullable<byte>), UntypedFormatterWrapper.Create(NullableByteFormatter.Instance)},
            {typeof(Nullable<sbyte>), UntypedFormatterWrapper.Create(NullableSByteFormatter.Instance)},
            {typeof(Nullable<DateTime>), UntypedFormatterWrapper.Create(NullableDateTimeFormatter.Instance)},
            {typeof(Nullable<char>), UntypedFormatterWrapper.Create(NullableCharFormatter.Instance)},
            
            // StandardClassLibraryFormatter
            {typeof(string), UntypedFormatterWrapper.Create(NullableStringFormatter.Instance)},
            {typeof(decimal), UntypedFormatterWrapper.Create(DecimalFormatter.Instance)},
            {typeof(decimal?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<decimal>(DecimalFormatter.Instance))},
            {typeof(TimeSpan), UntypedFormatterWrapper.Create(TimeSpanFormatter.Instance)},
            {typeof(TimeSpan?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<TimeSpan>(TimeSpanFormatter.Instance))},
            {typeof(DateTimeOffset), UntypedFormatterWrapper.Create(DateTimeOffsetFormatter.Instance)},
            {typeof(DateTimeOffset?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<DateTimeOffset>(DateTimeOffsetFormatter.Instance))},
            {typeof(Guid), UntypedFormatterWrapper.Create(GuidFormatter.Instance)},
            {typeof(Guid?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<Guid>(GuidFormatter.Instance))},
            {typeof(Uri), UntypedFormatterWrapper.Create(UriFormatter.Instance)},
            {typeof(Version), UntypedFormatterWrapper.Create(VersionFormatter.Instance)},
            {typeof(StringBuilder), UntypedFormatterWrapper.Create(StringBuilderFormatter.Instance)},
            {typeof(BitArray), UntypedFormatterWrapper.Create(BitArrayFormatter.Instance)},
            
            // special primitive
            {typeof(byte[]), UntypedFormatterWrapper.Create(ByteArrayFormatter.Instance)},
            
            // Nil
            {typeof(Nil), UntypedFormatterWrapper.Create(NilFormatter.Instance)},
            {typeof(Nil?), UntypedFormatterWrapper.Create(NullableNilFormatter.Instance)},
            
            // otpmitized primitive array formatter
            {typeof(Int16[]), UntypedFormatterWrapper.Create(Int16ArrayFormatter.Instance)},
            {typeof(Int32[]), UntypedFormatterWrapper.Create(Int32ArrayFormatter.Instance)},
            {typeof(Int64[]), UntypedFormatterWrapper.Create(Int64ArrayFormatter.Instance)},
            {typeof(UInt16[]), UntypedFormatterWrapper.Create(UInt16ArrayFormatter.Instance)},
            {typeof(UInt32[]), UntypedFormatterWrapper.Create(UInt32ArrayFormatter.Instance)},
            {typeof(UInt64[]), UntypedFormatterWrapper.Create(UInt64ArrayFormatter.Instance)},
            {typeof(Single[]), UntypedFormatterWrapper.Create(SingleArrayFormatter.Instance)},
            {typeof(Double[]), UntypedFormatterWrapper.Create(DoubleArrayFormatter.Instance)},
            {typeof(Boolean[]), UntypedFormatterWrapper.Create(BooleanArrayFormatter.Instance)},
            {typeof(SByte[]), UntypedFormatterWrapper.Create(SByteArrayFormatter.Instance)},
            {typeof(DateTime[]), UntypedFormatterWrapper.Create(DateTimeArrayFormatter.Instance)},
            {typeof(Char[]), UntypedFormatterWrapper.Create(CharArrayFormatter.Instance)},
            {typeof(string[]), UntypedFormatterWrapper.Create(NullableStringArrayFormatter.Instance)},

            // well known collections
            {typeof(List<Int16>), UntypedFormatterWrapper.Create(new ListFormatter<Int16>())},
            {typeof(List<Int32>), UntypedFormatterWrapper.Create(new ListFormatter<Int32>())},
            {typeof(List<Int64>), UntypedFormatterWrapper.Create(new ListFormatter<Int64>())},
            {typeof(List<UInt16>), UntypedFormatterWrapper.Create(new ListFormatter<UInt16>())},
            {typeof(List<UInt32>), UntypedFormatterWrapper.Create(new ListFormatter<UInt32>())},
            {typeof(List<UInt64>), UntypedFormatterWrapper.Create(new ListFormatter<UInt64>())},
            {typeof(List<Single>), UntypedFormatterWrapper.Create(new ListFormatter<Single>())},
            {typeof(List<Double>), UntypedFormatterWrapper.Create(new ListFormatter<Double>())},
            {typeof(List<Boolean>), UntypedFormatterWrapper.Create(new ListFormatter<Boolean>())},
            {typeof(List<byte>), UntypedFormatterWrapper.Create(new ListFormatter<byte>())},
            {typeof(List<SByte>), UntypedFormatterWrapper.Create(new ListFormatter<SByte>())},
            {typeof(List<DateTime>), UntypedFormatterWrapper.Create(new ListFormatter<DateTime>())},
            {typeof(List<Char>), UntypedFormatterWrapper.Create(new ListFormatter<Char>())},
            {typeof(List<string>), UntypedFormatterWrapper.Create(new ListFormatter<string>())},

            { typeof(ArraySegment<byte>), UntypedFormatterWrapper.Create(ByteArraySegmentFormatter.Instance) },
            { typeof(ArraySegment<byte>?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<ArraySegment<byte>>(ByteArraySegmentFormatter.Instance)) },

#if NETSTANDARD
            {typeof(System.Numerics.BigInteger), UntypedFormatterWrapper.Create(BigIntegerFormatter.Instance)},
            {typeof(System.Numerics.BigInteger?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<System.Numerics.BigInteger>(BigIntegerFormatter.Instance))},
            {typeof(System.Numerics.Complex), UntypedFormatterWrapper.Create(ComplexFormatter.Instance)},
            {typeof(System.Numerics.Complex?), UntypedFormatterWrapper.Create(new StaticNullableFormatter<System.Numerics.Complex>(ComplexFormatter.Instance))},
            {typeof(System.Threading.Tasks.Task), UntypedFormatterWrapper.Create(TaskUnitFormatter.Instance)},
#endif
        };

        internal static object GetFormatter(Type t)
        {
            object formatter;
            if (formatterMap.TryGetValue(t, out formatter))
            {
                return formatter;
            }

            return null;
        }

        internal static bool HasFormatterForType(Type type)
        {
            return formatterMap.ContainsKey(type);
        }
    }
}