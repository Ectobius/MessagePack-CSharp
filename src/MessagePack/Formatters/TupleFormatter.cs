#if NETSTANDARD

using System;

namespace MessagePack.Formatters
{

    public sealed class TupleFormatter<T1> : IMessagePackFormatter<Tuple<T1>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 1);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 1) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1>(item1);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2> : IMessagePackFormatter<Tuple<T1, T2>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 2);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 2) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2>(item1, item2);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2, T3> : IMessagePackFormatter<Tuple<T1, T2, T3>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2, T3> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 3);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item3, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2, T3> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 3) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item3 = context.MetaInfoFormatter.Deserialize<T3>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2, T3>(item1, item2, item3);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2, T3>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2, T3, T4> : IMessagePackFormatter<Tuple<T1, T2, T3, T4>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2, T3, T4> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 4);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item3, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item4, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2, T3, T4> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 4) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item3 = context.MetaInfoFormatter.Deserialize<T3>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item4 = context.MetaInfoFormatter.Deserialize<T4>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2, T3, T4>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2, T3, T4, T5> : IMessagePackFormatter<Tuple<T1, T2, T3, T4, T5>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2, T3, T4, T5> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 5);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item3, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item4, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item5, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2, T3, T4, T5> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 5) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item3 = context.MetaInfoFormatter.Deserialize<T3>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item4 = context.MetaInfoFormatter.Deserialize<T4>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item5 = context.MetaInfoFormatter.Deserialize<T5>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2, T3, T4, T5>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2, T3, T4, T5, T6> : IMessagePackFormatter<Tuple<T1, T2, T3, T4, T5, T6>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2, T3, T4, T5, T6> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 6);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item3, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item4, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item5, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item6, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2, T3, T4, T5, T6> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 6) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item3 = context.MetaInfoFormatter.Deserialize<T3>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item4 = context.MetaInfoFormatter.Deserialize<T4>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item5 = context.MetaInfoFormatter.Deserialize<T5>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item6 = context.MetaInfoFormatter.Deserialize<T6>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2, T3, T4, T5, T6>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2, T3, T4, T5, T6, T7> : IMessagePackFormatter<Tuple<T1, T2, T3, T4, T5, T6, T7>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2, T3, T4, T5, T6, T7> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 7);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item3, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item4, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item5, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item6, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item7, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2, T3, T4, T5, T6, T7> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 7) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item3 = context.MetaInfoFormatter.Deserialize<T3>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item4 = context.MetaInfoFormatter.Deserialize<T4>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item5 = context.MetaInfoFormatter.Deserialize<T5>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item6 = context.MetaInfoFormatter.Deserialize<T6>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item7 = context.MetaInfoFormatter.Deserialize<T7>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2, T3, T4, T5, T6, T7>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class TupleFormatter<T1, T2, T3, T4, T5, T6, T7, TRest> : IMessagePackFormatter<Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IMessagePackUntypedFormatter
    {
        public int Serialize(ref byte[] bytes, int offset, Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 8);

                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item1, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item2, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item3, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item4, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item5, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item6, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Item7, formatterResolver, context);
                offset += context.MetaInfoFormatter.Serialize(ref bytes, offset, value.Rest, formatterResolver, context);

                return offset - startOffset;
            }
        }

        public Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            else
            {
                var startOffset = offset;
                var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (count != 8) throw new InvalidOperationException("Invalid Tuple count");
                offset += readSize;

                var item1 = context.MetaInfoFormatter.Deserialize<T1>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item2 = context.MetaInfoFormatter.Deserialize<T2>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item3 = context.MetaInfoFormatter.Deserialize<T3>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item4 = context.MetaInfoFormatter.Deserialize<T4>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item5 = context.MetaInfoFormatter.Deserialize<T5>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item6 = context.MetaInfoFormatter.Deserialize<T6>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item7 = context.MetaInfoFormatter.Deserialize<T7>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
                var item8 = context.MetaInfoFormatter.Deserialize<TRest>(bytes, offset, formatterResolver, out readSize, context);
                offset += readSize;
            
                readSize = offset - startOffset;
                return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, item8);
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>) value, formatterResolver, context);
        }

        object IMessagePackUntypedFormatter.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }

}

#endif