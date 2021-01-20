using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediviaLyzer.HUDs.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BedmakersHUD.xaml
    /// </summary>
    public partial class BedmakersHUD : Window
    {
        public BedmakersHUD()
        {
            InitializeComponent();
            var w = DataContext as ViewModels.BedmakersHUDViewModel;
            w.CloseAction = new Action(this.Close);
            w.IsFocused = new Func<bool>(() => {
                return this.Dispatcher.Invoke(() =>
                {
                    return this.IsMouseOver; }); 
        });
        }
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            if(e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
