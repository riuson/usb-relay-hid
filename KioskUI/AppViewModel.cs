using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using UsbRelayNet.RelayLib;

namespace KioskUI {
    public class AppViewModel : ReactiveObject {
        private readonly ReadOnlyObservableCollection<RelayItem> _bindingFoundRelays;
        private readonly SourceList<RelayItem> _foundRelays = new SourceList<RelayItem>();
        private readonly RelaysEnumerator _relaysEnumerator = new RelaysEnumerator();

        public AppViewModel() {
            var foundRelaysDerived = this._foundRelays
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this._bindingFoundRelays)
                .Subscribe();

            this.CommandFind = ReactiveCommand.Create(this.Find);
            this.CommandOpenAll = ReactiveCommand.Create(this.OpenAll);
            this.CommandCloseAll = ReactiveCommand.Create(this.CloseAll);
        }

        [Reactive] public ReactiveCommand<Unit, Unit> CommandFind { get; set; }
        [Reactive] public ReactiveCommand<Unit, Unit> CommandOpenAll { get; set; }
        [Reactive] public ReactiveCommand<Unit, Unit> CommandCloseAll { get; set; }

        public ReadOnlyObservableCollection<RelayItem> FoundRelays => this._bindingFoundRelays;

        [Reactive] public RelayItem SelectedRelay { get; set; }

        private void Find() {
            var items = this._relaysEnumerator.CollectInfo()
                .Select(x => new RelayItem(x))
                .ToArray();
            this._foundRelays.Clear();
            this._foundRelays.AddRange(items);
        }

        private void OpenAll() {
            var relayInfo = this.SelectedRelay?.RelayInfo;

            if (relayInfo != null) {
                using (var relay = new Relay(relayInfo)) {
                    if (relay.Open()) {
                        relay.WriteChannels(true);
                        relay.Close();
                    } else {
                        SystemSounds.Exclamation.Play();
                    }
                }
            } else {
                SystemSounds.Exclamation.Play();
            }
        }

        private void CloseAll() {
            var relayInfo = this.SelectedRelay?.RelayInfo;

            if (relayInfo != null) {
                using (var relay = new Relay(relayInfo)) {
                    if (relay.Open()) {
                        relay.WriteChannels(false);
                        relay.Close();
                    } else {
                        SystemSounds.Exclamation.Play();
                    }
                }
            } else {
                SystemSounds.Exclamation.Play();
            }
        }
    }
}
