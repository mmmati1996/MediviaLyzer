using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;

namespace MediviaLyzer
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand CloseWindow { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }       

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            this.CloseWindow = new DelegateCommand(ClosingWindow);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void ClosingWindow()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void Navigate(string page)
        {
            _regionManager.RequestNavigate("PagesRegion", page);
        }


    }
}
