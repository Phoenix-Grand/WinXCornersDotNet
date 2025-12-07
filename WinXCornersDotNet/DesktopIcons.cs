using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    internal static class DesktopIcons
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string? lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string? lpszWindow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Toggle visibility of the desktop icons ListView.
        /// </summary>
        public static void Toggle()
        {
            IntPtr desktopListView = GetDesktopListViewHandle();

            if (desktopListView == IntPtr.Zero)
            {
                MessageBox.Show(
                    "Could not find desktop icons window.",
                    "WinXCorners",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            bool visible = IsWindowVisible(desktopListView);
            ShowWindow(desktopListView, visible ? SW_HIDE : SW_SHOW);
        }

        /// <summary>
        /// Gets the handle of the SysListView32 that actually hosts the desktop icons.
        /// </summary>
        private static IntPtr GetDesktopListViewHandle()
        {
            // Step 1: Get the "Progman" window.
            IntPtr progman = FindWindow("Progman", null);

            // Step 2: In modern Windows, actual desktop icons are often on a WorkerW behind Progman.
            IntPtr shellViewWin = IntPtr.Zero;
            IntPtr workerW = IntPtr.Zero;

            // Search under Progman
            shellViewWin = FindWindowEx(progman, IntPtr.Zero, "SHELLDLL_DefView", null);
            if (shellViewWin == IntPtr.Zero)
            {
                // If not under Progman, try WorkerW chain
                workerW = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "WorkerW", null);
                while (workerW != IntPtr.Zero && shellViewWin == IntPtr.Zero)
                {
                    shellViewWin = FindWindowEx(workerW, IntPtr.Zero, "SHELLDLL_DefView", null);
                    if (shellViewWin == IntPtr.Zero)
                    {
                        workerW = FindWindowEx(IntPtr.Zero, workerW, "WorkerW", null);
                    }
                }
            }

            if (shellViewWin == IntPtr.Zero)
                return IntPtr.Zero;

            // The ListView that holds the icons
            IntPtr listView = FindWindowEx(shellViewWin, IntPtr.Zero, "SysListView32", null);
            return listView;
        }
    }
}
