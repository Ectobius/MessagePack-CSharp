namespace MessagePack.Formatters
{
    public interface IMessagePackFormatterWithPopulate<T> : IMessagePackFormatter<T>
    {
        void Populate(ref T value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context);
    }

    public interface IMessagePackUntypedFormatterWithPopulate : IMessagePackUntypedFormatter
    {
        void Populate(ref object value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context);
    }
}
