namespace UsbRelayNet.HidLib {
    public class HidDeviceInfo {
        private HidDeviceInfo() {
        }

        internal HidDeviceInfo(string path, int vendorId, int productId, string version, string vendor, string product) {
            this.Path = path;
            this.VendorID = vendorId;
            this.ProductId = productId;
            this.Version = version;
            this.Vendor = vendor;
            this.Product = product;
        }

        public string Path { get; }
        public int VendorID { get; }
        public int ProductId { get; }
        public string Version { get; }
        public string Vendor { get; }
        public string Product { get; }
    }
}
