using System;
using Prism.Services.Dialogs;
using System.ComponentModel;
using Prism.Commands;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MediviaLyzer.Extensions;
using System.Windows.Media;
using System.Windows.Input;
using System.Collections.Generic;

namespace MediviaLyzer.Dialogs.ViewModels
{
    class EditAddCooldownViewModel : Models.CooldownModel, IDialogAware
    {
        private readonly IDialogService _dialogService;
        public DelegateCommand<string> CloseDialogCommand { get; set; }
        public DelegateCommand<string> PickColorCommand { get; set; }
        public DelegateCommand<KeyEventArgs> HotkeyDownCommand { get; set; }
        public DelegateCommand HotkeyFinishCommand { get; set; }
        public event Action<IDialogResult> RequestClose;

        private string _title = "Cooldown item";
        private Models.CooldownModel _selectedCooldown = null;
        private List<Key> _keys = new List<Key>();



        public EditAddCooldownViewModel(IDialogService dialogService)
        {
            this.CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
            this.PickColorCommand = new DelegateCommand<string>(PickColorWindow);
            this.HotkeyDownCommand = new DelegateCommand<KeyEventArgs>(HotkeyDown);
            this.HotkeyFinishCommand = new DelegateCommand(HotkeyFinish);
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
        private void CloseDialog(string confirm)
        {
            if (confirm == "true")
            {
                DialogParameters parameters = new DialogParameters
                {
                    { "cooldown", SelectedCooldown }
                };
                RaiseRequestClose(new DialogResult(ButtonResult.OK, parameters));
            }
            else
            {
                RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
            }
        }
        private void PickColorWindow(string type)
        {
            if (type == "ForegroundColorProgressBar")
            {
                var parameters = new DialogParameters
                {
                    { "color", SelectedCooldown.ForegroundColor }
                };
                _dialogService.ShowDialog("ColorPicker", parameters, r => { if (r.Result == ButtonResult.OK) SelectedCooldown.ForegroundColor = r.Parameters.GetValue<Color>("color"); });
            }
            else if (type == "BackgroundColorProgressBar")
            {
                var parameters = new DialogParameters
                {
                    { "color", SelectedCooldown.BackgroundColor }
                };
                _dialogService.ShowDialog("ColorPicker", parameters, r => { if (r.Result == ButtonResult.OK) SelectedCooldown.BackgroundColor = r.Parameters.GetValue<Color>("color"); });
            }
            else if (type == "FontColor")
            {
                var parameters = new DialogParameters
                {
                    { "color", SelectedCooldown.FontColor }
                };
                _dialogService.ShowDialog("ColorPicker", parameters, r => { if (r.Result == ButtonResult.OK) SelectedCooldown.FontColor = r.Parameters.GetValue<Color>("color"); });
            }
        }
        private void HotkeyDown(KeyEventArgs keystroke)
        {
            if(!_keys.Contains(keystroke.Key))
                _keys.Add(keystroke.Key);
            SelectedCooldown.HotkeyStr = PrintKeyValues();
        }
        private void HotkeyFinish()
        {
            _keys.Clear();
        }
        private string PrintKeyValues()
        {
            var str = string.Empty;
            foreach (var k in _keys)
            {
                str += "{" + k.ToString() + "}+";
            }
            if(str.Length > 0)
                return str.Substring(0, str.Length - 1);
            else return str;
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
            this.SelectedCooldown = parameters.GetValue<Models.CooldownModel>("cooldown");
            if(SelectedCooldown == null)
            {
                this.SelectedCooldown = new Models.CooldownModel();
            }
        }
    }
}
