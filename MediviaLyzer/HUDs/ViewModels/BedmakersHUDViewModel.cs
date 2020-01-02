using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Prism.Events;

namespace MediviaLyzer.HUDs.ViewModels
{
    class BedmakersHUDViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IEventAggregator _ea;
        private double _windowOpacity = 0.5;
        private ObservableCollection<Models.CharacterModel> _listOfCharacters;

        public BedmakersHUDViewModel(IEventAggregator ea)
        {
            this._ea = ea;
            this._listOfCharacters = new ObservableCollection<Models.CharacterModel>();
            _ea.GetEvent<Tabs.Events.ListOfBedmakers>().Subscribe(ListOfCharacters_Subscribe);
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
        private void ListOfCharacters_Subscribe(ObservableCollection<Models.CharacterModel> list)
        {
            this._listOfCharacters = list;
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
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
