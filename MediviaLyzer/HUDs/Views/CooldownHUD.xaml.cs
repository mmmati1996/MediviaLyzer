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
        }

        private void ProgressBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((DialogWindow)this.Parent).DragMove();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ((DialogWindow)this.Parent).Owner = null;
            var w = DataContext as ViewModels.CooldownHUDViewModel;
            w.Show = new Action(((DialogWindow)this.Parent).Show);
            w.Hide = new Action(((DialogWindow)this.Parent).Hide);
        }
    }
}
