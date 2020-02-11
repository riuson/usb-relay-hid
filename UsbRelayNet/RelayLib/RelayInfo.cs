using UsbRelayNet.HidLib;

namespace UsbRelayNet.RelayLib {
    public class RelayInfo {
        private RelayInfo() {
        }

        internal RelayInfo(string id, int channelsCount, HidDeviceInfo hidInfo) {
            this.Id = id;
            this.ChannelsCount = channelsCount;
            this.HidInfo = hidInfo;
        }

        public string Id { get; set; }
        public int ChannelsCount { get; }
        public HidDeviceInfo HidInfo { get; }
    }
}
