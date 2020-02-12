using System;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
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

            var devicesInfo = en.CollectDevices();
            var deviceInfo = devicesInfo.First();
            var device = new Relay(deviceInfo);

            Assert.That(device.IsOpened, Is.False);

            var opened = device.Open();
            Assert.That(opened, Is.True);
            Assert.That(device.IsOpened, Is.True);

            device.Close();
            Assert.That(device.IsOpened, Is.False);
        }

        [Test]
        public void CanReadStatus() {
            var en = new Enumerator();

            var devicesInfo = en.CollectDevices();
            var deviceInfo = devicesInfo.First();
            var device = new Relay(deviceInfo);

            if (device.Open()) {
                device.ReadStatus();

                device.Close();
            }
        }

        [Test]
        public void CanReadId() {
            var en = new Enumerator();

            var devicesInfo = en.CollectDevices();
            var deviceInfo = devicesInfo.First();
            var device = new Relay(deviceInfo);

            if (device.Open()) {
                var id = device.ReadId();

                Assert.That(id.Length, Is.EqualTo(5));

                device.Close();
            }
        }

        [Test]
        public void CanWriteId() {
            var en = new Enumerator();

            var devicesInfo = en.CollectDevices();
            var deviceInfo = devicesInfo.First();
            var device = new Relay(deviceInfo);

            if (device.Open()) {
                var id = device.ReadId();

                var newId = DateTime.Now.ToString("Hmmss").Substring(0, 5);
                device.WriteId(newId);

                var newIdCheck = device.ReadId();

                Assert.That(id, Is.Not.EqualTo(newIdCheck));
                Assert.That(newIdCheck, Is.EqualTo(newId));

                device.Close();
            }
        }

        [Test]
        public void CanReadWriteOneChannel([Range(1, 8)]int channel) {
            var en = new Enumerator();

            var devicesInfo = en.CollectDevices();
            var deviceInfo = devicesInfo.First();
            var device = new Relay(deviceInfo);

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

            var devicesInfo = en.CollectDevices();
            var deviceInfo = devicesInfo.First();
            var device = new Relay(deviceInfo);

            var mask = 0;

            for (var i = device.ChannelsCount - 1; i >= 0; i--) {
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

        [Test]
        public void HasVersionInfo() {
            var assembly = typeof(Enumerator).Assembly;

            var versionAttribute1 = assembly
                .GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
                .Cast<AssemblyFileVersionAttribute>()
                .FirstOrDefault();

            var versionAttribute2 = assembly
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false)
                .Cast<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault();

            var versionAttribute3 = assembly.GetName().Version.ToString();

            Assert.That(string.IsNullOrWhiteSpace(versionAttribute1.Version), Is.False);
            Assert.That(string.IsNullOrWhiteSpace(versionAttribute2.InformationalVersion), Is.False);
            Assert.That(string.IsNullOrWhiteSpace(versionAttribute3), Is.False);
        }
    }
}
