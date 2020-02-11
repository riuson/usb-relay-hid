using NUnit.Framework;
using System.Linq;
using UsbRelayNet.HidLib;

namespace UsbRelayNetTests {
    public class UsbHidTests {
        [Test]
        public void CanCollectDevices() {
            var sut = new Enumerator();

            var devices = sut.CollectDevices();

            Assert.That(devices.Count(), Is.GreaterThan(0));
        }
    }
}
