using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediviaLyzer.Tabs.ViewModels
{
    class HUDPageViewModel : INotifyPropertyChanged
    {
        private bool IsBedmakersHUDRunning = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public HUDPageViewModel()
        {
        }

        public bool BedmakersHUDStatus
        {
            get { return IsBedmakersHUDRunning; }
            set
            {
                IsBedmakersHUDRunning = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BedmakersHUDStatus"));
            }
        }
    }
}
