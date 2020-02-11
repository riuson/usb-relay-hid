using System.Text;

namespace UsbRelayNet.HidLib {
    internal static class ArrayExt {
        public static string GetString(this byte[] array) {
            string str = Encoding.Unicode.GetString(array);
            str = str.Trim('\0');
            return str;
        }
    }
}
