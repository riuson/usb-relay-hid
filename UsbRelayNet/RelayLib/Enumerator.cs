using System.Collections.Generic;
using System.Linq;

namespace UsbRelayNet.RelayLib {
    /// <summary>
    /// The class searches and collects information about connected devices (USB Relay Modules).
    /// </summary>
    public class Enumerator {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Enumerator() {
        }

        /// <summary>
        /// Search and collect information.
        /// </summary>
        /// <returns>Enumerator of devices found.</returns>
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
