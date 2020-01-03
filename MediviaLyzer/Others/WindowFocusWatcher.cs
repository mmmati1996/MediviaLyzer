using System;
using System.Collections.Generic;
using System.Text;

namespace MediviaLyzer.Others
{
    class WindowFocusWatcher
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
    }
}
