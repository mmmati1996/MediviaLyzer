using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MediviaLyzer.Others
{
    class WindowFocusWatcher : IDisposable
    {
        private bool _isWindowVisible;
        private readonly IEventAggregator _ea;
        private bool _isRunning = true;

        public WindowFocusWatcher(IEventAggregator ea)
        {
            this._ea = ea;
        }
        public bool IsWindowVisible
        {
            get { return _isWindowVisible; }
            set
            {
                if (_isWindowVisible != value)
                {
                    _isWindowVisible = value;
                    _ea.GetEvent<Events.IsWindowVisible>().Publish(_isWindowVisible);
                }
            }
        }
        private void UpdateWindowStatus()
        {
            if (Others.ConnectedClientVariables._clientInfo != null)
            {
                if (Natives.GetForegroundWindow() == Others.ConnectedClientVariables._clientInfo.CHWND)
                    IsWindowVisible = true;
                else
                    IsWindowVisible = false;
            }
        }
        public void Update()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (_isRunning)
                {
                    UpdateWindowStatus();
                    Thread.Sleep(100);
                }
            }).Start();
        }
        public void Dispose()
        {
            _isRunning = false;
        }
    }
}
