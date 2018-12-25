using System;

namespace MessagePack.Formatters
{
    public class NoMetaInfoFormatter : IMetaInfoFormatter
    {
        public int Serialize<T>(ref byte[]           bytes, int offset, T value, IFormatterResolver formatterResolver,
                                SerializationContext context, bool forceWriteHeader = false)
        {
            return formatterResolver.GetFormatterWithVerify<T>().Serialize(ref bytes, offset, value, formatterResolver, context);
        }

        public T Deserialize<T>(byte[]                 bytes, int offset, IFormatterResolver formatterResolver, out int readSize,
                                DeserializationContext context, bool forceReadHeader = false)
        {
            return formatterResolver.GetFormatterWithVerify<T>()
                                    .Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public void Populate<T>(ref T                  value, byte[] bytes, int offset, IFormatterResolver formatterResolver,
                                out int                readSize,
                                DeserializationContext context, bool forceReadHeader = false)
        {
            var formatter = formatterResolver.GetFormatterWithVerify<T>();
            if(formatter is IMessagePackFormatterWithPopulate<T> withPopulate)
            {
                withPopulate.Populate(ref value, bytes, offset, formatterResolver, out readSize, context);
            }
            else
            {
                value = formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }
        }

        public bool NeedsMetaHeader(Type type)
        {
            return false;
        }
    }
}
