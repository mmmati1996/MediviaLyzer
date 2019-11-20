using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MediviaLyzer.HUDs.ViewModels
{
    class GeneralHUDModelView : INotifyPropertyChanged
    {
        private Models.GeneralHUDModel GeneralHUDModel = new Models.GeneralHUDModel();
        public double OpacityWindow { get; set; } = 0.5;

        public event PropertyChangedEventHandler PropertyChanged;

        public string HUDName
        {
            get { return GeneralHUDModel.NameOfHUD; }
            set
            {
                GeneralHUDModel.NameOfHUD = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HUDName"));
            }
        }
    }
}
