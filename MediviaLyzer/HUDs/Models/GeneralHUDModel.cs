using System;
using System.Collections.Generic;
using System.Text;

namespace MediviaLyzer.HUDs.Models
{
    class GeneralHUDModel
    {
        public string NameOfHUD { get; set; } //f.e. Warlocks HUD, Demons HUD, and so goes on..
        public int Level { get; set; }
        public int PercentOfLevel { get; set; }
        public long Experience { get; set; }
        public long ExperienceSaveState { get; }
        public long ExperienceLeft { get; set; }
        public long ExperienceGained { get; set; }
        public double ExperiencePerHour { get; set; }
        public TimeSpan OnlineTime { get; set; }
        public TimeSpan TimeToLevel { get; set; }

        public GeneralHUDModel()
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
    }
}
