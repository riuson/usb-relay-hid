using UsbRelayNet.RelayLib;

namespace KioskUI {
    public class ChannelItem {
        public ChannelItem(RelayInfo relayInfo, int channel, string title) {
            this.RelayInfo = relayInfo;
            this.Channel = channel;
            this.Title = title;
        }

        public RelayInfo RelayInfo { get; }
        public int Channel { get; }
        public string Title { get; }
    }
}
