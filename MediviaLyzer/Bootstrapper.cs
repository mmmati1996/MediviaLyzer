using System.Windows;
using Prism.Unity;

namespace MediviaLyzer
{
    public class Bootstrapper : UnityBootstrapper 
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterTypeForNavigation<Tabs.Views.HomePage>("HomePage");
            Container.RegisterTypeForNavigation<Tabs.Views.SettingsPage>("SettingsPage");
            Container.RegisterTypeForNavigation<Tabs.Views.AboutPage>("AboutPage");
            Container.RegisterTypeForNavigation<Tabs.Views.HUDPage>("HUDPage");
        }
    }
}
