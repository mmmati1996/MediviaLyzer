using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;

namespace MediviaLyzer.HUDs.ViewModels
{
    class GeneralHUDModelView : INotifyPropertyChanged
    {
        private Models.GeneralHUDModel GeneralHUDModel = new Models.GeneralHUDModel();
        private DispatcherTimer Timer;
        public double OpacityWindow { get; set; } = 0.5;
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand ResetClock { get; set; }

        public GeneralHUDModelView()
        {
            this.Timer = new DispatcherTimer();
            this.Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnlineTime += TimeSpan.FromSeconds(1);
            ExperienceGained = Experience - GeneralHUDModel.ExperienceSaveState;
            ExperiencePerHour = Math.Round(ExperienceGained / OnlineTime.TotalSeconds * 3600, 2);
            #region test
            if(OnlineTime.TotalSeconds % 10 == 0)
                Experience += 40;
            #endregion
        }

        public string HUDName
        {
            get { return GeneralHUDModel.NameOfHUD; }
            set
            {
                GeneralHUDModel.NameOfHUD = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HUDName"));
            }
        }

        public int Level
        {
            get { return GeneralHUDModel.Level; }
            set
            {
                GeneralHUDModel.Level = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Level"));
            }
        }

        public int PercentOfLevel
        {
            get { return GeneralHUDModel.PercentOfLevel; }
            set
            {
                GeneralHUDModel.PercentOfLevel = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PercentOfLevel"));
            }
        }

        public long Experience
        {
            get { return GeneralHUDModel.Experience; }
            set
            {
                GeneralHUDModel.Experience = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Experience"));
            }
        }

        public double ExperiencePerHour
        {
            get { return GeneralHUDModel.ExperiencePerHour; }
            set
            {
                GeneralHUDModel.ExperiencePerHour = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExperiencePerHour"));
            }
        }
        public long ExperienceGained
        {
            get { return GeneralHUDModel.ExperienceGained; }
            set
            {
                GeneralHUDModel.ExperienceGained = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExperienceGained"));
            }
        }

        public TimeSpan OnlineTime
        {
            get { return GeneralHUDModel.OnlineTime; }
            set
            {
                GeneralHUDModel.OnlineTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OnlineTime"));
            }
        }

        public TimeSpan TimeToLevel
        {
            get { return GeneralHUDModel.TimeToLevel; }
            set
            {
                GeneralHUDModel.TimeToLevel = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeToLevel"));
            }
        }

    }
}
