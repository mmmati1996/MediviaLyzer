using Prism.Commands;
using Prism.Events;
using System.ComponentModel;
using Prism.Regions;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace MediviaLyzer.Tabs.ViewModels
{
    class HUDPageViewModel : INotifyPropertyChanged
    {
        private bool _bedmakersHUDStatus = false;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand<string> NavigateCommand { get; set; }

        public HUDPageViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            this._regionManager = regionManager;
            this._ea = ea;
            this.NavigateCommand = new DelegateCommand<string>(Navigate);

            _ea.GetEvent<Events.IsBedmakerEnabled>().Subscribe(BedmakersStatus_Subscribe);
        } 
        public bool BedmakersHUDStatus
        {
            get { return _bedmakersHUDStatus; }
            set
            {
                _bedmakersHUDStatus = value;
                NotifyPropertyChanged();
            }
        }
        private void BedmakersStatus_Subscribe(bool status)
        {
            BedmakersHUDStatus = status;
        }
        private void Navigate(string page)
        {
            _regionManager.RequestNavigate("PagesRegion", page);
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
