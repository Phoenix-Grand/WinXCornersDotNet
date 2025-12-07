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
    }
}
