using UsbRelayNet.RelayLib;

namespace KioskUI {
    public class RelayItem {
        public RelayItem(RelayInfo relayInfo) {
            this.RelayInfo = relayInfo;
        }

        public RelayInfo RelayInfo { get; }

        public string Title => $"#{this.RelayInfo.Id}  @ '{this.RelayInfo.HidInfo.Path}'";
    }
}
