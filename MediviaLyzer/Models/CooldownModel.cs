using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;
using System.Windows;
using MediviaLyzer.Extensions;

namespace MediviaLyzer.Models
{
    public class CooldownModel : Settings
    {
        private string _name;
        private string _hotkeyStr;
        private double _time;
        private Color _foregroundColor;
        private Color _backgroundColor;
        private Color _fontColor;
        private TextAlignmentTypes _textAlignment;

        public string Name
        {
            get { return _name; }
            set { 
                this._name = value;
                NotifyPropertyChanged();
            }
        }
        public string HotkeyStr
        {
            get { return _hotkeyStr; }
            set
            {
                this._hotkeyStr = value;
                NotifyPropertyChanged();
            }
        }
        public double Time
        {
            get { return _time; }
            set
            {
                this._time = value;
                NotifyPropertyChanged();
            }
        }
        public Color ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {
                this._foregroundColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                this._backgroundColor = value;
                NotifyPropertyChanged();
            }
        }
        public Color FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                this._fontColor = value;
                NotifyPropertyChanged();
            }
        }
        public TextAlignmentTypes TextAlignment
        {
            get
            {
                return _textAlignment;
            }
            set
            {
                this._textAlignment = value;
                NotifyPropertyChanged();
            }
        }
    }

    public enum TextAlignmentTypes
    {
        Center = 0,
        Top = 1,
        Bottom = 2,
        Left = 3,
        Right = 4
    }
}
