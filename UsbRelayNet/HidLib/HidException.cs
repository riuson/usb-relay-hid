using System;

namespace UsbRelayNet.HidLib {
    public class HidException : System.IO.IOException {
        public HidException(string message)
            : base(message) {
        }

        public HidException(string message, Exception innerException)
            : base(message, innerException) {
        }
    }
}
