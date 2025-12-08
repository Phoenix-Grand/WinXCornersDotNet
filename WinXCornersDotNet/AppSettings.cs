namespace WinXCornersDotNet
{
    public class CornerSettings
    {
        public HotCornerActionType Action { get; set; } = HotCornerActionType.None;
        public string? CustomExecutablePath { get; set; } = string.Empty;
        public string? CustomArguments { get; set; } = string.Empty;

        /// <summary>
        /// Per-corner delay in milliseconds. 0 = use global delay.
        /// </summary>
        public int DelayMs { get; set; } = 0;
    }

    public class AppSettings
    {
        public CornerSettings TopLeft { get; set; } = new();
        public CornerSettings TopRight { get; set; } = new();
        public CornerSettings BottomLeft { get; set; } = new();
        public CornerSettings BottomRight { get; set; } = new();

        public int GlobalDelayMs { get; set; } = 300;
        public int CornerSizePx { get; set; } = 3;
        public bool Enabled { get; set; } = true;
        public bool DisableOnFullscreen { get; set; } = true;
        public bool RunOnStartup { get; set; } = false;

        // Global hotkey settings (default: Ctrl+Alt+D)
        public bool HotkeyEnabled { get; set; } = true;
        public bool HotkeyCtrl { get; set; } = true;
        public bool HotkeyAlt { get; set; } = true;
        public bool HotkeyShift { get; set; } = false;
        public bool HotkeyWin { get; set; } = false;
        public int HotkeyKeyCode { get; set; } = 68; // D key

        // Toggle hot corners hotkey settings (default: Ctrl+Alt+H)
        public bool ToggleHotkeyEnabled { get; set; } = true;
        public bool ToggleHotkeyCtrl { get; set; } = true;
        public bool ToggleHotkeyAlt { get; set; } = true;
        public bool ToggleHotkeyShift { get; set; } = false;
        public bool ToggleHotkeyWin { get; set; } = false;
        public int ToggleHotkeyKeyCode { get; set; } = 72; // H key

        // Desktop double-click toggle
        // NOTE: Currently disabled due to Windows hook compatibility issues in .NET
        // Use the hotkey (Ctrl+Alt+D) or tray menu instead
        public bool DoubleClickToggle { get; set; } = false;
    }
}
