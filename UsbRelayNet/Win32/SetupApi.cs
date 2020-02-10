using System;
using System.Runtime.InteropServices;

namespace UsbRelayNet.Win32
{
    public static class SetupApi
    {
        #region Structures
        //https://www.pinvoke.net/default.aspx/Structures/SP_DEVICE_INTERFACE_DATA.html
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public Int32 cbSize;
            public Guid interfaceClassGuid;
            public Int32 flags;
            private UIntPtr reserved;
        }

        //https://www.pinvoke.net/default.aspx/Structures/SP_DEVICE_INTERFACE_DETAIL_DATA.html
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string DevicePath;
        }

        //https://www.pinvoke.net/default.aspx/Structures/SP_DEVINFO_DATA.html
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public UInt32 cbSize;
            public Guid ClassGuid;
            public UInt32 DevInst;
            public IntPtr Reserved;
        }

        #endregion

        #region Functions

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, [MarshalAs(UnmanagedType.LPStr)] string strEnumerator, IntPtr hParent, uint nFlags);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
            IntPtr hDevInfo,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
            UInt32 deviceInterfaceDetailDataSize,
            out UInt32 requiredSize,
            ref SP_DEVINFO_DATA deviceInfoData
        );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
            IntPtr hDevInfo,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            IntPtr deviceInterfaceDetailData,
            UInt32 deviceInterfaceDetailDataSize,
            out UInt32 requiredSize,
            IntPtr deviceInfoData
        );

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr lpDeviceInfoSet, uint nDeviceInfoData, ref Guid gClass, uint nIndex, ref SP_DEVICE_INTERFACE_DATA oInterfaceData);


        [DllImport("setupapi.dll")]
        public static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        #endregion
    }
}
