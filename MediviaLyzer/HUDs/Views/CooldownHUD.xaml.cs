using System;
using System.Collections.Generic;
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
    public partial class CooldownHUD : Window
    {
        public CooldownHUD()
        {
            InitializeComponent();
            var w = DataContext as ViewModels.CooldownHUDViewModel;
            w.CloseAction = new Action(this.Close);
            w.IsFocused = new Func<bool>(() => {
                return this.Dispatcher.Invoke(() =>
                {
                    return this.IsMouseOver;
                });
            });
        }
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.DragMove();
        }
    }
}
