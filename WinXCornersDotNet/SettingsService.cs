using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    public static class SettingsService
    {
        private const string SettingsFileName = "settings.json";

        public static AppSettings Load()
        {
            try
            {
                var path = GetSettingsPath();
                if (!File.Exists(path))
                    return new AppSettings();

                var json = File.ReadAllText(path);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var settings = JsonSerializer.Deserialize<AppSettings>(json, options);
                return settings ?? new AppSettings();
            }
            catch
            {
                // On any error, just return defaults rather than crash.
                return new AppSettings();
            }
        }

        public static void Save(AppSettings settings)
        {
            try
            {
                var path = GetSettingsPath();
                var json = JsonSerializer.Serialize(
                    settings,
                    new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to save settings:\n{ex.Message}",
                    "WinXCorners",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static string GetSettingsPath()
        {
            var dir = Application.StartupPath;
            return Path.Combine(dir, SettingsFileName);
        }
    }
}
