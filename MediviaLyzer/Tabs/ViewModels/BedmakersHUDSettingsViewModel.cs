using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;

namespace MediviaLyzer.Tabs.ViewModels
{
    class BedmakersHUDSettingsViewModel : Models.CharacterModel, INotifyPropertyChanged
    {
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand AddCharacter { get; set; }
        public DelegateCommand BedmakersStart { get; set; }
        public DelegateCommand BedmakersStop { get; set; }

        private readonly IRegionManager RegionManager;
        private ObservableCollection<Models.CharacterModel> ListOfCharacters;
        private uint AlarmTime;
        private string _CharacterNameAdd;
        private readonly DispatcherTimer BedmakersTimer;
        private readonly DispatcherTimer AlarmTimer;
        private bool CheckIfPlayAlarm;
        private bool CheckIfSendEmail;


        public BedmakersHUDSettingsViewModel(IRegionManager regionmanager)
        {
            this.RegionManager = regionmanager;
            this.ListOfCharacters = new ObservableCollection<Models.CharacterModel>();
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
            this.AddCharacter = new DelegateCommand(AddCharacterToList);
            this.BedmakersStart = new DelegateCommand(Start);
            this.BedmakersStop = new DelegateCommand(Stop);
            this.BedmakersTimer = new DispatcherTimer();
            this.BedmakersTimer.Interval = new TimeSpan(0, 0, 15);
            this.BedmakersTimer.Tick += BedmakersTimer_Tick;
            this.AlarmTimer = new DispatcherTimer();
            this.AlarmTimer.Interval = new TimeSpan(0, 0, 1);
            this.AlarmTimer.Tick += AlarmTimer_Tick;
        }
        public bool IsPlayAlarmChecked
        {
            get { return CheckIfPlayAlarm; }
            set
            {
                CheckIfPlayAlarm = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsSendEmailChecked
        {
            get { return CheckIfSendEmail; }
            set
            {
                CheckIfSendEmail = value;
                NotifyPropertyChanged();
            }
        }
        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            if (IsPlayAlarmChecked)
            {
                foreach (var character in ListOfCharacters)
                {
                    if (ConvertTextToMinutes(character.TimeOffline) >= TimeToAlarm)
                    {
                        new Thread(() =>
                        {
                            System.Media.SystemSounds.Beep.Play();
                        }).Start();
                    }
                }
            }
        }
        private void BedmakersTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
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
                NotifyPropertyChanged();
            }
        }
        private void AddCharacterToList()
        {
            Others.WebScrapping scrapper = new Others.WebScrapping();
            if (scrapper.CheckIfCharacterExist(_CharacterNameAdd))
                this.ListOfCharacters.Add(new Models.CharacterModel { CharacterName = _CharacterNameAdd, TimeOffline = "0" });
        }
        private void Start()
        {
            Refresh();
            BedmakersTimer.Start();
            AlarmTimer.Start();
        }
        private void Stop()
        {
            BedmakersTimer.Stop();
            AlarmTimer.Stop();
        }
        private void Refresh()
        {
            Others.WebScrapping scrapper = new Others.WebScrapping();
            foreach(var character in CharactersList)
            {
                character.TimeOffline = scrapper.GetLastLogin(character.CharacterName);
            }
        }
        public string CharacterNameAdd
        {
            get { return _CharacterNameAdd; }
            set
            {
                if (value == _CharacterNameAdd)
                    return;
                _CharacterNameAdd = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }
        private int ConvertTextToMinutes(string text) //fe. 16 minutes ago, 2 hours ago == 120 minutes, 2 years ago
        {
            Match type_regx = Regex.Match(text, @"[a-z]+");
            Match amount_regx = Regex.Match(text, @"\d+");
            int amount = 0;
            int multiplier = 0;
            if (amount_regx.Success)
            {
                amount = Convert.ToInt32(amount_regx.Value);
            }
            if (type_regx.Success)
            {
                if (type_regx.Value == "minutes" || type_regx.Value == "minute")
                    multiplier = 1;
                else if (type_regx.Value == "hours" || type_regx.Value == "hour")
                    multiplier = 60;
                else if (type_regx.Value == "days" || type_regx.Value == "day")
                    multiplier = 3600;
                else
                    multiplier = 0;
            }
            return amount * multiplier;
        }
    }

}
