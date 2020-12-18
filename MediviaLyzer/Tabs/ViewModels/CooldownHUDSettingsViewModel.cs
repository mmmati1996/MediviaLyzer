using Prism.Commands;
using Prism.Regions;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;
using Prism.Ioc;
using System.Diagnostics;
using Prism.Services.Dialogs;

namespace MediviaLyzer.Tabs.ViewModels
{
    class CooldownHUDSettingsViewModel : Models.CooldownModel
    {
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand AddCooldown { get; set; }
        public DelegateCommand EditCooldown { get; set; }
        public DelegateCommand DeleteCooldown { get; set; }
        public DelegateCommand CooldownStart { get; set; }
        public DelegateCommand CooldownStop { get; set; }

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;
        private readonly IContainerExtension _container;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Models.CooldownModel> _listOfCooldowns;
        private bool _isCooldownRunning;
        private bool _isWindowHudOptionChecked_Always = true;
        private bool _isWindowHudOptionChecked_Auto;
        private Models.CooldownModel SelectedCooldown;


        public CooldownHUDSettingsViewModel(IRegionManager regionmanager, IEventAggregator ea, IContainerExtension container, IDialogService dialogService)
        {
            this._ea = ea;
            this._regionManager = regionmanager;
            this._container = container;
            this._dialogService = dialogService;
            this._listOfCooldowns = new ObservableCollection<Models.CooldownModel>();
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
            this.CooldownStart = new DelegateCommand(Start);
            this.CooldownStop = new DelegateCommand(Stop);
            this.AddCooldown = new DelegateCommand(AddNewCooldown);
            this.EditCooldown = new DelegateCommand(EditSelectedCooldown);
            this.DeleteCooldown = new DelegateCommand(DeleteSelectedCooldown);
        }

        public bool IsCooldownRunning
        {
            get { return _isCooldownRunning; }
            set
            {
                _isCooldownRunning = value;
                NotifyPropertyChanged();
                _ea.GetEvent<Events.IsBedmakerEnabled>().Publish(_isCooldownRunning);
            }
        }
        public bool IsWindowHudOptionChecked_Always
        {
            get { return _isWindowHudOptionChecked_Always; }
            set
            {
                _isWindowHudOptionChecked_Always = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsWindowHudOptionChecked_Auto
        {
            get { return _isWindowHudOptionChecked_Auto; }
            set
            {
                _isWindowHudOptionChecked_Auto = value;
                NotifyPropertyChanged();
            }
        }
        private void AddNewCooldown()
        {
            throw new NotImplementedException();
        }
        private void EditSelectedCooldown()
        {
            throw new NotImplementedException();
        }
        private void DeleteSelectedCooldown()
        {
            if (SelectedCooldownChanged != null)
            {
                this.ListOfCooldowns.Remove(SelectedCooldown);
                _ea.GetEvent<Events.ListOfCooldowns>().Publish(ListOfCooldowns);
            }
        }
        public Models.CooldownModel SelectedCooldownChanged
        {
            get { return SelectedCooldown; }
            set
            {
                SelectedCooldown = value;
                NotifyPropertyChanged();
            }
        }
        private void Navigate(string page)
        {
            _regionManager.RequestNavigate("PagesRegion", page);
        }
        public ObservableCollection<Models.CooldownModel> ListOfCooldowns
        {
            get { return _listOfCooldowns; }
            set
            {
                if (value == _listOfCooldowns)
                    return;
                _listOfCooldowns = value;
                NotifyPropertyChanged();
            }
        }
        private void Start()
        {
            //if (_isWindowHudOptionChecked_Always)
            //{
            //    _container.Resolve<HUDs.Views.BedmakersHUD>().Show();
            //    _ea.GetEvent<Events.ListOfBedmakers>().Publish(ListOfCharacters);
            //}
        }
        private void Stop()
        {

        }
    }

}
