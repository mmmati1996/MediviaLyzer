using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Text;
using System.Windows;
using MediviaLyzer.Extensions;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediviaLyzer.Models
{
    public class CooldownListModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CooldownModel> _cooldowns = new ObservableCollection<CooldownModel>();
        public ObservableCollection<CooldownModel> Cooldowns
        {
            get
            {
                return _cooldowns;
            }
            set
            {
                _cooldowns = value;
                NotifyPropertyChanged();
            }
        }
        public void Save()
        {
            Others.XmlService.SerializeToXml(this);
        }
        public void Load()
        {
            Cooldowns = Others.XmlService.DeserializeXml<CooldownListModel>(nameof(CooldownListModel)).Cooldowns;
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    public class CooldownModel : Settings
    {
        private int _Id = -1;
        private string _name;
        private double _time;
        private Color _foregroundColor;
        private Color _backgroundColor;
        private Color _fontColor;
        private TextAlignmentTypes _textAlignment;
        private int _gridRow = 1;
        private int _gridColumn = 1;
        private string _hotkeyStr;
        private HotkeyModel _hotkeys;
        [XmlIgnore]
        private Visibility _visibilityText = Visibility.Collapsed;

        [XmlIgnore]
        public bool ToDelete { get; private set; } = false;

        [XmlIgnore]
        public Visibility VisibilityText
        {
            get
            {
                return _visibilityText;
            }
            protected set
            {
                _visibilityText = value;
                NotifyPropertyChanged();
            }
        }
        [XmlIgnore]
        public int GridRow
        {
            get
            {
                return _gridRow;
            }
            protected set
            {
                _gridRow = value;
                NotifyPropertyChanged();
            }
        }
        [XmlIgnore]
        public int GridColumn
        {
            get
            {
                return _gridColumn;
            }
            protected set
            {
                _gridColumn = value;
                NotifyPropertyChanged();
            }
        }
        public int Id
        {
            get { return _Id; }
            set
            {
                this._Id = value;
                NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set { 
                this._name = value;
                NotifyPropertyChanged();
            }
        }
        public HotkeyModel Hotkeys
        {
            get { return _hotkeys; }
            set
            {
                this._hotkeys = value;
                NotifyPropertyChanged();
            }
        }
        public string HotkeyStr
        {
            get { return _hotkeyStr; }
            set { this._hotkeyStr = value; NotifyPropertyChanged(); }
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
                switch (value)
                {
                    default:
                    case TextAlignmentTypes.None:
                        VisibilityText = Visibility.Collapsed;
                        GridRow = 1;
                        GridColumn = 1;
                        break;
                    case TextAlignmentTypes.Center:
                        VisibilityText = Visibility.Visible;
                        GridRow = 1;
                        GridColumn = 1;
                        break;
                    case TextAlignmentTypes.Bottom:
                        VisibilityText = Visibility.Visible;
                        GridRow = 2;
                        GridColumn = 1;
                        break;
                    case TextAlignmentTypes.Top:
                        VisibilityText = Visibility.Visible;
                        GridRow = 0;
                        GridColumn = 1;
                        break;
                    case TextAlignmentTypes.Left:
                        VisibilityText = Visibility.Visible;
                        GridRow = 1;
                        GridColumn = 0;
                        break;
                    case TextAlignmentTypes.Right:
                        VisibilityText = Visibility.Visible;
                        GridRow = 1;
                        GridColumn = 2;
                        break;
                }
                NotifyPropertyChanged();
            }
        }
        public void Delete()
        {
            this.ToDelete = true;
        }
    }

    public enum TextAlignmentTypes
    {
        None = 0,
        Center = 1,
        Top = 2,
        Bottom = 3,
        Left = 4,
        Right = 5
    }
}
