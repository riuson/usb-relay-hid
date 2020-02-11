using System;
using NUnit.Framework;
using System.Linq;
using UsbRelayNet.RelayLib;

namespace UsbRelayNetTests {
    public class UsbRelayTests {
        [Test]
        public void CanCollectDevices() {
            var en = new Enumerator();

            var devices = en.CollectDevices();

            Assert.That(devices.Count(), Is.GreaterThan(0), "At least one device should be connected, but found {0}!", devices.Count());
        }

        [Test]
        public void CanGetChannelsCount() {
            var en = new Enumerator();

            var devices = en.CollectDevices();
            var device = devices.First();

            Assert.That(device.ChannelsCount, Is.GreaterThanOrEqualTo(1));
            Assert.That(device.ChannelsCount, Is.LessThanOrEqualTo(8));
        }

        [Test]
        public void CanOpenClose() {
            var en = new Enumerator();

            var devices = en.CollectDevices();
            var device = devices.First();

            Assert.That(device.IsOpened, Is.False);

            bool opened = device.Open();
            Assert.That(opened, Is.True);
            Assert.That(device.IsOpened, Is.True);

            device.Close();
            Assert.That(device.IsOpened, Is.False);
        }

        [Test]
        public void CanReadStatusRaw() {
            var en = new Enumerator();

            var devices = en.CollectDevices();
            var device = devices.First();

            if (device.Open()) {
                device.ReadStatusRaw(out var rawData, out var states);

                device.Close();
            }
        }

        [Test]
        public void CanReadId() {
            var en = new Enumerator();

            var devices = en.CollectDevices();
            var device = devices.First();

            if (device.Open()) {
                var id = device.ReadId();

                Assert.That(id.Length, Is.EqualTo(5));

                device.Close();
            }
        }

        [Test]
        public void CanReadWriteOneChannel([Range(1, 8)]int channel) {
            var en = new Enumerator();

            var devices = en.CollectDevices();
            var device = devices.First();

            if (device.ChannelsCount < channel) {
                return;
            }

            if (device.Open()) {
                device.WriteChannel(channel, false);
                var state = device.ReadChannel(channel);
                Assert.That(state, Is.False);

                device.WriteChannel(channel, true);
                state = device.ReadChannel(channel);
                Assert.That(state, Is.True);

                device.WriteChannel(channel, false);
                state = device.ReadChannel(channel);
                Assert.That(state, Is.False);

                device.Close();
            }
        }

        [Test]
        public void CanWriteAllChannels() {
            var en = new Enumerator();

            var devices = en.CollectDevices();
            var device = devices.First();

            int mask = 0;

            for (int i = device.ChannelsCount - 1; i >= 0; i--) {
                mask |= 1 << i;
            }

            if (device.Open()) {
                device.WriteChannels(false);
                var status = device.ReadStatus();
                Assert.That(status & mask, Is.EqualTo(0));

                device.WriteChannels(true);
                status = device.ReadStatus();
                Assert.That(status & mask, Is.EqualTo(mask));

                device.WriteChannels(false);
                status = device.ReadStatus();
                Assert.That(status & mask, Is.EqualTo(0));

                device.Close();
            }
        }
    }
}
