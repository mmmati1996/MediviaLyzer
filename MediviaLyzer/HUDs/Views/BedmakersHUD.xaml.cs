using System;
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
        }
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
