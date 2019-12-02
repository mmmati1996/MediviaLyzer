using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Regions;

namespace MediviaLyzer.Tabs.ViewModels
{
    class HUDPageViewModel : INotifyPropertyChanged
    {
        private bool IsBedmakersHUDRunning = false;
        private readonly IRegionManager RegionManager;
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand<string> NavigateCommand { get; set; }

        public HUDPageViewModel(IRegionManager regionManager)
        {
            this.RegionManager = regionManager;
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
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

        private void Navigate(string page)
        {
            RegionManager.RequestNavigate("PagesRegion", page);
        }
    }
}
