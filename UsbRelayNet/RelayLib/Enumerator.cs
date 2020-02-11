using System.Collections.Generic;
using System.Linq;

namespace UsbRelayNet.RelayLib {
    public class Enumerator {
        public Enumerator() {
        }

        public IEnumerable<Relay> CollectDevices() {
            var usbHid = new HidLib.Enumerator();
            var result = usbHid.CollectDevices()
                .Where(x => x.VendorID == 0x16C0 && x.ProductId == 0x05DF)
                .Select(x => new Relay(x))
                .ToArray();
            return result;
        }
    }
}
