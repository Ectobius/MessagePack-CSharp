using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.Formatters
{
    public interface IMetaInfoFormatter
    {
        int Serialize<T>(ref byte[]           bytes, int offset, T value, IFormatterResolver formatterResolver,
                         SerializationContext context);

        T Deserialize<T>(byte[]                 bytes, int offset, IFormatterResolver formatterResolver,
                         out int                readSize,
                         DeserializationContext context);

        void Populate<T>(ref T                  value, byte[] bytes, int offset, IFormatterResolver formatterResolver,
                         out int                readSize,
                         DeserializationContext context);
    }
}
