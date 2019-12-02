using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MediviaLyzer.Tabs.ViewModels
{
    class BedmakersHUDSettingsViewModel : INotifyPropertyChanged
    {
        public DelegateCommand<string> NavigateCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand AddCharacter { get; set; }

        private readonly IRegionManager RegionManager;
        private uint AlarmTime;
        private ObservableCollection<Models.CharacterModel> ListOfCharacters;
        private string _CharacterName;

        public BedmakersHUDSettingsViewModel(IRegionManager regionmanager)
        {
            this.RegionManager = regionmanager;
            this.ListOfCharacters = new ObservableCollection<Models.CharacterModel>();
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
            this.AddCharacter = new DelegateCommand(AddCharacterToList);
        }
        private void Navigate(string page)
        {
            RegionManager.RequestNavigate("PagesRegion", page);
        }
        public ObservableCollection<Models.CharacterModel> CharactersList
        {
            get { return ListOfCharacters; }
            set
            {
                if (value == ListOfCharacters)
                    return;
                ListOfCharacters = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CharactersList"));
            }
        }
        private void AddCharacterToList ()
        {
            this.ListOfCharacters.Add(new Models.CharacterModel { CharacterName = _CharacterName, TimeOffline = 0 });
        }
        public string CharacterName
        {
            get { return _CharacterName; }
            set
            {
                if (value == _CharacterName)
                    return;
                _CharacterName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CharacterName"));
            }
        }
        public uint TimeToAlarm
        {
            get { return AlarmTime; }
            set
            {
                if (value == AlarmTime)
                    return;
                AlarmTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeToAlarm"));
            }
        }
    }
}
