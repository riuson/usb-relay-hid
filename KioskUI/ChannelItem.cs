using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using UsbRelayNet.RelayLib;

namespace KioskUI {
    public class ChannelItem : ReactiveObject {
        public ChannelItem(RelayInfo relayInfo, int channel, string title) {
            this.RelayInfo = relayInfo;
            this.Channel = channel;
            this.Title = title;

            this.IsOpen = this.GetCurrentState(channel);
            this.CommandToggle = ReactiveCommand.Create<int>(this.Toggle);
        }

        public RelayInfo RelayInfo { get; }
        public int Channel { get; }
        public string Title { get; }

        public ReactiveCommand<int, Unit> CommandToggle { get; }

        [Reactive] public bool IsOpen { get; set; }

        private void Toggle(int channel) {
            var relay = new Relay(this.RelayInfo);
            if (relay.Open()) {
                relay.WriteChannel(channel + 1, !this.IsOpen);
                this.IsOpen = !this.IsOpen;

                relay.Close();
            }
        }

        private bool GetCurrentState(int channel) {
            var relay = new Relay(this.RelayInfo);
            var result = false;

            if (relay.Open()) {
                result = relay.ReadChannel(channel + 1);
                relay.Close();
            }

            return result;
        }
    }
}
