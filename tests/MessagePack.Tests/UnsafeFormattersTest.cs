using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MessagePack.Tests
{
    public class UnsafeFormattersTest
    {
        [Fact]
        public void GuidTest()
        {
            var guid = Guid.NewGuid();
            byte[] bin = null;
            var serializationContext = new MessagePack.Formatters.SerializationContext();
            BinaryGuidFormatter.Instance.Serialize(ref bin, 0, guid, null, serializationContext).Is(18);

            int readSize;
            var deserializationContext = new MessagePack.Formatters.DeserializationContext();
            var nguid = BinaryGuidFormatter.Instance.Deserialize(bin, 0, null, out readSize, deserializationContext);
            readSize.Is(18);

            guid.Is(nguid);
        }

        [Fact]
        public void DecimalTest()
        {
            var d = new Decimal(1341, 53156, 61, true, 3);
            byte[] bin = null;
            var serializationContext = new MessagePack.Formatters.SerializationContext();
            BinaryDecimalFormatter.Instance.Serialize(ref bin, 0, d, null, serializationContext).Is(18);

            int readSize;
            var deserializationContext = new MessagePack.Formatters.DeserializationContext();
            var nd = BinaryDecimalFormatter.Instance.Deserialize(bin, 0, null, out readSize, deserializationContext);
            readSize.Is(18);

            d.Is(nd);
        }
    }
}
