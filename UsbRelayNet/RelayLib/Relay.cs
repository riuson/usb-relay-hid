using System;
using System.Linq;
using System.Text;
using UsbRelayNet.HidLib;

namespace UsbRelayNet.RelayLib {
    public class Relay : IDisposable {
        private readonly HidDeviceInfo _deviceInfo;
        private readonly HidDevice _device;

        public Relay(HidDeviceInfo deviceInfo) {
            this._deviceInfo = deviceInfo;
            this._device = new HidDevice();
        }

        public void Dispose() {
            this._device.Dispose();
        }

        public HidDeviceInfo Info => this._deviceInfo;

        public bool Open() => this._device.Open(this._deviceInfo.Path);

        public void Close() => this._device.Close();

        public bool IsOpened => this._device.IsOpened;

        public int ChannelsCount {
            get {
                var lastChar = this._deviceInfo.Product.Last();

                if (char.IsDigit(lastChar) && lastChar >= '1' && lastChar <= '8') {
                    var count = Convert.ToInt32(lastChar) - Convert.ToInt32('0');
                    return count;
                }

                throw new Exception("Last character of product name doesn't recognized as channel's count.");
            }
        }

        /// <summary>
        /// // Read state of all relays
        /// </summary>
        /// <param name="data">State of all relays</param>
        /// <returns>bit mask of all relays (R1->bit 0, R2->bit 1 ...) or -1 on error</returns>
        public void ReadStatusRaw(out byte[] rawData, out int status) {
            var reportNumber = 0;
            var length = 8 + 1; /* report id 1 byte + 8 bytes data */

            if (!this._device.GetFeature(reportNumber, out var buffer)) {
                throw new Exception("Error reading status");
            }

            if (buffer[0] != reportNumber) {
                throw new Exception("Wrong HID report returned!");
            }

            rawData = new byte[9];
            Array.Copy(buffer, 0, rawData, 0, length);

            status = buffer[8];
        }

        public int ReadStatus() {
            this.ReadStatusRaw(out var rawData, out var status);
            return status;
        }

        public string ReadId() {
            this.ReadStatusRaw(out var rawData, out _);

            if (Enumerable.Range(1, 5).Select(i => rawData[i]).All(x => (x >= 0x20) && (x <= 0x7f))) {
                if (rawData[6] == 0) {
                    var id = Encoding.ASCII.GetString(rawData, 1, 5);
                    return id;
                }
            }

            return string.Empty;
        }

        public void WriteId(string id) {
            if (string.IsNullOrEmpty(id)) {
                id = DateTime.Now.ToString("Hmmss");
            } else if (id.Length < 5) {
                id = id.PadRight(5, ' ');
            }

            var idBytes = Encoding.ASCII.GetBytes(id);

            var buffer = new byte[9];
            buffer[1] = 0xfa;
            Array.Copy(idBytes, 0, buffer, 2, 5);

            this._device.SetFeature(0, buffer);
        }

        public bool ReadChannel(int channel) {
            if (channel < 1 || channel > 8) {
                throw new ArgumentOutOfRangeException(nameof(channel), channel, "Channel index should be in the range 1…8!");
            }

            var status = this.ReadStatus();
            return (status & (1 << (channel - 1))) != 0;
        }

        public bool WriteChannel(int channel, bool state) {
            if (channel < 1 || channel > 8) {
                throw new ArgumentOutOfRangeException(nameof(channel), channel, "Channel index should be in the range 1…8!");
            }

            var buffer = new byte[9];
            buffer[0] = 0;
            buffer[1] = state ? (byte)0xff : (byte)0xfd;
            buffer[2] = Convert.ToByte(channel);

            if (this._device.SetFeature(0, buffer)) {
                var newState = this.ReadChannel(channel);

                if (newState == state) {
                    return true;
                }
            }

            return false;
        }

        public bool WriteChannels(bool state) {
            var buffer = new byte[9];
            buffer[0] = 0;
            buffer[1] = state ? (byte)0xfe : (byte)0xfc;
            buffer[2] = 0;

            if (this._device.SetFeature(0, buffer)) {
                var status = this.ReadStatus();

                var mask = 0;

                for (var i = this.ChannelsCount - 1; i >= 0; i--) {
                    mask |= 1 << i;
                }

                if (state) {
                    return (status & mask) == mask;
                } else {
                    return (status & mask) == 0;
                }
            }

            return false;
        }
    }
}
