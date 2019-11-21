﻿using System;
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
    /// Logika interakcji dla klasy GeneralHUD.xaml
    /// </summary>
    public partial class GeneralHUD : Window
    {
        public GeneralHUD()
        {
            InitializeComponent();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
