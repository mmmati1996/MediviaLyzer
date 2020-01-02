using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MediviaLyzer.Models
{
    public class CharacterModel : INotifyPropertyChanged
    {
        private string _characterName { get; set; }
        private string _timeOffline { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string CharacterName
        {
            get { return _characterName; }
            set { _characterName = value; NotifyPropertyChanged(); }
        }
        public string TimeOffline
        {
            get { return _timeOffline; }
            set { _timeOffline = value; NotifyPropertyChanged(); }
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
