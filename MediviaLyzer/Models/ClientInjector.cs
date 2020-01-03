using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediviaLyzer.Models
{
    public class ClientInjector
    {
        private IntPtr _cHandle;
        private IntPtr _baseAddress;
        private IntPtr _cHWND;
        private int _pid;

        public IntPtr cHandle { 
            get { return _cHandle; }
            set { _cHandle = value; }
        }
        public IntPtr BaseAddress { 
            get { return _baseAddress; }
            set { _baseAddress = value; }
        }
        public IntPtr CHWND { 
            get { return _cHWND; } 
            set { _cHWND = value; }
        }
        public int Pid { 
            get { return _pid; } 
            set { _pid = value; }
        }
        public ClientInjector(Process proc)
        {
            this._cHandle = proc.Handle;
            this._baseAddress = proc.MainModule.BaseAddress;
            this._cHWND = proc.MainWindowHandle;
            this._pid = proc.Id;
        }
    }
}
