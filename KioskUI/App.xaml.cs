using System.Reflection;
using System.Windows;
using ReactiveUI;
using Splat;

namespace KioskUI {
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }
    }
}
