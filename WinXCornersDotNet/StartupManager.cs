using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WinXCornersDotNet
{
    public static class StartupManager
    {
        private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string AppName = "WinXCornersDotNet";

        public static void UpdateStartup(bool enable)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: true)
                               ?? Registry.CurrentUser.CreateSubKey(RunKeyPath, true);

                if (enable)
                {
                    string exePath = Application.ExecutablePath;
                    key.SetValue(AppName, $""{exePath}"");
                }
                else
                {
                    key.DeleteValue(AppName, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to update startup setting:\n{ex.Message}",
                    "WinXCorners",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
