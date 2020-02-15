using UsbRelayNet.RelayLib;

namespace DemoUI {
    public class RelayItem {
        private readonly RelayInfo _relayInfo;

        public RelayItem(RelayInfo relayInfo) {
            this._relayInfo = relayInfo;
        }

        public RelayInfo RelayInfo => this._relayInfo;


        public override string ToString() => $"#{this._relayInfo.Id}  @ '{this._relayInfo.HidInfo.Path}'";
    }
}
