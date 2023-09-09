using System.Reactive.Disposables;
using ReactiveUI;

namespace KioskUI {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<AppViewModel> {
        public MainWindow() {
            this.InitializeComponent();

            this.ViewModel = new AppViewModel();

            this.WhenActivated(disposables => {
                this.BindCommand(this.ViewModel,
                        vm => vm.CommandFind,
                        v => v.ButtonFind)
                    .DisposeWith(disposables);

                this.OneWayBind(this.ViewModel,
                        vm => vm.FoundRelays,
                        v => v.ListBoxFound.ItemsSource)
                    .DisposeWith(disposables);
                this.Bind(this.ViewModel,
                        vm => vm.SelectedRelay,
                        v => v.ListBoxFound.SelectedItem)
                    .DisposeWith(disposables);
            });
        }
    }
}
