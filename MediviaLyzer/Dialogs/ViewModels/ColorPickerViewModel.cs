using System;
using Prism.Services.Dialogs;
using System.ComponentModel;
using Prism.Commands;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media;

namespace MediviaLyzer.Dialogs.ViewModels
{
    class ColorPickerViewModel : IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand<string> CloseDialogCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        private string _title = "Color picker";
        private Color _selectedColor = new Color();

        public ColorPickerViewModel()
        {
            this.CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        public System.Windows.Media.Color SelectedColor
        {
            get {
                return _selectedColor;
            }
            set { 
                _selectedColor = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("SelectedColorHex");
            }
        }
        public string SelectedColorHex
        {
            get
            {
                return _selectedColor.ToString();
            }
            set
            {
                SelectedColor = (Color)ColorConverter.ConvertFromString(value);
                NotifyPropertyChanged();
            }
        }
        private void CloseDialog(string Confirm)
        {
            if (Confirm == "true")
            {
                DialogParameters parameters = new DialogParameters
                {
                    { "color", SelectedColor }
                };
                RaiseRequestClose(new DialogResult(ButtonResult.OK, parameters));
            }
            else
            {
                RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
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
        public void OnDialogClosed() {
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            if(parameters != null)
            {
                this.SelectedColor = parameters.GetValue<Color>("color");
            }
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
