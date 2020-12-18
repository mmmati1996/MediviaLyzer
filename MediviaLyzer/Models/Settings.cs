using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MediviaLyzer.Models
{ 
    public abstract class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WindowStartupLocation _windowStartupLocation;
        private double _windowPositionLeft;
        private double _windowPositionRight;
        private double _windowWidth;
        private double _windowHeight;
        public WindowStartupLocation WindowStartupLocation
        {
            get { return _windowStartupLocation; }
            set
            {
                _windowStartupLocation = value;
                NotifyPropertyChanged();
            }
        }
        public double WindowPositionLeft
        {
            get { return _windowPositionLeft; }
            set
            {
                _windowPositionLeft = value;
                NotifyPropertyChanged();
            }
        }
        public double WindowPositionRight
        {
            get { return _windowPositionRight; }
            set
            {
                _windowPositionRight = value;
                NotifyPropertyChanged();
            }
        }
        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                NotifyPropertyChanged();
            }
        }
        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                _windowHeight = value;
                NotifyPropertyChanged();
            }
        }
        public virtual void Load() { }
        public virtual void Save() { }
        protected virtual void Clone(Settings obj) 
        { 
            this.WindowStartupLocation = obj.WindowStartupLocation;
            this.WindowPositionLeft = obj.WindowPositionLeft;
            this.WindowPositionRight = obj.WindowPositionRight;
            this.WindowWidth = obj.WindowWidth;
            this.WindowHeight = obj.WindowHeight;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
