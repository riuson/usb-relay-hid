using System.Collections.Generic;
using System.Linq;
using UsbRelayNet.HidLib;

namespace UsbRelayNet.RelayLib {
    /// <summary>
    /// The class searches and collects information about connected USB relay modules.
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
        /// <returns>A collection of information about devices found..</returns>
        public IEnumerable<RelayInfo> CollectDevices() {
            var usbHid = new HidLib.Enumerator();
            var result = usbHid.CollectDevices()
                .Where(x => x.VendorID == 0x16C0 && x.ProductId == 0x05DF)
                .Select(this.GetInfo)
                .Where(x => x != null)
                .ToArray();
            return result;
        }

        private RelayInfo GetInfo(HidDeviceInfo hidInfo) {
            var relay = new Relay(hidInfo);
            RelayInfo relayInfo = null;

            if (relay.Open()) {
                relayInfo = new RelayInfo(relay.ReadId(), relay.ChannelsCount, hidInfo);
                relay.Close();
            }

            return relayInfo;
        }
    }
}
