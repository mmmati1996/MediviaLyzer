using Prism.Commands;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace MediviaLyzer
{
    class MainWindowModelView
    {
        public DelegateCommand CloseWindow { get; set; }
        public DelegateCommand SwitchHomeTab { get; set; }
        

        public MainWindowModelView()
        {
            this.CloseWindow = new DelegateCommand(ClosingWindow);
            this.SwitchHomeTab = new DelegateCommand(HomeTab);
        }
        private void ClosingWindow()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void HomeTab()
        {
            Debug.WriteLine("home");
        }
    }
}
