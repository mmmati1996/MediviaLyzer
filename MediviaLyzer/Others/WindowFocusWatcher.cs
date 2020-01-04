using System;
using System.Collections.Generic;
using System.Text;

namespace MediviaLyzer.Others
{
    class WindowFocusWatcher : IDisposable
    {
        private bool _isWindowVisible;

        public bool IsWindowVisible
        {
            get { return _isWindowVisible; }
            set
            {
                _isWindowVisible = value;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
