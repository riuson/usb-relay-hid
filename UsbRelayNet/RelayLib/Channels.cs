using System;

namespace UsbRelayNet.RelayLib {
    [Flags]
    public enum Channels : byte {
        None = 0x00,
        Channel1 = 0x01,
        Channel2 = 0x02,
        Channel3 = 0x04,
        Channel4 = 0x08,
        Channel5 = 0x10,
        Channel6 = 0x20,
        Channel7 = 0x40,
        Channel8 = 0x80,
        All = 0xff,
    }
}
