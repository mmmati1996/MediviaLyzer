using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace MediviaLyzer.Tabs.ViewModels
{
    class BedmakersHUDSettingsViewModel : INotifyPropertyChanged
    {
        public DelegateCommand<string> NavigateCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand AddCharacter { get; set; }
        public DelegateCommand RefreshTest { get; set; }

        private readonly IRegionManager RegionManager;
        private uint AlarmTime;
        private ObservableCollection<Models.CharacterModel> ListOfCharacters;
        private string _CharacterNameAdd;


        public BedmakersHUDSettingsViewModel(IRegionManager regionmanager)
        {
            this.RegionManager = regionmanager;
            this.ListOfCharacters = new ObservableCollection<Models.CharacterModel>();
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
            this.AddCharacter = new DelegateCommand(AddCharacterToList);
            this.RefreshTest = new DelegateCommand(Refresh);
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
        private void AddCharacterToList()
        {
            Others.WebScrapping web = new Others.WebScrapping();
            Debug.WriteLine(web.CheckIfCharacterExist(_CharacterNameAdd));
            this.ListOfCharacters.Add(new Models.CharacterModel { CharacterName = _CharacterNameAdd, TimeOffline = 0 });
        }

        private void Refresh()
        {
            Others.WebScrapping scrapper = new Others.WebScrapping();
            foreach(var character in CharactersList)
            {
                character.TimeOffline = scrapper.GetLastLogin(character.CharacterName);
                PropertyChanged(this, new PropertyChangedEventArgs("CharactersList"));
            }
        }


        //adding character
        public string CharacterNameAdd
        {
            get { return _CharacterNameAdd; }
            set
            {
                if (value == _CharacterNameAdd)
                    return;
                _CharacterNameAdd = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CharacterNameAdd"));
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
