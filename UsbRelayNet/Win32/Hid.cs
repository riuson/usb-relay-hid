using System;
using System.Runtime.InteropServices;

namespace UsbRelayNet.Win32
{
    public static class Hid
    {
        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct HidD_Attributes
        {
            public Int32 Size;
            public UInt16 VendorID;
            public UInt16 ProductID;
            public UInt16 VersionNumber;
        }


        #endregion

        #region Functions

        [DllImport("hid.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int HidD_GetManufacturerString(IntPtr hFile, ref byte lpBuffer, uint BufferLength);

        [DllImport("hid.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int HidD_GetProductString(IntPtr hFile, ref byte lpBuffer, uint BufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(out Guid gHid);


        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetAttributes(IntPtr hFile, ref HidD_Attributes Attributes);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetFeature(IntPtr hFile, ref byte lpReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetFeature(IntPtr hFile, ref byte lpReportBuffer, int ReportBufferLength);

        #endregion
    }
}
