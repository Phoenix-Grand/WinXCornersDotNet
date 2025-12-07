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
                using RegistryKey? key =
                    Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: true)
                    ?? Registry.CurrentUser.CreateSubKey(RunKeyPath, true);

                if (key == null)
                    return;

                if (enable)
                {
                    string exePath = Application.ExecutablePath;
                    // Surround with quotes in case the path has spaces
                    key.SetValue(AppName, "\"" + exePath + "\"");
                }
                else
                {
                    key.DeleteValue(AppName, throwOnMissingValue: false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to update startup setting:\n" + ex.Message,
                    "WinXCorners",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
