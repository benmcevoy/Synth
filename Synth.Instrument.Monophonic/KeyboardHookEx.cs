using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Synth.Instrument.Monophonic
{
    class KeyEventArgs
    {
        public KeyEventArgs(Key key) => Key = key;
        public Key Key { get; set; }
    }

    /// <summary>
    /// Global! key board hook, so will listen to keypresses even if the app doesn't have focus
    /// </summary>
    /// <remarks>
    /// https://web.archive.org/web/20190828074433/https://blogs.msdn.microsoft.com/toub/2006/05/03/low-level-keyboard-hook-in-c/
    /// </remarks>
    class KeyboardHookEx : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc;
        private static IntPtr _hookID = IntPtr.Zero;

        public EventHandler<KeyEventArgs> KeyDown;
        public EventHandler<KeyEventArgs> KeyUp;

        private int _currentKey;

        public KeyboardHookEx()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule curModule = curProcess.MainModule;
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                // skip key repeats
                if (_currentKey == vkCode) return CallNextHookEx(_hookID, nCode, wParam, lParam); ;

                _currentKey = vkCode;

                KeyDown?.Invoke(this, new KeyEventArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                _currentKey = 0;

                KeyUp?.Invoke(this, new KeyEventArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
