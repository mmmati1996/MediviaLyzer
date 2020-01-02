using Prism.Commands;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace MediviaLyzer.HUDs.ViewModels
{
    class GeneralHUDViewModel : Models.CharacterModel_GeneralHUD
    {
        private DispatcherTimer Timer;
        private double _windowOpacity = 0.5;
        public DelegateCommand ResetClock { get; set; }

        public GeneralHUDViewModel()
        {
            this.Timer = new DispatcherTimer();
            this.Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        public double WindowOpacity
        {
            get { return _windowOpacity; }
            set
            {
                _windowOpacity = value;
                NotifyPropertyChanged();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            OnlineTime += TimeSpan.FromSeconds(1);
            ExperienceGained = Experience - ExperienceSaveState;
            ExperiencePerHour = Math.Round(ExperienceGained / OnlineTime.TotalSeconds * 3600, 2);
            #region test
            if(OnlineTime.TotalSeconds % 10 == 0)
                Experience += 40;
            #endregion
        }
    }
}
