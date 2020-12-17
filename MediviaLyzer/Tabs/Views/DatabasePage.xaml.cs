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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediviaLyzer.Tabs.Views
{
    /// <summary>
    /// Logika interakcji dla klasy DatabasePage.xaml
    /// </summary>
    public partial class DatabasePage : UserControl
    {
        public DatabasePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Database.WikiScrapper wiki = new Database.WikiScrapper();
            try
            {
                wiki.Test();
            }
            catch(Exception ez)
            {
                MessageBox.Show(ez.ToString());
            }
        }
    }
}
