using System;
using Prism.Services.Dialogs;
using System.ComponentModel;
using Prism.Commands;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MediviaLyzer.Extensions;
using System.Windows.Media;

namespace MediviaLyzer.Dialogs.ViewModels
{
    class EditAddCooldownViewModel : Models.CooldownModel, IDialogAware
    {
        private readonly IDialogService _dialogService;
        public DelegateCommand CloseDialogCommand { get; set; }
        public DelegateCommand<string> PickColorCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        private string _title = "Cooldown item";
        private Models.CooldownModel _selectedCooldown = null;



        public EditAddCooldownViewModel(IDialogService dialogService)
        {
            this.CloseDialogCommand = new DelegateCommand(CloseDialog);
            this.PickColorCommand = new DelegateCommand<string>(PickColorWindow);
            this._dialogService = dialogService;
        }

        public Models.CooldownModel SelectedCooldown
        {
            get { return _selectedCooldown; }
            set { 
                _selectedCooldown = value;
                NotifyPropertyChanged();
            }
        }
        private void CloseDialog()
        {
            RaiseRequestClose(null);
        }
        private void PickColorWindow(string type)
        {
            if (type == "ForegroundColorProgressBar")
            {
                var parameters = new DialogParameters
                {
                    { "color", ForegroundColor }
                };
                _dialogService.ShowDialog("ColorPicker", parameters, r => { if (r.Result == ButtonResult.OK) ForegroundColor = r.Parameters.GetValue<Color>("color"); });
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
            if(parameters != null)
            {
                this.SelectedCooldown = parameters.GetValue<Models.CooldownModel>("cooldown");
            }
            else
            {
                this.SelectedCooldown = new Models.CooldownModel();
            }
        }
    }
}
