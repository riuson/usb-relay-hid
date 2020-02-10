using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UsbRelayNet.Win32;

namespace UsbRelayNet
{
    public class UsbHid
    {
        public UsbHid()
        {

        }

        public string GetVendorString(IntPtr handle)
        {
            var buffer = new byte[256];
            Hid.HidD_GetManufacturerString(handle, ref buffer[0], Convert.ToUInt32(buffer.Length));
            return ConvertHidString(buffer);
        }

        public string GetProductString(IntPtr handle)
        {
            var _buf = new byte[256];
            Hid.HidD_GetProductString(handle, ref _buf[0], 256);
            return ConvertHidString(_buf);
        }

        public void EnumDevices(int vendorId, int productId)
        {
            Guid hidGuid;
            Hid.HidD_GetHidGuid(out hidGuid);

            IntPtr deviceInfoList = SetupApi.SetupDiGetClassDevs(ref hidGuid, null, IntPtr.Zero,
                Constants.DIGCF_DEVICEINTERFACE | Constants.DIGCF_PRESENT);

            if (deviceInfoList != IntPtr.Zero && deviceInfoList != Constants.INVALID_HANDLE_VALUE)
            {
                var deviceInfo = new SetupApi.SP_DEVICE_INTERFACE_DATA();
                deviceInfo.cbSize = Marshal.SizeOf(deviceInfo);

                IntPtr handle = Constants.INVALID_HANDLE_VALUE;

                for (int i = 0; ; i++)
                {
                    if (handle != Constants.INVALID_HANDLE_VALUE)
                    {
                        Kernel32.CloseHandle(handle);
                        handle = Constants.INVALID_HANDLE_VALUE;
                    }

                    if (!SetupApi.SetupDiEnumDeviceInterfaces(deviceInfoList, 0, ref hidGuid, Convert.ToUInt32(i),
                        ref deviceInfo))
                    {
                        break;
                    }

                    var path = this.GetPath(deviceInfoList, deviceInfo);
                }
            }
        }

        private string ConvertHidString(byte[] bytes)
        {
            string str = Encoding.Unicode.GetString(bytes);
            str = str.Trim('\0');
            return str;
        }

        private string GetPath(IntPtr deviceInfoList, SetupApi.SP_DEVICE_INTERFACE_DATA deviceInfo)
        {
            var deviceInterfaceDetailData = IntPtr.Zero;

            if (!SetupApi.SetupDiGetDeviceInterfaceDetail(deviceInfoList, ref deviceInfo,
                IntPtr.Zero, 0,
                out var needed, IntPtr.Zero))
            {
                int error = Marshal.GetLastWin32Error();

                if (error != 122)
                {
                    return string.Empty;
                }
            }


            var spDeviceInterfaceData = new SetupApi.SP_DEVICE_INTERFACE_DATA();
            spDeviceInterfaceData.cbSize = Marshal.SizeOf(spDeviceInterfaceData);

            try
            {
                deviceInterfaceDetailData = Marshal.AllocHGlobal((int)(8 + needed));
                int size = IntPtr.Size == 8 ? 8 : 6;
                Marshal.WriteInt32(deviceInterfaceDetailData, size);
                bool result4 = SetupApi.SetupDiGetDeviceInterfaceDetail(deviceInfoList, ref deviceInfo,
                    deviceInterfaceDetailData, needed, out needed, IntPtr.Zero);

                if (!result4)
                {
                    int error = Marshal.GetLastWin32Error();
                }

                IntPtr pStr = IntPtr.Add(deviceInterfaceDetailData, size);
                string path = Marshal.PtrToStringUni(pStr);

                return path;
            }
            finally
            {
                Marshal.FreeHGlobal(deviceInterfaceDetailData);
            }
        }

        private void GetAttributes()
        {
            Win32Usb.HidD_Attributes attributes = new Win32.Win32Usb.HidD_Attributes();
            attributes.Size = Marshal.SizeOf(attributes);
            Win32Usb.HidD_GetAttributes(handle, ref attributes);

            this.VendorId = attributes.VendorID;
            this.ProductId = attributes.ProductID;
            this.Version = String.Format("{0}.{1}", (attributes.VersionNumber >> 8) & 0xff, attributes.VersionNumber & 0xff);

        }
    }
}
