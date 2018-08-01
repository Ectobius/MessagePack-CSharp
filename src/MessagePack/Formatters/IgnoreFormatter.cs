﻿namespace MessagePack.Formatters
{
    public sealed class IgnoreFormatter<T> : IMessagePackFormatter<T>
    {
        public int Serialize(ref byte[] bytes, int offset, T value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return MessagePackBinary.WriteNil(ref bytes, offset);
        }

        public T Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            return default(T);
        }
    }
}