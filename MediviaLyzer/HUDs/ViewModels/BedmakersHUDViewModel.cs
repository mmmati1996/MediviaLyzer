using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Services.Dialogs;
using Prism.Commands;
using System.Windows;
using System.Diagnostics;

namespace MediviaLyzer.HUDs.ViewModels
{
    class BedmakersHUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Action CloseAction { get; internal set; }
        

        private Visibility _visibility = Visibility.Visible;
        private bool _isTopMost = true;
        public readonly IEventAggregator _ea;
        private double _windowOpacity = 0.5;
        private ObservableCollection<Models.CharacterModel> _listOfCharacters;

        public BedmakersHUDViewModel(IEventAggregator ea)
        {
            this._ea = ea;
            this._listOfCharacters = new ObservableCollection<Models.CharacterModel>();
            _ea.GetEvent<Events.ListOfBedmakers>().Subscribe(ListOfCharacters_Subscribe);
            _ea.GetEvent<Events.IsBedmakerEnabled>().Subscribe(BedmakersStatus_Subscribe);
            _ea.GetEvent<Events.IsWindowVisible>().Subscribe(IsWindowVisible_Subscribe);
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
        public bool IsTopMost
        {
            get { return _isTopMost; }
            set
            {
                _isTopMost = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Models.CharacterModel> ListOfCharacters
        {
            get { return _listOfCharacters; }
            set
            {
                _listOfCharacters = value;
                NotifyPropertyChanged();
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
        private void IsWindowVisible_Subscribe(bool status)
        {
            if (status == true)
            {
                if(!IsTopMost)
                {
                    _ea.GetEvent<Events.ActivateWindow>().Publish();
                    IsTopMost = true;
                }
                //_ea.GetEvent<Events.ActivateWindow>().Publish();
                //IsTopMost = true;
                //Visibility = Visibility.Visible;
            }
            else
            {
                IsTopMost = false;
                //Visibility = Visibility.Collapsed;
            }
        }
        private void ListOfCharacters_Subscribe(ObservableCollection<Models.CharacterModel> list)
        {
            this.ListOfCharacters = list;
        }
        private void BedmakersStatus_Subscribe(bool status)
        {
            if (status == false)
                CloseWindow();
        }
        private void CloseWindow()
        {
            CloseAction();
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
