using System;
using System.Runtime.InteropServices;

namespace UsbRelayNet.Win32 {
    /// <summary>
    /// Functions, imported from setupapi.dll
    /// </summary>
    public static class SetupApi {
        #region Structures
        /// <summary>
        /// An SP_DEVICE_INTERFACE_DATA structure defines a device interface in a device information set.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/setupapi/ns-setupapi-sp_device_interface_data">SP_DEVICE_INTERFACE_DATA structure</see>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA {
            /// <summary>
            /// The size, in bytes, of the SP_DEVICE_INTERFACE_DATA structure. For more information, see the Remarks section.
            /// </summary>
            public int cbSize;
            /// <summary>
            /// The GUID for the class to which the device interface belongs.
            /// </summary>
            public Guid interfaceClassGuid;
            /// <summary>
            /// Can be one or more of the following:
            /// <list type="bullet">
            ///   <item>
            ///     <term>SPINT_ACTIVE</term>
            ///     <description>The interface is active (enabled).</description>
            ///   </item>
            ///   <item>
            ///     <term>SPINT_DEFAULT</term>
            ///     <description>The interface is the default interface for the device class.</description>
            ///   </item>
            ///   <item>
            ///     <term>SPINT_REMOVED</term>
            ///     <description>The interface is removed.</description>
            ///   </item>
            /// </list>
            /// </summary>
            public int flags;
            /// <summary>
            /// Reserved. Do not use.
            /// </summary>
            private UIntPtr reserved;
        }

