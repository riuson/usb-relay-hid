using System;

namespace UsbRelayNet.Win32
{
    public static class Constants
    {
        
        public const int DIGCF_PRESENT = 0x02;
        
        public const int DIGCF_DEVICEINTERFACE = 0x10;

        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

    }
}
