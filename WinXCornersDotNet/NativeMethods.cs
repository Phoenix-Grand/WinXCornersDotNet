using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    internal static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint WM_SYSCOMMAND = 0x0112;
        private const int SC_SCREENSAVE = 0xF140;
        private const int SC_MONITORPOWER = 0xF170;
        private const int MONITOR_OFF = 2;

        private static readonly IntPtr HWND_BROADCAST = new(0xffff);

        private const byte VK_LWIN = 0x5B;
        private const byte VK_TAB = 0x09;
        private const byte VK_D = 0x44;
        private const byte VK_HOME = 0x24;
        private const byte VK_A = 0x41;

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static bool IsForegroundWindowFullscreen()
        {
            IntPtr hWnd = GetForegroundWindow();
            if (hWnd == IntPtr.Zero)
                return false;

            if (!GetWindowRect(hWnd, out var rect))
                return false;

            Rectangle screen = Screen.PrimaryScreen.Bounds;
            const int tolerance = 2;

            return rect.Left <= screen.Left + tolerance &&
                   rect.Top <= screen.Top + tolerance &&
                   rect.Right >= screen.Right - tolerance &&
                   rect.Bottom >= screen.Bottom - tolerance;
        }

        public static void ShowDesktop()
        {
            SendWinKeyCombo(VK_D);
        }

        public static void ShowAllWindowsTaskView()
        {
            SendWinKeyCombo(VK_TAB);
        }

        public static void ToggleActionCenter()
        {
            // Windows 10/11: Win + A
            SendWinKeyCombo(VK_A);
        }

        public static void ShowStartMenu()
        {
            // Press and release Win alone
            keybd_event(VK_LWIN, 0, 0, UIntPtr.Zero);
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        public static void HideOtherWindows()
        {
            // Win + Home
            SendWinKeyCombo(VK_HOME);
        }

        public static void StartScreenSaver()
        {
            SendMessage(GetDesktopWindow(), WM_SYSCOMMAND, new IntPtr(SC_SCREENSAVE), IntPtr.Zero);
        }

        public static void TurnOffMonitors()
        {
            SendMessage(
                HWND_BROADCAST,
                WM_SYSCOMMAND,
                new IntPtr(SC_MONITORPOWER),
                new IntPtr(MONITOR_OFF));
        }

        private static void SendWinKeyCombo(byte key)
        {
            // Win down
            keybd_event(VK_LWIN, 0, 0, UIntPtr.Zero);

            if (key != 0)
            {
                // key down/up
                keybd_event(key, 0, 0, UIntPtr.Zero);
                keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }

            // Win up
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        // ===== Desktop icon show/hide support =====

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string? lpClassName, string? lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(
            IntPtr parentHandle,
            IntPtr childAfter,
            string? lpszClass,
            string? lpszWindow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        /// <summary>
        /// Get the SysListView32 window that contains desktop icons.
        /// This is the child of SHELLDLL_DefView that actually holds the icons.
        /// </summary>
        private static IntPtr GetDesktopIconListView()
        {
            // First try the classic Progman -> SHELLDLL_DefView -> SysListView32
            IntPtr progman = FindWindow("Progman", null);
            if (progman != IntPtr.Zero)
            {
                IntPtr shellView = FindWindowEx(progman, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (shellView != IntPtr.Zero)
                {
                    IntPtr listView = FindWindowEx(shellView, IntPtr.Zero, "SysListView32", null);
                    if (listView != IntPtr.Zero)
                        return listView;
                }
            }

            // Fallback: Enum all top-level windows, looking for SHELLDLL_DefView -> SysListView32
            IntPtr found = IntPtr.Zero;

            EnumWindows((hWnd, _) =>
            {
                IntPtr defView = FindWindowEx(hWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (defView != IntPtr.Zero)
                {
                    IntPtr listView = FindWindowEx(defView, IntPtr.Zero, "SysListView32", null);
                    if (listView != IntPtr.Zero)
                    {
                        found = listView;
                        return false; // stop enum
                    }
                }

                return true; // continue
            }, IntPtr.Zero);

            return found;
        }

        /// <summary>
        /// Returns true if desktop icons are currently visible.
        /// </summary>
        public static bool AreDesktopIconsVisible()
        {
            IntPtr listView = GetDesktopIconListView();
            if (listView == IntPtr.Zero)
            {
                // If we can't find it, assume visible so we don't get stuck hidden.
                return true;
            }

            return IsWindowVisible(listView);
        }

        /// <summary>
        /// Show or hide the desktop icons (without affecting wallpaper).
        /// </summary>
        public static void SetDesktopIconsVisible(bool visible)
        {
            IntPtr listView = GetDesktopIconListView();
            if (listView == IntPtr.Zero)
                return;

            ShowWindow(listView, visible ? SW_SHOW : SW_HIDE);
        }

        /// <summary>
        /// Toggle desktop icon visibility.
        /// </summary>
        public static void ToggleDesktopIcons()
        {
            bool currentlyVisible = AreDesktopIconsVisible();
            SetDesktopIconsVisible(!currentlyVisible);
        }
    }
}
