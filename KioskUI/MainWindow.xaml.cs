using ReactiveUI;

namespace KioskUI {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<AppViewModel> {
        public MainWindow() {
            this.InitializeComponent();

            this.ViewModel = new AppViewModel();
        }
    }
}
