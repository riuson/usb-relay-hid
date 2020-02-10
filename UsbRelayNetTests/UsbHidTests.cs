using NUnit.Framework;
using UsbRelayNet;

namespace UsbRelayNetTests
{
    public class UsbHidTests
    {
        [Test]
        public void EnumDevices()
        {
            var sut = new UsbHid();
            sut.EnumDevices(0x16C0, 0x05DF);
        }
    }
}