        /// <summary>
        /// An SP_DEVINFO_DATA structure defines a device instance that is a member of a device information set.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/setupapi/ns-setupapi-sp_devinfo_data">SP_DEVINFO_DATA structure</see>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA {
            /// <summary>
            /// The size, in bytes, of the SP_DEVINFO_DATA structure. For more information, see the following Remarks section.
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// The GUID of the device's setup class.
            /// </summary>
            public Guid ClassGuid;
            /// <summary>
            /// An opaque handle to the device instance (also known as a handle to the devnode).
            /// </summary>
            public uint DevInst;
            /// <summary>
            /// Reserved. For internal use only.
            /// </summary>
            public IntPtr Reserved;
        }

        #endregion

        #region Functions

        /// <summary>
        /// The SetupDiGetClassDevs function returns a handle to a device information set that contains requested device information elements for a local computer.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdigetclassdevsw">SetupDiGetClassDevsW function</see>.
        /// </summary>
        /// <param name="gClass">A pointer to the GUID for a <see href="https://docs.microsoft.com/windows/desktop/api/setupapi/ns-setupapi-sp_devinfo_data">device setup class</see> or a <see href="https://msdn.microsoft.com/C989D2D3-E8DE-4D64-86EE-3D3B3906390D">device interface class</see>. This pointer is optional and can be NULL. For more information about how to set ClassGuid, see the following Remarks section.</param>
        /// <param name="strEnumerator">
        /// A pointer to a NULL-terminated string that specifies:
        /// <list type="bullet">
        ///   <item>
        ///     <description>An identifier(ID) of a Plug and Play(PnP) enumerator.
        ///       This ID can either be the value's globally unique identifier (GUID) or symbolic name.
        ///       For example, "PCI" can be used to specify the PCI PnP value.
        ///       Other examples of symbolic names for PnP values include "USB," "PCMCIA," and "SCSI".
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>A PnP device instance ID.When specifying a PnP device instance ID, DIGCF_DEVICEINTERFACE must be set in the Flags parameter.
        ///     </description>
        ///   </item>
        /// </list>
        /// This pointer is optional and can be NULL.If an enumeration value is not used to select devices, set Enumerator to NULL</param>
        /// <param name="hParent">A handle to the top-level window to be used for a user interface that is associated with installing a device instance in the device information set.
        /// This handle is optional and can be NULL.</param>
        /// <param name="nFlags">A variable of type DWORD that specifies control options that filter the device information elements that are added to the device information set.</param>
        /// <returns>If the operation succeeds, SetupDiGetClassDevs returns a handle to a <see href="https://docs.microsoft.com/windows-hardware/drivers/install/device-information-sets">device information set</see> that contains all installed devices that matched the supplied parameters. If the operation fails, the function returns INVALID_HANDLE_VALUE. To get extended error information, call GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, [MarshalAs(UnmanagedType.LPStr)] string strEnumerator, IntPtr hParent, uint nFlags);

        /// <summary>
        /// The SetupDiGetDeviceInterfaceDetail function returns details about a device interface.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdigetdeviceinterfacedetailw">SetupDiGetDeviceInterfaceDetailW function</see>.
        /// </summary>
        /// <param name="hDevInfo">A pointer to the device information set that contains the interface for which to retrieve details. This handle is typically returned by SetupDiGetClassDevs.</param>
        /// <param name="deviceInterfaceData">A pointer to an SP_DEVICE_INTERFACE_DATA structure that specifies the interface in DeviceInfoSet for which to retrieve details. A pointer of this type is typically returned by SetupDiEnumDeviceInterfaces.</param>
        /// <param name="deviceInterfaceDetailData">A pointer to an SP_DEVICE_INTERFACE_DETAIL_DATA structure to receive information about the specified interface.
        /// This parameter is optional and can be NULL.
        /// This parameter must be NULL if DeviceInterfaceDetailSize is zero.
        /// If this parameter is specified, the caller must set DeviceInterfaceDetailData.cbSize to sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA) before calling this function.
        /// The cbSize member always contains the size of the fixed part of the data structure, not a size reflecting the variable-length string at the end.</param>
        /// <param name="deviceInterfaceDetailDataSize">The size of the DeviceInterfaceDetailData buffer.
        /// The buffer must be at least (offsetof(SP_DEVICE_INTERFACE_DETAIL_DATA, DevicePath) + sizeof(TCHAR)) bytes, to contain the fixed part of the structure and a single NULL to terminate an empty MULTI_SZ string.
        /// This parameter must be zero if DeviceInterfaceDetailData is NULL.</param>
        /// <param name="requiredSize">A pointer to a variable of type DWORD that receives the required size of the DeviceInterfaceDetailData buffer.
        /// This size includes the size of the fixed part of the structure plus the number of bytes required for the variable-length device path string.
        /// This parameter is optional and can be NULL.</param>
        /// <param name="deviceInfoData">A pointer to a buffer that receives information about the device that supports the requested interface.
        /// The caller must set DeviceInfoData.cbSize to sizeof(SP_DEVINFO_DATA).
        /// This parameter is optional and can be NULL.</param>
        /// <returns>SetupDiGetDeviceInterfaceDetail returns TRUE if the function completed without error.
        /// If the function completed with an error, FALSE is returned and the error code for the failure can be retrieved by calling GetLastError.</returns>
        [DllImport(@"setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr hDevInfo,
            ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            IntPtr deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,
            out uint requiredSize,
            IntPtr deviceInfoData
        );

        /// <summary>
        /// The SetupDiEnumDeviceInterfaces function enumerates the device interfaces that are contained in a device information set.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/ru-ru/windows/win32/api/setupapi/nf-setupapi-setupdienumdeviceinterfaces">SetupDiEnumDeviceInterfaces function</see>.
        /// </summary>
        /// <param name="lpDeviceInfoSet">A pointer to a device information set that contains the device interfaces for which to return information.
        /// This handle is typically returned by SetupDiGetClassDevs.</param>
        /// <param name="nDeviceInfoData">A pointer to an SP_DEVINFO_DATA structure that specifies a device information element in DeviceInfoSet.
        /// This parameter is optional and can be NULL.
        /// If this parameter is specified, SetupDiEnumDeviceInterfaces constrains the enumeration to the interfaces that are supported by the specified device.
        /// If this parameter is NULL, repeated calls to SetupDiEnumDeviceInterfaces return information about the interfaces that are associated with all the device information elements in DeviceInfoSet.
        /// This pointer is typically returned by SetupDiEnumDeviceInfo.</param>
        /// <param name="gClass">A pointer to a GUID that specifies the device interface class for the requested interface.</param>
        /// <param name="nIndex">A zero-based index into the list of interfaces in the device information set.
        /// The caller should call this function first with MemberIndex set to zero to obtain the first interface.
        /// Then, repeatedly increment MemberIndex and retrieve an interface until this function fails and GetLastError returns ERROR_NO_MORE_ITEMS.</param>
        /// <param name="oInterfaceData">A pointer to a caller-allocated buffer that contains, on successful return, a completed SP_DEVICE_INTERFACE_DATA structure that identifies an interface that meets the search parameters.
        /// The caller must set DeviceInterfaceData.cbSize to sizeof(SP_DEVICE_INTERFACE_DATA) before calling this function.</param>
        /// <returns>SetupDiEnumDeviceInterfaces returns TRUE if the function completed without error.
        /// If the function completed with an error, FALSE is returned and the error code for the failure can be retrieved by calling GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr lpDeviceInfoSet, uint nDeviceInfoData, ref Guid gClass, uint nIndex, ref SP_DEVICE_INTERFACE_DATA oInterfaceData);


        /// <summary>
        /// The SetupDiDestroyDeviceInfoList function deletes a device information set and frees all associated memory.
        /// </summary>
        /// <param name="lpInfoSet">A handle to the device information set to delete.</param>
        /// <returns>The function returns TRUE if it is successful.
        /// Otherwise, it returns FALSE and the logged error can be retrieved with a call to GetLastError.</returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern int SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

        #endregion
    }
}
