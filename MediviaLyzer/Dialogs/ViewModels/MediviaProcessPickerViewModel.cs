using System;
using Prism.Services.Dialogs;
using System.ComponentModel;
using Prism.Commands;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MediviaLyzer.Dialogs.ViewModels
{
    class MediviaProcessPickerViewModel : IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand CloseDialogCommand { get; set; }
        public DelegateCommand RefreshProcesses { get; set; }
        public event Action<IDialogResult> RequestClose;

        private string _title = "Medivia process picker";
        private ObservableCollection<Models.ClientInjector> _processes;

        public MediviaProcessPickerViewModel()
        {
            this._processes = new ObservableCollection<Models.ClientInjector>();
            this.CloseDialogCommand = new DelegateCommand(CloseDialog);
            this.RefreshProcesses = new DelegateCommand(RefreshProccessList);
        }

        public ObservableCollection<Models.ClientInjector> Processes
        {
            get { return _processes; }
            set
            {
                _processes = value;
                NotifyPropertyChanged();
            }
        }
        private void RefreshProccessList()
        {
            Processes.Clear();
            foreach (Process p in Process.GetProcessesByName("Medivia_OGL"))
            {
                Processes.Add(new Models.ClientInjector(p));
            }
            foreach (Process p in Process.GetProcessesByName("Medivia_D3D"))
            {
                Processes.Add(new Models.ClientInjector(p));
            }
        }
        private void CloseDialog()
        {
            RaiseRequestClose(null);
        }
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }
        public bool CanCloseDialog()
        {
            return true;
        }
        public void OnDialogClosed() {}
        public void OnDialogOpened(IDialogParameters parameters){}
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
