using System;

namespace UsbRelayNet.Win32 {
    /// <summary>
    /// Constants used for system functions.
    /// </summary>
    public static class Constants {

        /// <summary>
        /// Return only devices that are currently present in a system.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdigetclassdevsexw">SetupDiGetClassDevsExW function</see>.
        /// </summary>
        public const int DIGCF_PRESENT = 0x02;

        /// <summary>
        /// Return devices that support device interfaces for the specified device interface classes.
        /// This flag must be set in the Flags parameter if the Enumerator parameter specifies a device instance ID.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/setupapi/nf-setupapi-setupdigetclassdevsexw">SetupDiGetClassDevsExW function</see>.
        /// </summary>
        public const int DIGCF_DEVICEINTERFACE = 0x10;

        /// <summary>
        /// Value of invalid handle (-1).
        /// </summary>
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        /// <summary>
        /// Read access.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#remarks">CreateFileW function</see>.
        /// </summary>
        public const uint GENERIC_READ = 0x80000000;

        /// <summary>
        /// Write access.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#remarks">CreateFileW function</see>.
        /// </summary>
        public const uint GENERIC_WRITE = 0x40000000;

        /// <summary>
        /// Enables subsequent open operations on a file or device to request write access.
        /// Otherwise, other processes cannot open the file or device if they request write access.
        /// If this flag is not specified, but the file or device has been opened for write access or has a file mapping with write access, the function fails.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#remarks">CreateFileW function</see>.
        /// </summary>
        public const uint FILE_SHARE_WRITE = 0x2;

        /// <summary>
        /// Enables subsequent open operations on a file or device to request read access.
        /// Otherwise, other processes cannot open the file or device if they request read access.
        /// If this flag is not specified, but the file or device has been opened for read access, the function fails.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#remarks">CreateFileW function</see>.
        /// </summary>
        public const uint FILE_SHARE_READ = 0x1;

        /// <summary>
        /// Opens a file or device, only if it exists.
        /// If the specified file or device does not exist, the function fails and the last-error code is set to ERROR_FILE_NOT_FOUND(2).
        /// For more information about devices, see the Remarks section.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#remarks">CreateFileW function</see>.
        /// </summary>
        public const uint OPEN_EXISTING = 3;
    }
}
