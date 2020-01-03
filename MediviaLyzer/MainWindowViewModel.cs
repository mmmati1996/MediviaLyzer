using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;
using Prism.Services.Dialogs;

namespace MediviaLyzer
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand CloseWindow { get; set; }
        public DelegateCommand OpenProcessPickerDialog { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }       

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            this._regionManager = regionManager;
            this._dialogService = dialogService;
            this.CloseWindow = new DelegateCommand(ClosingWindow);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
            this.OpenProcessPickerDialog = new DelegateCommand(OpenProcessPicker);
        }

        private void OpenProcessPicker()
        {
            _dialogService.ShowDialog("MediviaProcessPicker",null,null);
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
