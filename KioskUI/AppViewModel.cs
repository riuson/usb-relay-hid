using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using UsbRelayNet.RelayLib;

namespace KioskUI {
    public class AppViewModel : ReactiveObject {
        private readonly ReadOnlyObservableCollection<ChannelItem> _bindingChannels;
        private readonly ReadOnlyObservableCollection<RelayInfo> _bindingFoundRelays;
        private readonly SourceList<ChannelItem> _channels = new SourceList<ChannelItem>();
        private readonly SourceList<RelayInfo> _foundRelays = new SourceList<RelayInfo>();
        private readonly RelaysEnumerator _relaysEnumerator = new RelaysEnumerator();

        public AppViewModel() {
            var foundRelaysDerived = this._foundRelays
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this._bindingFoundRelays)
                .Subscribe();
            var channelsDerived = this._channels
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this._bindingChannels)
                .Subscribe();

            this.CommandFind = ReactiveCommand.Create(this.Find);
            this.CommandReadRelayInfo = ReactiveCommand.Create<RelayInfo>(this.ReadRelayInfo);

            this.WhenAnyValue(x => x.SelectedRelay)
                .InvokeCommand(this.CommandReadRelayInfo);
        }

        [Reactive] public ReactiveCommand<Unit, Unit> CommandFind { get; set; }
        [Reactive] public ReactiveCommand<RelayInfo, Unit> CommandReadRelayInfo { get; set; }

        public ReadOnlyObservableCollection<RelayInfo> FoundRelays => this._bindingFoundRelays;
        public ReadOnlyObservableCollection<ChannelItem> Channels => this._bindingChannels;

        [Reactive] public RelayInfo SelectedRelay { get; set; }

        private void Find() {
            var items = this._relaysEnumerator.CollectInfo()
                .ToArray();
            this._foundRelays.Clear();
            this._foundRelays.AddRange(items);
        }

        private void ReadRelayInfo(RelayInfo relayItem) {
            var relayInfo = this.SelectedRelay;
            this._channels.Clear();

            if (relayInfo != null) {
                var titles = this.GetChannelTitles(relayInfo.Id)
                    .ToArray();

                this._channels.AddRange(
                    Enumerable.Range(0, relayInfo.ChannelsCount)
                        .Select(channel => {
                            string title;

                            if (titles.Any(x => x.id == relayInfo.Id && x.channel == channel + 1)) {
                                var item = titles.FirstOrDefault(y => y.id == relayInfo.Id && y.channel == channel + 1);
                                title = item.title;
                            } else {
                                title = $"Channel {channel}";
                            }

                            return new ChannelItem(
                                relayInfo,
                                channel,
                                title);
                        }));
            }
        }

        private IEnumerable<(string id, int channel, string title)> GetChannelTitles(string id) {
            var dir = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var file = Path.Combine(dir, "ChannelNames.txt");
            var lines = File.ReadAllLines(file);
            var regex = new Regex(@"^(?<id>\w+):(?<channel>\d+):(?<title>.+)$");

            foreach (var line in lines) {
                var match = regex.Match(line);

                if (match.Success) {
                    var lineId = match.Groups["id"].Value;
                    var lineChannel = int.Parse(match.Groups["channel"].Value);
                    var lineTitle = match.Groups["title"].Value;

                    if (lineId == id) {
                        yield return (lineId, lineChannel, lineTitle);
                    }
                }
            }
        }
    }
}
