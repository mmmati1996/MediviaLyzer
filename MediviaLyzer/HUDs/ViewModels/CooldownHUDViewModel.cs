using MediviaLyzer.Models;
using Prism.Commands;
using Prism.Events;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace MediviaLyzer.HUDs.ViewModels
{
    class CooldownHUDViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DispatcherTimer Timer;
        private double _windowOpacity = 0.5;
        public readonly IEventAggregator _ea;
        public DelegateCommand ResetClock { get; set; }
        private Visibility _visibility = Visibility.Visible;
        private bool _isTopMost = true;
        public Func<bool> IsFocused { get; internal set; }
        public Action CloseAction { get; internal set; }
        public CooldownModel Model { get; private set; }

        public CooldownHUDViewModel(IEventAggregator ea)
        {
            this._ea = ea;
            _ea.GetEvent<Events.IsCooldownEnabled>().Subscribe(CooldownStatus_Subscribe);
            _ea.GetEvent<Events.OnCooldownUpdate>().Subscribe(OnCooldownUpdate_Subscribe);
            _ea.GetEvent<Events.OnCooldownDelete>().Subscribe(OnCooldownDelete_Subscribe);
            _ea.GetEvent<Events.IsWindowVisible>().Subscribe(IsWindowVisible_Subscribe);
            this.Timer = new DispatcherTimer();
            this.Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
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
        public double WindowOpacity
        {
            get { return _windowOpacity; }
            set
            {
                _windowOpacity = value;
                NotifyPropertyChanged();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {

        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void CloseWindow()
        {
            Dispose();
            CloseAction();
        }
        public void Dispose()
        {
            _ea.GetEvent<Events.IsWindowVisible>().Unsubscribe(IsWindowVisible_Subscribe);
            _ea.GetEvent<Events.OnCooldownUpdate>().Unsubscribe(OnCooldownUpdate_Subscribe);
            _ea.GetEvent<Events.OnCooldownDelete>().Unsubscribe(OnCooldownDelete_Subscribe);
            _ea.GetEvent<Events.IsCooldownEnabled>().Unsubscribe(CooldownStatus_Subscribe);
        }
    }
}
