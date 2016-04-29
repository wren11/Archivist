using System.Windows;

using Archivist.Views;

namespace Archivist
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow();
            window.ShowDialog();
        }
    }
}
