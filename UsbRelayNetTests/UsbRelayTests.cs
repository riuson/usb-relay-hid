using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UsbRelayNet.RelayLib;

namespace UsbRelayNetTests {
    public class UsbRelayTests {
        [Test]
        public void CanCollectDevices() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();

            Assert.That(relaysInfo.Count(), Is.GreaterThan(0),
                "At least one module should be connected, but found {0}!", relaysInfo.Count());
        }

        [Test]
        public void CanGetChannelsCount() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();

            Assert.That(relayInfo.ChannelsCount, Is.GreaterThanOrEqualTo(1));
            Assert.That(relayInfo.ChannelsCount, Is.LessThanOrEqualTo(8));
        }

        [Test]
        public void CanOpenClose() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();
            var relay = new Relay(relayInfo);

            Assert.That(relay.IsOpened, Is.False);

            var opened = relay.Open();
            Assert.That(opened, Is.True);
            Assert.That(relay.IsOpened, Is.True);

            relay.Close();
            Assert.That(relay.IsOpened, Is.False);
        }

        [Test]
        public void CanReadStatus() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();
            var relay = new Relay(relayInfo);

            if (relay.Open()) {
                relay.ReadChannels();

                relay.Close();
            }
        }

        [Test]
        public void CanReadId() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();
            var relay = new Relay(relayInfo);

            if (relay.Open()) {
                var id = relay.ReadId();

                Assert.That(id.Length, Is.EqualTo(5));

                relay.Close();
            }
        }

        [Test]
        public void CanWriteId() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();
            var relay = new Relay(relayInfo);

            if (relay.Open()) {
                var id = relay.ReadId();

                var newId = DateTime.Now.ToString("Hmmss").Substring(0, 5);
                relay.WriteId(newId);

                var newIdCheck = relay.ReadId();

                Assert.That(id, Is.Not.EqualTo(newIdCheck));
                Assert.That(newIdCheck, Is.EqualTo(newId));

                relay.Close();
            }
        }

        [Test]
        public void CanReadWriteOneChannel([Range(1, 8)] int channel) {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();
            var relay = new Relay(relayInfo);

            if (relay.ChannelsCount < channel) {
                return;
            }

            if (relay.Open()) {
                relay.WriteChannel(channel, false);
                var state = relay.ReadChannel(channel);
                Assert.That(state, Is.False);

                relay.WriteChannel(channel, true);
                state = relay.ReadChannel(channel);
                Assert.That(state, Is.True);

                relay.WriteChannel(channel, false);
                state = relay.ReadChannel(channel);
                Assert.That(state, Is.False);

                relay.Close();
            }
        }

        [Test]
        public void CanWriteAllChannels() {
            var en = new RelaysEnumerator();

            var relaysInfo = en.CollectInfo();
            var relayInfo = relaysInfo.First();
            var relay = new Relay(relayInfo);

            var mask = 0;

            for (var i = relay.ChannelsCount - 1; i >= 0; i--) {
                mask |= 1 << i;
            }

            if (relay.Open()) {
                relay.WriteChannels(false);
                var status = relay.ReadChannels();
                Assert.That(status & mask, Is.EqualTo(0));

                relay.WriteChannels(true);
                status = relay.ReadChannels();
                Assert.That(status & mask, Is.EqualTo(mask));

                relay.WriteChannels(false);
                status = relay.ReadChannels();
                Assert.That(status & mask, Is.EqualTo(0));

                relay.Close();
            }
        }

        [Test]
        public void HasVersionInfo() {
            var assembly = typeof(RelaysEnumerator).Assembly;

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
