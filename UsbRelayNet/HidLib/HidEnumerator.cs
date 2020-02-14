using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UsbRelayNet.Win32;

namespace UsbRelayNet.HidLib {
    internal class HidEnumerator {
        public HidEnumerator() {
        }

        public IEnumerable<HidDeviceInfo> CollectInfo() {
            var result = new List<HidDeviceInfo>();

            Hid.HidD_GetHidGuid(out var hidGuid);

            var deviceInfoList = SetupApi.SetupDiGetClassDevs(ref hidGuid, null, IntPtr.Zero,
                Constants.DIGCF_DEVICEINTERFACE | Constants.DIGCF_PRESENT);

            if (deviceInfoList != IntPtr.Zero && deviceInfoList != Constants.INVALID_HANDLE_VALUE) {
                var deviceInfo = new SetupApi.SP_DEVICE_INTERFACE_DATA();
                deviceInfo.cbSize = Marshal.SizeOf(deviceInfo);

                for (var i = 0; ; i++) {
                    if (!SetupApi.SetupDiEnumDeviceInterfaces(deviceInfoList, 0, ref hidGuid, Convert.ToUInt32(i),
                        ref deviceInfo)) {
                        break;
                    }

                    var path = this.GetPath(deviceInfoList, deviceInfo);

                    var device = new HidDevice();

                    if (device.Open(path)) {
                        var attributes = device.GetAttributes();
                        var vendor = device.GetVendorString();
                        var product = device.GetProductString();

                        result.Add(new HidDeviceInfo(
                            path,
                            attributes.VendorID,
                            attributes.ProductID,
                            attributes.VersionString,
                            vendor,
                            product));

                        device.Close();
                    }
                }

                SetupApi.SetupDiDestroyDeviceInfoList(deviceInfoList);
            }

            return result;
        }

        private string GetPath(IntPtr deviceInfoList, SetupApi.SP_DEVICE_INTERFACE_DATA deviceInfo) {
            var deviceInterfaceDetailData = IntPtr.Zero;

            if (!SetupApi.SetupDiGetDeviceInterfaceDetail(deviceInfoList, ref deviceInfo,
                IntPtr.Zero, 0,
                out var needed, IntPtr.Zero)) {
                var error = Marshal.GetLastWin32Error();

                if (error != 122) {
                    return string.Empty;
                }
            }


            var spDeviceInterfaceData = new SetupApi.SP_DEVICE_INTERFACE_DATA();
            spDeviceInterfaceData.cbSize = Marshal.SizeOf(spDeviceInterfaceData);

            try {
                deviceInterfaceDetailData = Marshal.AllocHGlobal((int)(8 + needed));
                var size = IntPtr.Size == 8 ? 8 : 6;
                Marshal.WriteInt32(deviceInterfaceDetailData, size);
                if (!SetupApi.SetupDiGetDeviceInterfaceDetail(deviceInfoList, ref deviceInfo,
                    deviceInterfaceDetailData, needed, out needed, IntPtr.Zero)) {
                    var error = Marshal.GetLastWin32Error();
                }

                var pStr = IntPtr.Add(deviceInterfaceDetailData, sizeof(uint));
                var path = Marshal.PtrToStringUni(pStr);

                return path;
            } finally {
                Marshal.FreeHGlobal(deviceInterfaceDetailData);
            }
        }
    }
}
