using System;
using System.Runtime.InteropServices;

namespace UsbRelayNet.Win32 {
    /// <summary>
    /// Functions, imported from kernel32.dll
    /// </summary>
    public static class Kernel32 {
        /// <summary>
        /// Closes an open object handle.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/handleapi/nf-handleapi-closehandle">CloseHandle function</see>.
        /// </summary>
        /// <param name="hFile">A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(IntPtr hFile);

        /// <summary>
        /// Creates or opens a file or I/O device.
        /// The most commonly used I/O devices are as follows: file, file stream, directory, physical disk, volume, console buffer, tape drive, communications resource, mailslot, and pipe.
        /// The function returns a handle that can be used to access the file or device for various types of I/O depending on the file or device and the flags and attributes specified.
        /// <br/>
        /// See <see href="https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew">CreateFileW function</see>.
        /// </summary>
        /// <param name="strName">The name of the file or device to be created or opened. You may use either forward slashes (/) or backslashes () in this name.</param>
        /// <param name="nAccess">The requested access to the file or device, which can be summarized as read, write, both or neither zero).</param>
        /// <param name="nShareMode">The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none (refer to the following table).
        /// Access requests to attributes or extended attributes are not affected by this flag.</param>
        /// <param name="lpSecurity">A pointer to a <see href="https://docs.microsoft.com/previous-versions/windows/desktop/legacy/aa379560(v=vs.85)">SECURITY_ATTRIBUTES</see> structure that contains two separate but related data members: an optional security descriptor, and a Boolean value that determines whether the returned handle can be inherited by child processes.</param>
        /// <param name="nCreationFlags">An action to take on a file or device that exists or does not exist.</param>
        /// <param name="nAttributes">The file or device attributes and flags, FILE_ATTRIBUTE_NORMAL being the most common default value for files.</param>
        /// <param name="lpTemplate">A valid handle to a template file with the GENERIC_READ access right.
        /// The template file supplies file attributes and extended attributes for the file that is being created.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot.
        /// If the function fails, the return value is INVALID_HANDLE_VALUE.To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPWStr)] string strName, uint nAccess,
     uint nShareMode, IntPtr lpSecurity, uint nCreationFlags, uint nAttributes, IntPtr lpTemplate);
    }
}
