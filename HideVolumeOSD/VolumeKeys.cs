using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HideVolumeOSD
{
    class VolumeKeys
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate int LowLevelKeyboardProc(int code, int wParam, ref KeyboardHookStruct lParam);

        public struct KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;

        private IntPtr hook = IntPtr.Zero;

        public event KeyPressHandler KeyPress;
        public delegate void KeyPressHandler(object sender, EventArgs e);

        private LowLevelKeyboardProc callbackCache;

        public VolumeKeys()
        {
            callbackCache = new LowLevelKeyboardProc(HookCallback);
            hook = SetHook(callbackCache);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }

        }

        public int HookCallback(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code >= 0 && wParam == WM_KEYDOWN)
            {
                Keys key = (Keys)lParam.vkCode;

                if (key == Keys.VolumeUp || key == Keys.VolumeDown)
                {
                    KeyPress?.Invoke(this, new EventArgs());
                }

            }
            return CallNextHookEx(hook, code, wParam, ref lParam);
        }

    }
}
