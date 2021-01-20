using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediviaLyzer.HUDs.Views
{
    /// <summary>
    /// Interaction logic for CooldownHUD.xaml
    /// </summary>
    public partial class CooldownHUD : UserControl
    {
        private int PreviousState { get; set; }
        public CooldownHUD()
        {
            InitializeComponent();
            var w = DataContext as ViewModels.CooldownHUDViewModel;
            w.IsFocused = new Func<bool>(() => {
                return this.Dispatcher.Invoke(() =>
                {
                    return ((DialogWindow)this.Parent).IsMouseOver;
                });
            });
            w.Lock = new Action<bool>((x) => {
                var helper = new WindowInteropHelper((DialogWindow)this.Parent);
                if (x)
                    Others.Natives.SetWindowLongPtr(helper.Handle, -20, 0x80000 | 0x20);
                else
                    Others.Natives.SetWindowLongPtr(helper.Handle, -20, PreviousState);
            });
        }

        private void ProgressBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                ((DialogWindow)this.Parent).DragMove();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((DialogWindow)this.Parent).Owner = null;
            var w = DataContext as ViewModels.CooldownHUDViewModel;
            w.Show = new Action(((DialogWindow)this.Parent).Show);
            w.Hide = new Action(((DialogWindow)this.Parent).Hide);
            var helper = new WindowInteropHelper((DialogWindow)this.Parent);
            PreviousState = Others.Natives.GetWindowLongPtr(helper.Handle, -20);
        }
    }
}
