namespace MessagePack.Formatters
{
    // marker
    public interface IMessagePackFormatter
    {

    }

    public interface IMessagePackFormatter<T> : IMessagePackFormatter
    {
        int Serialize(ref byte[] bytes, int offset, T value, IFormatterResolver formatterResolver, SerializationContext context);
        T Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context);
    }

    public interface IMessagePackUntypedFormatter : IMessagePackFormatter
    {
        int Serialize(ref byte[] bytes, int offset, object                  value,             IFormatterResolver formatterResolver, SerializationContext   context);
        object   Deserialize(byte[]   bytes, int offset, IFormatterResolver formatterResolver, out int            readSize,          DeserializationContext context);
    }
}
