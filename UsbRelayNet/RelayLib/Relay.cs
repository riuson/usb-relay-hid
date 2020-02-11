using System;
using System.Collections;
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

        public bool Open() => this._device.Open(this._deviceInfo.Path);

        public void Close() => this._device.Close();

        public bool IsOpened => this._device.IsOpened;

        public int ChannelsCount {
            get {
                var lastChar = this._deviceInfo.Product.Last();

                if (char.IsDigit(lastChar) && lastChar >= '1' && lastChar <= '8') {
                    int count = Convert.ToInt32(lastChar) - Convert.ToInt32('0');
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
        public void ReadStatusRaw(out byte[] rawData, out Channels states) {
            int reportNumber = 0;
            int length = 8 + 1; /* report id 1 byte + 8 bytes data */

            if (!this._device.GetFeature(reportNumber, out var buffer)) {
                throw new HidException("Error reading status");
            }

            if (buffer[0] != reportNumber) {
                throw new HidException("Wrong HID report returned!");
            }

            rawData = new byte[9];
            Array.Copy(buffer, 0, rawData, 0, length);

            states = (Channels)buffer[8];
        }

        public Channels ReadStatus() {
            this.ReadStatusRaw(out var rawData, out var states);
            return states;
        }

        public string ReadId() {
            this.ReadStatusRaw(out var rawData, out _);

            if (Enumerable.Range(1, 5).Select(i => rawData[i]).All(x => (x >= 0x20) && (x <= 0x7f))) {
                if (rawData[6] == 0) {
                    string id = Encoding.ASCII.GetString(rawData, 1, 5);
                    return id;
                }
            }

            return string.Empty;
        }

        public bool ReadChannel(Channels channel) {
            var states = this.ReadStatus();
            return (states & channel) != Channels.None;
        }

        public bool WriteChannel(Channels channel, bool state) {
            if (channel == Channels.None) {
                return false;
            }

            byte cmd1, cmd2;

            if (channel == Channels.All) {
                cmd2 = 0;
                cmd1 = state ? (byte) 0xfe : (byte) 0xfc;
            } else {
                int bitsCount = 0;
                int number = 0;

                for (int i = 0; i < 8; i++) {
                    if ((Convert.ToByte(channel) & (1 << i)) != 0) {
                        bitsCount++;
                        number = i + 1;
                    }
                }

                if (bitsCount > 1) {
                    throw new ArgumentException("Can't change multiple channels at once!", nameof(channel));
                }

                cmd2 = Convert.ToByte(number);
                cmd1 = state ? (byte) 0xff : (byte) 0xfd;
            }

            var buffer = new byte[9];
            buffer[0] = 0;
            buffer[1] = cmd1;
            buffer[2] = cmd2;

            if (this._device.SetFeature(0, buffer)) {
                if (channel == Channels.All) {
                    var newStates = this.ReadStatus();
                } else {
                    var newState = this.ReadChannel(channel);

                    if (newState == state) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
