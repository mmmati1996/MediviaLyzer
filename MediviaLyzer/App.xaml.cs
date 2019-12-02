﻿using System.Windows;
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
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Tabs.Views.HomePage>("HomePage");
            containerRegistry.RegisterForNavigation<Tabs.Views.SettingsPage>("SettingsPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.AboutPage>("AboutPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.HUDPage>("HUDPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.BedmakersHUDSettings>("BedmakersSettings");
        }
    }
}
