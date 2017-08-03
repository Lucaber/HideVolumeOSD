using HideVolumeOSD.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HideVolumeOSD
{
	public class HideVolumeOSDLib
	{
		[DllImport("user32.dll")]
		static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		NotifyIcon ni;

		IntPtr hWndInject = IntPtr.Zero;

        VolumeKeys volumeKeys;
        System.Timers.Timer hideTimer = new System.Timers.Timer();
        bool hiding = false;

        public HideVolumeOSDLib(NotifyIcon ni)
		{
			this.ni = ni;
		}

		public void Init()
		{
			hWndInject = FindOSDWindow(true);

			int count = 0;

			while (hWndInject == IntPtr.Zero && count < 5)
			{
				keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
				keybd_event((byte)Keys.VolumeDown, 0, 0, 0);

				hWndInject = FindOSDWindow(true);

				System.Threading.Thread.Sleep(1000);
				count++;		
			}

			// final try

			hWndInject = FindOSDWindow(false);

			if (hWndInject == IntPtr.Zero)
			{
				Program.InitFailed = true;
				return;
			}
				
			if (ni != null)
			{
				if (Settings.Default.HideOSD)
					HideOSD();
				else
					ShowOSD();

				Application.ApplicationExit += Application_ApplicationExit;
			}

            volumeKeys = new VolumeKeys();
            volumeKeys.KeyPress += VolumeKeys_KeyPress;

            hideTimer.Elapsed += HideTimer_Elapsed;
        }

        
        private void VolumeKeys_KeyPress(object sender, EventArgs e)
        {
            hideTimer.Stop();
            if (Settings.Default.HideOSD == false) return;
            if (Settings.Default.ShowMs == 0) return;
            hideTimer.Interval = Settings.Default.ShowMs;

            if (hiding) ShowOSD(false);

            hideTimer.Start();
        }

        private void HideTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            hideTimer.Stop();

            if (!hiding && Settings.Default.HideOSD) HideOSD();
        }

        private IntPtr FindOSDWindow(bool bSilent)
		{
			IntPtr hwndRet = IntPtr.Zero;
			IntPtr hwndHost = IntPtr.Zero;

			int pairCount = 0;

			// search for all windows with class 'NativeHWNDHost'

			while ((hwndHost = FindWindowEx(IntPtr.Zero, hwndHost, "NativeHWNDHost", "")) != IntPtr.Zero)
			{
				// if this window has a child with class 'DirectUIHWND' it might be the volume OSD

				if (FindWindowEx(hwndHost, IntPtr.Zero, "DirectUIHWND", "") != IntPtr.Zero)
				{
					// if this is the only pair we are sure

					if (pairCount == 0)
					{
						hwndRet = hwndHost;
					}

					pairCount++;

					// if there are more pairs the criteria has failed...

					if (pairCount > 1)
					{
						MessageBox.Show("Severe error: Multiple pairs found!", "HideVolumeOSD");
						return IntPtr.Zero;
					}
				}
			}

			// if no window found yet, there is no OSD window at all

			if (hwndRet == IntPtr.Zero && !bSilent)
			{
				MessageBox.Show("Severe error: OSD window not found!", "HideVolumeOSD");
			}

			return hwndRet;
		}

		private void Application_ApplicationExit(object sender, EventArgs e)
		{
			ShowOSD();
		}

		public void HideOSD()
		{
			ShowWindow(hWndInject, 6); // SW_MINIMIZE

			if (ni != null)
				ni.Icon = Resources.IconDisabled;

            hiding = true;
            hideTimer.Stop();
        }

		public void ShowOSD(bool show=true)
		{
			ShowWindow(hWndInject, 9); // SW_RESTORE
            
            if (show)
            {
                // show window on the screen
                keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
                keybd_event((byte)Keys.VolumeDown, 0, 0, 0);
            }
			

			if (ni != null)
				ni.Icon = Resources.Icon;

            hiding = false;
        }
	}
}
