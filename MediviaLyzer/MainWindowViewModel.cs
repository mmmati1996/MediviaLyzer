using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;
using Prism.Services.Dialogs;
using System.Diagnostics;
using Prism.Events;
using System.Windows.Media.Effects;
using System.Runtime.CompilerServices;

namespace MediviaLyzer
{
    class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        #pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        public DelegateCommand CloseWindow { get; set; }
        private Effect _windowEffect;

        public DelegateCommand OpenProcessPickerDialog { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }
        private Others.WindowFocusWatcher _focusWatcher;

        public Effect WindowEffect
        {
            get { return _windowEffect; }
            set
            {
                this._windowEffect = value;
                NotifyPropertyChanged();
            }
        }


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
            WindowEffect = new BlurEffect();
            _dialogService.ShowDialog("MediviaProcessPicker",null,null);
            WindowEffect = null;
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
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
