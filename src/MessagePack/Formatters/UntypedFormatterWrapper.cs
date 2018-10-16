namespace MessagePack.Formatters
{
    public class UntypedFormatterWrapper<T> : IMessagePackFormatter<T>, IMessagePackUntypedFormatter
    {
        private readonly IMessagePackFormatter<T> _actualFormatter;

        public UntypedFormatterWrapper(IMessagePackFormatter<T> actualFormatter)
        {
            _actualFormatter = actualFormatter;
        }

        public int Serialize(ref byte[]           bytes, int offset, T value, IFormatterResolver formatterResolver,
                             SerializationContext context)
        {
            return _actualFormatter.Serialize(ref bytes, offset, value, formatterResolver, context);
        }

        T IMessagePackFormatter<T>.Deserialize(byte[]                 bytes, int offset, IFormatterResolver formatterResolver, out int readSize,
                                            DeserializationContext context)
        {
            return _actualFormatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public int Serialize(ref byte[]           bytes, int offset, object value, IFormatterResolver formatterResolver,
                             SerializationContext context)
        {
            return _actualFormatter.Serialize(ref bytes, offset, (T)value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[]                 bytes, int offset, IFormatterResolver formatterResolver, out int readSize,
                                                        DeserializationContext context)
        {
            return _actualFormatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }

    public static class UntypedFormatterWrapper
    {
        public static UntypedFormatterWrapper<T> Create<T>(IMessagePackFormatter<T> actualFormatter)
        {
            return new UntypedFormatterWrapper<T>(actualFormatter);
        }
    }
}
