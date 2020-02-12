using UsbRelayNet.HidLib;

namespace UsbRelayNet.RelayLib {
    /// <summary>
    /// Information about found relay module.
    /// </summary>
    public class RelayInfo {
        private RelayInfo() {
        }

        internal RelayInfo(string id, int channelsCount, HidDeviceInfo hidInfo) {
            this.Id = id;
            this.ChannelsCount = channelsCount;
            this.HidInfo = hidInfo;
        }

        /// <summary>
        /// Id of relay module.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Number of channels available on relay module.
        /// </summary>
        public int ChannelsCount { get; }
        /// <summary>
        /// Information about HID.
        /// </summary>
        public HidDeviceInfo HidInfo { get; }
    }
}
