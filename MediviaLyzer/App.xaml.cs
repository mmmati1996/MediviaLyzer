using System;
using System.Diagnostics;
using System.Windows;
using CommonServiceLocator;
using Prism.Ioc;
using Prism.Unity;

namespace MediviaLyzer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<Dialogs.Views.MediviaProcessPicker, Dialogs.ViewModels.MediviaProcessPickerViewModel>();
            containerRegistry.RegisterForNavigation<Tabs.Views.HomePage>("HomePage");
            containerRegistry.RegisterForNavigation<Tabs.Views.SettingsPage>("SettingsPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.AboutPage>("AboutPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.HUDPage>("HUDPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.DatabasePage>("DatabasePage");
            containerRegistry.RegisterForNavigation<Tabs.Views.BedmakersHUDSettings>("BedmakersSettings");
        }
    }
}
