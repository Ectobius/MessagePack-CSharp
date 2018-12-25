using System;

namespace MessagePack.Formatters
{
    public interface IMetaInfoFormatter
    {
        int Serialize<T>(ref byte[]           bytes, int offset, T value, IFormatterResolver formatterResolver,
                         SerializationContext context, bool forceWriteHeader = false);

        T Deserialize<T>(byte[]                 bytes, int offset, IFormatterResolver formatterResolver,
                         out int                readSize,
                         DeserializationContext context, bool forceReadHeader = false);

        void Populate<T>(ref T                  value, byte[] bytes, int offset, IFormatterResolver formatterResolver,
                         out int                readSize,
                         DeserializationContext context, bool forceReadHeader = false);

        bool NeedsMetaHeader(Type type);
    }
}
