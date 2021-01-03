using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Windows.Interop;

namespace MediviaLyzer.Others
{
    class WindowFocusWatcher : IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        private Models.ClientInjector Client;
        private bool _isWindowVisible;
        private readonly IEventAggregator _ea;
        private bool _isRunning = true;

        private Models.HotkeyModel Hotkey = new Models.HotkeyModel();

        private IntPtr _hookID = IntPtr.Zero;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private LowLevelKeyboardProc _proc;


        public WindowFocusWatcher(IEventAggregator ea, Models.ClientInjector client)
        {
            this._ea = ea;
            this.Client = client;
            _proc = HookCallback;
            _hookID = SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, _proc, IntPtr.Zero, 0);
        }
        private void OnAddHotkey(Key key)
        {
            var isModifier = HotkeyManager.KeyModifierList.Where(x => x.Key == key).FirstOrDefault();
            if (isModifier != null)
            {
                if (!Hotkey.Modifiers.Contains(isModifier))
                    Hotkey.Modifiers.Add(isModifier);
            }
            else
                if (!Hotkey.Keys.Contains(key))
                    Hotkey.Keys.Add(key);
            _ea.GetEvent<Events.HotkeyFired>().Publish(Hotkey);
        }
        private void OnRemoveHotkey(Key key)
        {
            var isModifier = HotkeyManager.KeyModifierList.Where(x => x.Key == key).FirstOrDefault();
            if (isModifier != null)
            {
                if (Hotkey.Modifiers.Contains(isModifier))
                    Hotkey.Modifiers.Remove(isModifier);
            }
            else
                if (Hotkey.Keys.Contains(key))
                    Hotkey.Keys.Remove(key);
            _ea.GetEvent<Events.HotkeyFired>().Publish(Hotkey);
        }
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (IsWindowVisible)
            {
                if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    var ke = KeyInterop.KeyFromVirtualKey(vkCode);
                    OnAddHotkey(ke);
                }
                else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    var ke = KeyInterop.KeyFromVirtualKey(vkCode);
                    OnRemoveHotkey(ke);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
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
                if (Natives.GetForegroundWindow() == Client.CHWND)
                    IsWindowVisible = true;
                else
                    IsWindowVisible = false;
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
            UnhookWindowsHookEx(_hookID);
        }
    }
}
