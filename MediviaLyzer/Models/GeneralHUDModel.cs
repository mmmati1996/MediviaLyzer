using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediviaLyzer.Models
{
    class CharacterModel_GeneralHUD : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _nameOfHUD { get; set; } //f.e. Warlocks HUD, Demons HUD, and so goes on..
        private int _level { get; set; }
        private int _percentOfLevel { get; set; }
        private long _experience { get; set; }
        private long _experienceSaveState { get; set; }
        private long _experienceLeft { get; set; }
        private long _experienceGained { get; set; }
        private double _experiencePerHour { get; set; }
        private TimeSpan _onlineTime { get; set; }
        private TimeSpan _timeToLevel { get; set; }

        public CharacterModel_GeneralHUD()
        {
            this.NameOfHUD = "Noname HUD";
            this.Level = 1;
            this.PercentOfLevel = 0;
            this.Experience = 0;
            this.ExperienceSaveState = Experience;
            this.ExperiencePerHour = 0;
            this.ExperienceGained = 0;
            this.ExperienceLeft = 100;
            this.OnlineTime = new TimeSpan();
            this.TimeToLevel = new TimeSpan();
        }

        public string NameOfHUD
        {
            get { return _nameOfHUD; }
            set
            {
                _nameOfHUD = value;
                NotifyPropertyChanged();
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                NotifyPropertyChanged();
            }
        }
        public int PercentOfLevel
        {
            get { return _percentOfLevel; }
            set
            {
                _percentOfLevel = value;
                NotifyPropertyChanged();
            }
        }
        public long Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                NotifyPropertyChanged();
            }
        }
        public long ExperienceSaveState
        {
            get { return _experienceSaveState; }
            set
            {
                _experienceSaveState = value;
                NotifyPropertyChanged();
            }
        }
        public long ExperienceLeft
        {
            get { return _experienceLeft; }
            set
            {
                _experienceLeft = value;
                NotifyPropertyChanged();
            }
        }
        public double ExperiencePerHour
        {
            get { return _experiencePerHour; }
            set
            {
                _experiencePerHour = value;
                NotifyPropertyChanged();
            }
        }
        public long ExperienceGained
        {
            get { return _experienceGained; }
            set
            {
                _experienceGained = value;
                NotifyPropertyChanged();
            }
        }
        public TimeSpan OnlineTime
        {
            get { return _onlineTime; }
            set
            {
                _onlineTime = value;
                NotifyPropertyChanged();
            }
        }
        public TimeSpan TimeToLevel
        {
            get { return _timeToLevel; }
            set
            {
                _timeToLevel = value;
                NotifyPropertyChanged();
            }
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
