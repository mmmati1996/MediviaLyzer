using Prism.Commands;
using Prism.Regions;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public DelegateCommand DeleteCharacter { get; set; }

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;
        private ObservableCollection<Models.CharacterModel> _listOfCharacters;
        private uint _alarmTime;
        private string _characterNameAdd;
        private readonly DispatcherTimer BedmakersTimer;
        private readonly DispatcherTimer AlarmTimer;
        private bool _isBedmakersRunning;
        private bool _isPlayAlarmChecked;
        private bool _isSendEmailChecked;
        private Models.CharacterModel SelectedCharacter;


        public BedmakersHUDSettingsViewModel(IRegionManager regionmanager, IEventAggregator ea)
        {
            this._ea = ea;
            this._regionManager = regionmanager;
            this._listOfCharacters = new ObservableCollection<Models.CharacterModel>();
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
            this.DeleteCharacter = new DelegateCommand(DeleteSelectedCharacter);
        }
        public bool IsBedmakersRunning
        {
            get { return _isBedmakersRunning; }
            set
            {
                _isBedmakersRunning = value;
                NotifyPropertyChanged();
                _ea.GetEvent<Events.IsBedmakerEnabled>().Publish(_isBedmakersRunning);
            }
        }
        public bool IsPlayAlarmChecked
        {
            get { return _isPlayAlarmChecked; }
            set
            {
                _isPlayAlarmChecked = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsSendEmailChecked
        {
            get { return _isSendEmailChecked; }
            set
            {
                _isSendEmailChecked = value;
                NotifyPropertyChanged();
            }
        }
        private void DeleteSelectedCharacter()
        {
            if (SelectedCharacterChanged != null)
                this.ListOfCharacters.Remove(SelectedCharacterChanged);
        }
        public Models.CharacterModel SelectedCharacterChanged
        {
            get { return SelectedCharacter; }
            set
            {
                SelectedCharacter = value;
                NotifyPropertyChanged();
            }
        }
        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            if (IsPlayAlarmChecked)
            {
                foreach (var character in ListOfCharacters)
                {
                    if (ConvertTextToMinutes(character.TimeOffline) >= AlarmTime)
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
            _regionManager.RequestNavigate("PagesRegion", page);
        }
        public ObservableCollection<Models.CharacterModel> ListOfCharacters
        {
            get { return _listOfCharacters; }
            set
            {
                if (value == _listOfCharacters)
                    return;
                _listOfCharacters = value;
                NotifyPropertyChanged();
            }
        }
        private void AddCharacterToList()
        {
            Others.WebScrapping scrapper = new Others.WebScrapping();
            if (scrapper.CheckIfCharacterExist(CharacterNameAdd))
                this.ListOfCharacters.Add(new Models.CharacterModel { CharacterName = CharacterNameAdd, TimeOffline = "0" });
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
            foreach(var character in ListOfCharacters)
            {
                character.TimeOffline = scrapper.GetLastLogin(character.CharacterName);
            }
        }
        public string CharacterNameAdd
        {
            get { return _characterNameAdd; }
            set
            {
                if (value == _characterNameAdd)
                    return;
                _characterNameAdd = value;
                NotifyPropertyChanged();
            }
        }
        public uint AlarmTime
        {
            get { return _alarmTime; }
            set
            {
                if (value == _alarmTime)
                    return;
                _alarmTime = value;
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
