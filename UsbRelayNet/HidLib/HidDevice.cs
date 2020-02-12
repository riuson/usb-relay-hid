using System;
using System.Runtime.InteropServices;
using UsbRelayNet.Win32;

namespace UsbRelayNet.HidLib {
    internal sealed class HidDevice : IDisposable {
        private IntPtr _handle;

        public HidDevice() {
            this._handle = Constants.INVALID_HANDLE_VALUE;
        }

        public void Dispose() => this.Close();

        public bool Open(string path, bool shared = false) {
            var handle = Kernel32.CreateFile(
                path,
                Constants.GENERIC_READ | Constants.GENERIC_WRITE,
                (shared ? (Constants.FILE_SHARE_WRITE | Constants.FILE_SHARE_READ) : 0),
                IntPtr.Zero,
                Constants.OPEN_EXISTING,
                0,
                IntPtr.Zero);

            if (handle != Constants.INVALID_HANDLE_VALUE) {
                this._handle = handle;
                return true;
            }

            return false;
        }

        public void Close() {
            if (this._handle != Constants.INVALID_HANDLE_VALUE) {
                Kernel32.CloseHandle(this._handle);
                this._handle = Constants.INVALID_HANDLE_VALUE;
            }
        }

        public bool IsOpened => this._handle != Constants.INVALID_HANDLE_VALUE;

        public Hid.HidD_Attributes GetAttributes() {
            var attributes = new Hid.HidD_Attributes();
            attributes.Size = Marshal.SizeOf(attributes);
            Hid.HidD_GetAttributes(this._handle, ref attributes);
            return attributes;
        }

        public string GetVendorString() {
            var buffer = new byte[260];
            Hid.HidD_GetManufacturerString(this._handle, ref buffer[0], Convert.ToUInt32(buffer.Length));
            return buffer.GetString();
        }

        public string GetProductString() {
            var buffer = new byte[260];
            Hid.HidD_GetProductString(this._handle, ref buffer[0], Convert.ToUInt32(buffer.Length));
            return buffer.GetString();
        }

        public bool GetFeature(int reportNumber, out byte[] result) {
            result = new byte[64];
            result[0] = Convert.ToByte(reportNumber);
            return Hid.HidD_GetFeature(this._handle, ref result[0], result.Length);
        }

        public bool SetFeature(int reportNumber, byte[] data) {
            if (data.Length > 64) {
                throw new ArgumentException("Array too large!", nameof(data));
            }

            data[0] = Convert.ToByte(reportNumber);

            return Hid.HidD_SetFeature(this._handle, ref data[0], data.Length);
        }
    }
}
