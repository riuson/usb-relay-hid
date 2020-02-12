using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UsbRelayNet.RelayLib;

namespace DemoUI {
    public partial class FormMain : Form {
        private Label[] _labelsName;
        private Button[] _buttonsOpen;
        private Button[] _buttonsClose;
        private Label[] _labelsStatus;
        private readonly Enumerator _relaysEnumerator = new Enumerator();
        private Relay _selectedRelay = null;

        public FormMain() {
            this.InitializeComponent();

            this.CreateUI();
            this.UpdateControls();
        }

        private void CreateUI() {
            this._labelsName = Enumerable.Range(0, 8)
                .Select(i => new Label() {
                    Anchor = AnchorStyles.None,
                    Text = $@"Relay {i + 1}",
                    AutoSize = true,
                })
                .ToArray();

            this._labelsStatus = Enumerable.Range(0, 8)
                .Select(i => new Label() {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5),
                })
                .ToArray();

            this._buttonsOpen = Enumerable.Range(0, 8)
                .Select(i => new Button() {
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Text = @"Open",
                    Tag = i,
                })
                .ToArray();

            this._buttonsClose = Enumerable.Range(0, 8)
                .Select(i => new Button() {
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    Text = @"Close",
                    Tag = i,
                })
                .ToArray();

            this._buttonsOpen.ForEach(button => { button.Click += this.OnChannelOpen; });
            this._buttonsClose.ForEach(button => { button.Click += this.OnChannelClose; });

            for (var i = 0; i < 8; i++) {
                this.tableLayoutPanel1.Controls.Add(this._labelsName[i], 0, 2 + i);
                this.tableLayoutPanel1.Controls.Add(this._buttonsOpen[i], 1, 2 + i);
                this.tableLayoutPanel1.Controls.Add(this._labelsStatus[i], 2, 2 + i);
                this.tableLayoutPanel1.Controls.Add(this._buttonsClose[i], 3, 2 + i);
            }
        }

        private void UpdateControls() {
            this.SuspendDrawing();

            var relaysFound = this.comboBoxPath.Items.Count > 0;
            var connected = this._selectedRelay?.IsOpened ?? false;

            this._labelsName.ForEach(label => label.Enabled = relaysFound);
            this._labelsStatus.ForEach(label => label.Enabled = relaysFound);
            this._buttonsOpen.ForEach(button => button.Enabled = relaysFound);
            this._buttonsClose.ForEach(button => button.Enabled = relaysFound);
            this.buttonConnect.Enabled = relaysFound;
            this.buttonDisconnect.Enabled = false;
            this.buttonOpenAll.Enabled = relaysFound;
            this.buttonCloseAll.Enabled = relaysFound;
            this.textBoxId.Enabled = false;
            this.textBoxId.Text = string.Empty;
            this.buttonSetId.Enabled = false;

            if (relaysFound) {
                this.buttonFindDevice.Enabled = !connected;
                this.comboBoxPath.Enabled = !connected;

                if (connected) {
                    this.buttonConnect.Enabled = false;
                    this.buttonDisconnect.Enabled = true;
                    this.textBoxId.Enabled = true;
                    this.textBoxId.Text = this._selectedRelay.ReadId();
                    this.buttonSetId.Enabled = true;

                } else {
                    this.buttonConnect.Enabled = true;
                    this.buttonDisconnect.Enabled = false;
                }

                this._labelsName.ForEach((i, label) => label.Enabled = connected && i < this._selectedRelay.ChannelsCount);
                this._labelsStatus.ForEach((i, label) => label.BackColor = (connected && i < this._selectedRelay.ChannelsCount) ? label.BackColor : Color.Transparent);
                this._buttonsOpen.ForEach((i, button) => button.Enabled = connected && i < this._selectedRelay.ChannelsCount);
                this._buttonsClose.ForEach((i, button) => button.Enabled = connected && i < this._selectedRelay.ChannelsCount);
                this.buttonOpenAll.Enabled = connected;
                this.buttonCloseAll.Enabled = connected;
            }

            this.ResumeDrawing();
        }

        private void UpdateChannelsStatus() {
            if (this._selectedRelay?.IsOpened ?? false) {
                var channelsStatus = this._selectedRelay.ReadStatus();

                for (var i = 0; i < 8; i++) {
                    if (i < this._selectedRelay.ChannelsCount) {
                        if ((channelsStatus & (1 << i)) != 0) {
                            this._labelsStatus[i].BackColor = Color.Green;
                        } else {
                            this._labelsStatus[i].BackColor = Color.Red;
                        }
                    } else {
                        this._labelsStatus[i].BackColor = Color.Transparent;
                    }
                }
            }
        }

        private void buttonFindDevice_Click(object sender, System.EventArgs e) {
            this.comboBoxPath.Items.Clear();
            var items = this._relaysEnumerator.CollectDevices()
                .Select(x => new RelayItem(x))
                .ToArray();
            this.comboBoxPath.Items.AddRange(items);

            if (items.Length > 0) {
                this.comboBoxPath.SelectedIndex = 0;
            }

            this.UpdateControls();
        }

        private void buttonConnect_Click(object sender, System.EventArgs e) {
            if (this._selectedRelay == null) {
                if (this.comboBoxPath.Items.Count > 0) {
                    this._selectedRelay = this.comboBoxPath.Items
                        .OfType<RelayItem>()
                        .Select(x => new Relay(x.RelayInfo))
                        .FirstOrDefault();
                }
            }

            if (!this._selectedRelay.IsOpened) {
                this._selectedRelay.Open();
            }

            this.UpdateControls();
            this.UpdateChannelsStatus();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e) {
            if (this._selectedRelay.IsOpened) {
                this._selectedRelay.Close();
            }

            this.UpdateControls();
            this.UpdateChannelsStatus();
        }

        private void OnChannelOpen(object sender, EventArgs e) {
            if (this._selectedRelay != null) {
                var channel = Convert.ToInt32((sender as Button)?.Tag ?? -1);

                if (channel >= 0) {
                    this._selectedRelay.WriteChannel(channel + 1, true);
                    this.UpdateChannelsStatus();
                }
            }
        }

        private void OnChannelClose(object sender, EventArgs e) {
            if (this._selectedRelay != null) {
                var channel = Convert.ToInt32((sender as Button)?.Tag ?? -1);

                if (channel >= 0) {
                    this._selectedRelay.WriteChannel(channel + 1, false);
                    this.UpdateChannelsStatus();
                }
            }
        }

        private void buttonOpenAll_Click(object sender, EventArgs e) {
            this._selectedRelay.WriteChannels(true);
            this.UpdateChannelsStatus();
        }

        private void buttonCloseAll_Click(object sender, EventArgs e) {
            this._selectedRelay.WriteChannels(false);
            this.UpdateChannelsStatus();
        }

        private void buttonSetId_Click(object sender, EventArgs e) {
            if (this._selectedRelay?.IsOpened ?? false) {
                this._selectedRelay.WriteId(this.textBoxId.Text);
            }

            this.UpdateControls();
        }
    }
}
