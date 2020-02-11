using System;

namespace UsbRelayNet.Win32 {
    public static class Constants {
        
        public const int DIGCF_PRESENT = 0x02;
        
        public const int DIGCF_DEVICEINTERFACE = 0x10;

        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public const uint GENERIC_READ = 0x80000000;

        public const uint GENERIC_WRITE = 0x40000000;

        public const uint FILE_SHARE_WRITE = 0x2;

        public const uint FILE_SHARE_READ = 0x1;

        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;

        public const uint OPEN_EXISTING = 3;
    }
}
