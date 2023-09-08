using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ReactiveUI;

namespace KioskUI {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<AppViewModel> {
        public MainWindow() {
            this.InitializeComponent();

            this.ViewModel = new AppViewModel();

            var channelLabels = new List<Label>();
            var stateLabels = new List<Label>();
            var openButtons = new List<Button>();
            var closeButtons = new List<Button>();

            for (var i = 0; i < 8; i++) {
                // Channel labels.
                var channelLabelBorder = new Border {
                    Child = this.AddToList(channelLabels, new Label {
                        Content = new Viewbox {
                            Child = new TextBlock {
                                Text = $"Channel {i}",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            }
                        }
                    }),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                this.GridMain.Children.Add(channelLabelBorder);
                Grid.SetRow(channelLabelBorder, i + 2);
                Grid.SetColumn(channelLabelBorder, 0);

                // Open buttons.
                var buttonOpenBorder = new Border {
                    Child = this.AddToList(openButtons, new Button {
                        Content = new Viewbox {
                            Child = new TextBlock {
                                Text = "Open",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            }
                        }
                    }),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                this.GridMain.Children.Add(buttonOpenBorder);
                Grid.SetRow(buttonOpenBorder, i + 2);
                Grid.SetColumn(buttonOpenBorder, 1);

                // State labels.
                var stateLabelBorder = new Border {
                    Child = this.AddToList(stateLabels, new Label {
                        Content = new Viewbox {
                            Child = new TextBlock {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            }
                        }
                    }),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                this.GridMain.Children.Add(stateLabelBorder);
                Grid.SetRow(stateLabelBorder, i + 2);
                Grid.SetColumn(stateLabelBorder, 2);

                var buttonCloseBorder = new Border {
                    Child = this.AddToList(closeButtons, new Button {
                        Content = new Viewbox {
                            Child = new TextBlock {
                                Text = "Close",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                            }
                        }
                    }),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                this.GridMain.Children.Add(buttonCloseBorder);
                Grid.SetRow(buttonCloseBorder, i + 2);
                Grid.SetColumn(buttonCloseBorder, 3);
            }
        }

        private T AddToList<T>(List<T> list, T control) {
            list.Add(control);
            return control;
        }
    }
}
