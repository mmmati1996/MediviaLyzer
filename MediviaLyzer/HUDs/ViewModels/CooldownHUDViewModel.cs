using MediviaLyzer.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace MediviaLyzer.HUDs.ViewModels
{
    class CooldownHUDViewModel : INotifyPropertyChanged, IDisposable, IDialogAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IDialogResult> RequestClose;

        public Action Show { get; internal set; }
        public Action Hide { get; internal set; }
        private DispatcherTimer Timer;
        private double _windowOpacity = 1;
        public readonly IEventAggregator _ea;
        public DelegateCommand ResetClock { get; set; }
        private Visibility _visibility = Visibility.Visible;
        private bool _isTopMost = true;
        public Func<bool> IsFocused { get; internal set; }
        public CooldownModel _model;
        private double _progressCurrent = 0;
        private bool VisibleAlways { get; set; } = true;


        public CooldownHUDViewModel(IEventAggregator ea)
        {
            this._ea = ea;
            _ea.GetEvent<Events.IsCooldownEnabled>().Subscribe(CooldownStatus_Subscribe);
            _ea.GetEvent<Events.OnCooldownUpdate>().Subscribe(OnCooldownUpdate_Subscribe);
            _ea.GetEvent<Events.OnCooldownDelete>().Subscribe(OnCooldownDelete_Subscribe);
            _ea.GetEvent<Events.IsWindowVisible>().Subscribe(IsWindowVisible_Subscribe);
            _ea.GetEvent<Events.HotkeyFired>().Subscribe(OnHotkeyFires_Subscribe);
            this.Timer = new DispatcherTimer();
            this.Timer.Interval = TimeSpan.FromSeconds(0.1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        public CooldownModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ProgressMaximum");
                NotifyPropertyChanged("ProgressCurrent");
                NotifyPropertyChanged("LabelText");
                NotifyPropertyChanged("WindowOpacity");
                NotifyPropertyChanged("IsTopMost");
                NotifyPropertyChanged("Visibility");
                NotifyPropertyChanged("IsTopMost");
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("TextAlignment");
                NotifyPropertyChanged("FontColor");
                NotifyPropertyChanged("ForegroundColor");
                NotifyPropertyChanged("BackgroundColor");
                NotifyPropertyChanged("Time");
            }
        }
        public double ProgressCurrent
        {
            get
            {
                return _progressCurrent;
            }
            set
            {
                _progressCurrent = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("LabelText");
            }
        }
        public string LabelText
        {
            get
            {
                return Model?.Name + " " + string.Format("{0:0.0}", (Model?.Time - ProgressCurrent) <= 0 ? 0 : (Model?.Time - ProgressCurrent));
            }
        }

        private void OnCooldownUpdate_Subscribe(CooldownModel obj)
        {
            if(this.Model.Id == obj.Id)
                this.Model = obj;
        }

        private void OnCooldownDelete_Subscribe(CooldownModel obj)
        {
            if (this.Model.Id == obj.Id && obj.ToDelete)
                CloseWindow();
        }

        private void OnHotkeyFires_Subscribe(HotkeyModel obj)
        {
            if(this.Model.Hotkeys.Compare(obj))
            {
                ProgressCurrent = 0;
                if(!VisibleAlways)
                    Show();
            }
        }
        public bool IsTopMost
        {
            get { return _isTopMost; }
            set
            {
                _isTopMost = value;
                NotifyPropertyChanged();
            }
        }
        private bool IsWindowFocused()
        {
            return IsFocused();
        }
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                NotifyPropertyChanged();
            }
        }
        private void CooldownStatus_Subscribe(bool status)
        {
            if (status == false)
            {
                CloseWindow();
            }
        }
        private void IsWindowVisible_Subscribe(bool status)
        {
            if (status != IsTopMost)
            {
                if (status == true)
                {
                    IsTopMost = true;
                    Visibility = Visibility.Visible;
                }
                else
                {
                    if (!IsWindowFocused())
                    {
                        IsTopMost = false;
                        Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        public double WindowOpacity
        {
            get { return _windowOpacity; }
            set
            {
                _windowOpacity = value;
                NotifyPropertyChanged();
            }
        }

        public string Title { get; set; } = "blbla";

        private void Timer_Tick(object sender, EventArgs e)
        {
            ProgressCurrent += 0.1;
            if (ProgressCurrent > Model?.Time && !VisibleAlways)
                Hide();
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void CloseWindow()
        {
            Dispose();
            RaiseRequestClose(new DialogResult(ButtonResult.Yes));
        }
        public void Dispose()
        {
            _ea.GetEvent<Events.IsWindowVisible>().Unsubscribe(IsWindowVisible_Subscribe);
            _ea.GetEvent<Events.OnCooldownUpdate>().Unsubscribe(OnCooldownUpdate_Subscribe);
            _ea.GetEvent<Events.OnCooldownDelete>().Unsubscribe(OnCooldownDelete_Subscribe);
            _ea.GetEvent<Events.IsCooldownEnabled>().Unsubscribe(CooldownStatus_Subscribe);
            _ea.GetEvent<Events.HotkeyFired>().Unsubscribe(OnHotkeyFires_Subscribe);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            this.Model = parameters.GetValue<Models.CooldownModel>("cooldown");
            if (Model == null)
            {
                this.Model = new Models.CooldownModel();
            }
            this.VisibleAlways = parameters.GetValue<bool>("visibleAlways");
        }
    }
}
