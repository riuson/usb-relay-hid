using System;
using System.Linq;
using System.Text;
using UsbRelayNet.HidLib;

namespace UsbRelayNet.RelayLib {
    /// <summary>
    /// Public interface to control USB relay module.
    /// </summary>
    public sealed class Relay : IDisposable {
        private readonly HidDevice _device;

        internal Relay(HidDeviceInfo deviceInfo) {
            this.Info = deviceInfo;
            this._device = new HidDevice();
        }

        /// <summary>
        /// Constructor from RelayInfo.
        /// </summary>
        /// <param name="deviceInfo">Relay module info.</param>
        public Relay(RelayInfo deviceInfo) {
            this.Info = deviceInfo.HidInfo;
            this._device = new HidDevice();
        }

        /// <inheritdoc />
        public void Dispose() => this._device.Dispose();

        /// <summary>
        /// Underlying HID info
        /// </summary>
        public HidDeviceInfo Info { get; }

        /// <summary>
        /// Establishes a connection to the relay module.
        /// </summary>
        /// <returns>True, if the operation is successful.</returns>
        public bool Open() => this._device.Open(this.Info.Path);

        /// <summary>
        /// Terminates the connection with the relay module.
        /// </summary>
        public void Close() => this._device.Close();

        /// <summary>
        /// True, if connection established.
        /// </summary>
        public bool IsOpened => this._device.IsOpened;

        /// <summary>
        /// Number of channels available on relay module.
        /// </summary>
        public int ChannelsCount {
            get {
                var lastChar = this.Info.Product.Last();

                if (char.IsDigit(lastChar) && lastChar >= '1' && lastChar <= '8') {
                    var count = Convert.ToInt32(lastChar) - Convert.ToInt32('0');
                    return count;
                }

                throw new Exception("Last character of product name doesn't recognized as channel's count.");
            }
        }

        private void ReadStatusRaw(out byte[] rawData, out int status) {
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

        /// <summary>
        /// Reads Id of the relay module.
        /// </summary>
        /// <returns>Id of the relay module.</returns>
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

        /// <summary>
        /// Writes new Id to the relay module.
        /// </summary>
        /// <param name="id">New Id value.</param>
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

        /// <summary>
        /// Reads state of specified channel.
        /// </summary>
        /// <param name="channel">Channel number, 1…8.</param>
        /// <returns>State of channel.</returns>
        public bool ReadChannel(int channel) {
            if (channel < 1 || channel > 8) {
                throw new ArgumentOutOfRangeException(nameof(channel), channel, "Channel index should be in the range 1…8!");
            }

            var status = this.ReadChannels();
            return (status & (1 << (channel - 1))) != 0;
        }

        /// <summary>
        /// Read state of all channels.
        /// </summary>
        /// <returns>State of channels.</returns>
        public int ReadChannels() {
            this.ReadStatusRaw(out _, out var status);
            return status;
        }

        /// <summary>
        /// Writes state of specified channel.
        /// </summary>
        /// <param name="channel">Channel number, 1…8.</param>
        /// <param name="state">New state of channel.</param>
        /// <returns>True, if state was successfully changed.</returns>
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

        /// <summary>
        /// Writes state of all channels at once.
        /// </summary>
        /// <param name="state">New state of all channels.</param>
        /// <returns>True, if state was successfully changed.</returns>
        public bool WriteChannels(bool state) {
            var buffer = new byte[9];
            buffer[0] = 0;
            buffer[1] = state ? (byte)0xfe : (byte)0xfc;
            buffer[2] = 0;

            if (this._device.SetFeature(0, buffer)) {
                var status = this.ReadChannels();

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
