using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;
using Prism.Services.Dialogs;
using System.Diagnostics;
using Prism.Events;

namespace MediviaLyzer
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        public DelegateCommand CloseWindow { get; set; }
        public DelegateCommand OpenProcessPickerDialog { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }
        private Others.WindowFocusWatcher _focusWatcher;

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService, IEventAggregator ea)
        {
            this._regionManager = regionManager;
            this._dialogService = dialogService;
            this.CloseWindow = new DelegateCommand(ClosingWindow);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
            this.OpenProcessPickerDialog = new DelegateCommand(OpenProcessPicker);
            this._focusWatcher = new Others.WindowFocusWatcher(ea);
            _focusWatcher.Update();
        }

        private void OpenProcessPicker()
        {
            _dialogService.ShowDialog("MediviaProcessPicker",null,null);
        }
        private void ClosingWindow()
        {
            _focusWatcher.Dispose();
            System.Windows.Application.Current.MainWindow.Close();
        }
        private void Navigate(string page)
        {
            _regionManager.RequestNavigate("PagesRegion", page);
        }


    }
}
