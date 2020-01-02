using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MediviaLyzer.Tabs.Models
{
    public class CharacterModel : INotifyPropertyChanged
    {
        private string _CharacterName { get; set; }
        private string _TimeOffline { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string CharacterName
        {
            get { return _CharacterName; }
            set { _CharacterName = value; NotifyPropertyChanged(); }
        }
        public string TimeOffline
        {
            get { return _TimeOffline; }
            set { _TimeOffline = value; NotifyPropertyChanged(); }
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
