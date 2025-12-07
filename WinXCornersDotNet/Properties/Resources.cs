using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace WinXCornersDotNet.Properties
{
    /// <summary>
    /// Minimal resource helper for tray icons.
    /// </summary>
    internal static class Resources
    {
        private static Icon? _desktopIconsVisible;
        private static Icon? _desktopIconsHidden;

        public static Icon desktop_icons_visible =>
            _desktopIconsVisible ??= LoadIcon("desktop_icons_visible.ico");

        public static Icon desktop_icons_hidden =>
            _desktopIconsHidden ??= LoadIcon("desktop_icons_hidden.ico");

        private static Icon LoadIcon(string fileName)
        {
            // 1) Try embedded resource: WinXCornersDotNet.Assets.<fileName>
            var asm = Assembly.GetExecutingAssembly();
            var resourceName = "WinXCornersDotNet.Assets." + fileName;

            try
            {
                using (var stream = asm.GetManifestResourceStream(resourceName))
                {
                    if (stream is not null)
                    {
                        return new Icon(stream);
                    }
                }
            }
            catch
            {
                // ignore and fall through
            }

            // 2) Try from Assets folder next to the EXE (publish output)
            try
            {
                var baseDir = AppContext.BaseDirectory;
                var path = Path.Combine(baseDir, "Assets", fileName);
                if (File.Exists(path))
                {
                    return new Icon(path);
                }
            }
            catch
            {
                // ignore and fall through
            }

            // 3) Last-resort: generic app icon so the app still works
            return SystemIcons.Application;
        }
    }
}
