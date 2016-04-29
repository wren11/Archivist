using System.Windows;

using Archivist.ViewModels;
using Archivist.Views;

namespace Archivist
{
    public partial class App : Application
    {
        public static readonly string ApplicationName = "Archivist";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainViewModel { DisplayName = ApplicationName };
            var window = new MainWindow();

            viewModel.CloseRequested += delegate { window.Close(); };

            window.DataContext = viewModel;
            window.ShowDialog();
        }
    }
}
