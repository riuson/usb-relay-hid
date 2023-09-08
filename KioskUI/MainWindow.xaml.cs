﻿using System.Windows;
using System.Windows.Controls;
using ReactiveUI;

namespace KioskUI {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<AppViewModel> {
        public MainWindow() {
            this.InitializeComponent();

            this.ViewModel = new AppViewModel();

            for (var i = 0; i < 8; i++) {
                // Channel labels.
                var channelLabelBorder = new Border {
                    Child = new Label {
                        Content = new TextBlock {
                            Text = $"Channel {i}",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                this.GridMain.Children.Add(channelLabelBorder);
                Grid.SetRow(channelLabelBorder, i + 2);
                Grid.SetColumn(channelLabelBorder, 0);

                // Open buttons.
                var buttonOpenBorder = new Border {
                    Child = new Button {
                        Content = new TextBlock {
                            Text = "Open",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    },
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                this.GridMain.Children.Add(buttonOpenBorder);
                Grid.SetRow(buttonOpenBorder, i + 2);
                Grid.SetColumn(buttonOpenBorder, 1);

                // State labels.
                var stateLabelBorder = new Border {
                    Child = new Label {
                        Content = new TextBlock {
                            Text = $"Channel {i}",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    },
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                this.GridMain.Children.Add(stateLabelBorder);
                Grid.SetRow(stateLabelBorder, i + 2);
                Grid.SetColumn(stateLabelBorder, 2);

                var buttonCloseBorder = new Border {
                    Child = new Button {
                        Content = new TextBlock {
                            Text = "Close",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    },
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                this.GridMain.Children.Add(buttonCloseBorder);
                Grid.SetRow(buttonCloseBorder, i + 2);
                Grid.SetColumn(buttonCloseBorder, 3);
            }
        }
    }
}