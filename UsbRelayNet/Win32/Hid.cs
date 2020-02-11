using System;
using System.Runtime.InteropServices;

namespace UsbRelayNet.Win32 {
    /// <summary>
    /// Functions, imported from hid.dll
    /// </summary>
    public static class Hid {
        #region Structures

        /// <summary>
        /// The HIDD_ATTRIBUTES structure contains vendor information about a HIDClass device.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/ns-hidsdi-_hidd_attributes">HIDD_ATTRIBUTES structure</see>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct HidD_Attributes {
            /// <summary>
            /// Specifies the size, in bytes, of a HIDD_ATTRIBUTES structure.
            /// </summary>
            public int Size;
            /// <summary>
            /// Specifies a HID device's vendor ID.
            /// </summary>
            public ushort VendorID;
            /// <summary>
            /// Specifies a HID device's product ID.
            /// </summary>
            public ushort ProductID;
            /// <summary>
            /// Specifies the manufacturer's revision number for a HIDClass device.
            /// </summary>
            public ushort VersionNumber;
            /// <summary>
            /// Specifies the manufacturer's revision number for a HIDClass device as string.
            /// </summary>
            public string VersionString => $"{(this.VersionNumber >> 8) & 0xff}.{this.VersionNumber & 0xff}";
        }

        #endregion

        #region Functions

        /// <summary>
        /// The HidD_GetManufacturerString routine returns a top-level collection's embedded string that identifies the manufacturer.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getmanufacturerstring">HidD_GetManufacturerString function</see>.
        /// </summary>
        /// <param name="hFile">Specifies an open handle to a top-level collection.</param>
        /// <param name="lpBuffer">Pointer to a caller-allocated buffer that the routine uses to return the collection's manufacturer string.
        /// The routine returns a NULL-terminated wide character string in a human-readable format.</param>
        /// <param name="BufferLength">Specifies the length, in bytes, of a caller-allocated buffer provided at Buffer.
        /// If the buffer is not large enough to return the entire NULL-terminated embedded string, the routine returns nothing in the buffer.</param>
        /// <returns>HidD_HidD_GetManufacturerString returns TRUE if it returns the entire NULL-terminated embedded string.
        /// Otherwise, the routine returns FALSE. Use GetLastError to get extended error information.</returns>
        [DllImport("hid.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int HidD_GetManufacturerString(IntPtr hFile, ref byte lpBuffer, uint BufferLength);

        /// <summary>
        /// The HidD_GetProductString routine returns the embedded string of a top-level collection that identifies the manufacturer's product.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getproductstring">HidD_GetProductString function</see>.
        /// </summary>
        /// <param name="hFile">Specifies an open handle to a top-level collection.</param>
        /// <param name="lpBuffer">Pointer to a caller-allocated buffer that the routine uses to return the requested product string.
        /// The routine returns a NULL-terminated wide character string.</param>
        /// <param name="BufferLength">Specifies the length, in bytes, of a caller-allocated buffer provided at Buffer.
        /// If the buffer is not large enough to return the entire NULL-terminated embedded string, the routine returns nothing in the buffer.</param>
        /// <returns>HidD_GetProductString returns TRUE if it successfully returns the entire NULL-terminated embedded string.
        /// Otherwise, the routine returns FALSE. Use GetLastError to get extended error information.</returns>
        [DllImport("hid.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int HidD_GetProductString(IntPtr hFile, ref byte lpBuffer, uint BufferLength);

        /// <summary>
        /// The HidD_GetHidGuid routine returns the <see href="https://docs.microsoft.com/windows-hardware/drivers/">device interface GUID</see> for HIDClass devices.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_gethidguid">HidD_GetHidGuid function</see>
        /// </summary>
        /// <param name="gHid">Pointer to a caller-allocated GUID buffer that the routine uses to return the device interface GUID for HIDClass devices.</param>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(out Guid gHid);

        /// <summary>
        /// The HidD_GetAttributes routine returns the attributes of a specified top-level collection.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getattributes">HidD_GetAttributes function</see>.
        /// </summary>
        /// <param name="hFile">Specifies an open handle to a top-level collection.</param>
        /// <param name="Attributes">Pointer to a caller-allocated HIDD_ATTRIBUTES structure that returns the attributes of the collection specified by HidDeviceObject.</param>
        /// <returns>HidD_GetAttributes returns TRUE if succeeds; otherwise, it returns FALSE.</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetAttributes(IntPtr hFile, ref HidD_Attributes Attributes);

        /// <summary>
        /// The HidD_SetFeature routine sends a feature report to a top-level collection.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_setfeature">HidD_SetFeature function</see>.
        /// </summary>
        /// <param name="hFile">Specifies an open handle to a top-level collection.</param>
        /// <param name="lpReportBuffer">Pointer to a caller-allocated feature report buffer that the caller uses to specify a HID report ID.</param>
        /// <param name="ReportBufferLength">Specifies the size, in bytes, of the report buffer.
        /// The report buffer must be large enough to hold the feature report -- excluding its report ID, if report IDs are used -- plus one additional byte that specifies a nonzero report ID or zero.</param>
        /// <returns>If HidD_SetFeature succeeds, it returns TRUE; otherwise, it returns FALSE. Use GetLastError to get extended error information.</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetFeature(IntPtr hFile, ref byte lpReportBuffer, int ReportBufferLength);

        /// <summary>
        /// The HidD_GetFeature routine returns a feature report from a specified top-level collection.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/hidsdi/nf-hidsdi-hidd_getfeature">HidD_GetFeature function</see>.
        /// </summary>
        /// <param name="hFile">Specifies an open handle to a top-level collection.</param>
        /// <param name="lpReportBuffer">Pointer to a caller-allocated HID report buffer that the caller uses to specify a report ID.
        /// HidD_GetFeature uses ReportBuffer to return the specified feature report.</param>
        /// <param name="ReportBufferLength">Specifies the size, in bytes, of the report buffer.
        /// The report buffer must be large enough to hold the feature report -- excluding its report ID, if report IDs are used -- plus one additional byte that specifies a nonzero report ID or zero.</param>
        /// <returns>If HidD_GetFeature succeeds, it returns TRUE; otherwise, it returns FALSE. Use GetLastError to get extended error information.</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetFeature(IntPtr hFile, ref byte lpReportBuffer, int ReportBufferLength);

        #endregion
    }
}
