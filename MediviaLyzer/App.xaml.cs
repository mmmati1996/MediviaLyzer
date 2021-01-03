using System.Windows;
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
            //containerRegistry.RegisterDialogWindow<HUDs.Views.CooldownHUD>("CooldownHUD");
            containerRegistry.RegisterDialog<Dialogs.Views.MediviaProcessPicker, Dialogs.ViewModels.MediviaProcessPickerViewModel>("MediviaProcessPicker");
            containerRegistry.RegisterDialog<Dialogs.Views.EditAddCooldown, Dialogs.ViewModels.EditAddCooldownViewModel>("EditAddCooldown");
            containerRegistry.RegisterDialog<Dialogs.Views.ColorPicker, Dialogs.ViewModels.ColorPickerViewModel>("ColorPicker");
            containerRegistry.RegisterDialog<HUDs.Views.CooldownHUD, HUDs.ViewModels.CooldownHUDViewModel>("CooldownHUD");
            containerRegistry.RegisterForNavigation<Tabs.Views.HomePage>("HomePage");
            containerRegistry.RegisterForNavigation<Tabs.Views.SettingsPage>("SettingsPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.AboutPage>("AboutPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.HUDPage>("HUDPage");
            containerRegistry.RegisterForNavigation<Tabs.Views.DatabasePage>("DatabasePage");
            containerRegistry.RegisterForNavigation<Tabs.Views.BedmakersHUDSettings>("BedmakersSettings");
            containerRegistry.RegisterForNavigation<Tabs.Views.CooldownHUDSettings>("CooldownSettings");
        }
    }
}
