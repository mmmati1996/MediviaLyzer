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
using System.Linq;
using Unity.Resolution;
using System.Collections.Generic;
using MediviaLyzer.Extensions;

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
                _ea.GetEvent<Events.IsCooldownEnabled>().Publish(_isCooldownRunning);
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
            _dialogService.ShowDialog("EditAddCooldown", null, r => { if (r.Result == ButtonResult.OK) FuncAddCooldown(r.Parameters.GetValue<Models.CooldownModel>("cooldown")); });
        }
        private void EditSelectedCooldown()
        {
            if (SelectedCooldown != null)
            {
                _dialogService.ShowDialog("EditAddCooldown", new DialogParameters
                {
                    { "cooldown", SelectedCooldown }
                }, r => { if (r.Result == ButtonResult.OK) FuncEditCooldown((r.Parameters.GetValue<Models.CooldownModel>("cooldown"))); });
            }
        }
        private void DeleteSelectedCooldown()
        {
            if (SelectedCooldownChanged != null)
            {
                FuncDeleteCooldown(SelectedCooldown);
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
        private void FuncAddCooldown(Models.CooldownModel model)
        {
            if (model == null || model?.Id != -1)
                return;
            model.Id = FindEmptyId();
            ListOfCooldowns.Add(model);
        }
        private void FuncEditCooldown(Models.CooldownModel model)
        {
            if (model == null || SelectedCooldown == null)
                return;
            SelectedCooldown = model;
            _ea.GetEvent<Events.OnCooldownUpdate>().Publish(SelectedCooldown);
        }
        private void FuncDeleteCooldown(Models.CooldownModel model)
        {
            if (model == null)
                return;
            model.Delete();
            ListOfCooldowns.Remove(model);
            _ea.GetEvent<Events.OnCooldownDelete>().Publish(model);
        }
        private int FindEmptyId()
        {
            for (int i = 64321; i < int.MaxValue; ++i)
            {
                if (ListOfCooldowns.Where(x => x.Id == i).FirstOrDefault() == null)
                    return i;
            }
            throw new Exception("Cannot find unused id (?)");
        }
        private void Start()
        {
            foreach (var elem in ListOfCooldowns)
            {
                elem.Hotkeys = elem.HotkeyStr.ConvertToHotkeyModel();
                _dialogService.Show("CooldownHUD", new DialogParameters
                {
                    { "cooldown", elem },
                    { "visibleAlways", IsWindowHudOptionChecked_Always }
                }, r => { });
            }
        }
        private void Stop()
        {

        }
    }

}
