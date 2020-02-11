using UsbRelayNet.RelayLib;

namespace DemoUI {
    public class RelayItem {
        private readonly Relay _relay;

        public RelayItem(Relay relay) {
            this._relay = relay;
        }

        public Relay Relay => this._relay;


        public override string ToString() => this._relay.Info.Path;
    }
}
