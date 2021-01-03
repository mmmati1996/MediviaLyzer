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
        public DelegateCommand<string> CloseDialogCommand { get; set; }
        public DelegateCommand RefreshProcesses { get; set; }
        public event Action<IDialogResult> RequestClose;

        private string _title = "Medivia process picker";
        private ObservableCollection<Models.ClientInjector> _processes;
        private Models.ClientInjector _selectedClient = null;

        public MediviaProcessPickerViewModel()
        {
            this._processes = new ObservableCollection<Models.ClientInjector>();
            this.CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
            this.RefreshProcesses = new DelegateCommand(RefreshProccessList);
        }

        public Models.ClientInjector SelectedClient
        {
            get { return _selectedClient; }
            set { 
                _selectedClient = value;
                NotifyPropertyChanged();
            }
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
            foreach (Process p in Process.GetProcessesByName("Medivia"))
            {
                Processes.Add(new Models.ClientInjector(p));
            }
        }
        private void CloseDialog(string confirm)
        {
            if (confirm == "true" && SelectedClient != null)
            {
                DialogParameters parameters = new DialogParameters
                {
                    { "client", SelectedClient }
                };
                RaiseRequestClose(new DialogResult(ButtonResult.OK, parameters));
            }
            else
            {
                RaiseRequestClose(new DialogResult(ButtonResult.No));
            }
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
        public void OnDialogOpened(IDialogParameters parameters)
        {
            RefreshProccessList();
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
