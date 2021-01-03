using MediviaLyzer.Others;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace MediviaLyzer.Models
{
    public class HotkeyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Key> _keys = new ObservableCollection<Key>();
        private ObservableCollection<ModifierKeyMap> _modifiers = new ObservableCollection<ModifierKeyMap>();
        private string _hotkeyStr;
        
        public string HotkeyStr
        {
            get
            {
                string ret = string.Empty;

                foreach (var k in Keys)
                    ret += "{" + k.ToString() + "}+";
                foreach (var k in Modifiers)
                    ret += "{" + k.Key.ToString() + "}+";
                if (ret.Length > 0)
                    return ret.Substring(0, ret.Length - 1);
                return ret;
            }
            set
            {
                this._hotkeyStr = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Key> Keys
        {
            get { return _keys; }
            set
            {
                this._keys = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<ModifierKeyMap> Modifiers
        {
            get { return _modifiers; }
            set
            {
                this._modifiers = value;
                NotifyPropertyChanged();
            }
        }
        public bool Compare(HotkeyModel other)
        {
            if (other == null)
                return false;
            foreach(var el in this.Modifiers)
            {
                if (!other.Modifiers.Contains(el))
                    return false;
            }
            foreach(var el in this.Keys)
            {
                if (!other.Keys.Contains(el))
                    return false;
            }
            return true;
        }
        public override string ToString()
        {
            return HotkeyStr;
        }
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
