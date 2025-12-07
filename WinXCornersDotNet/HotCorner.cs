namespace WinXCornersDotNet
{
    public enum HotCorner
    {
        None = 0,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public enum HotCornerActionType
    {
        None = 0,
        ShowAllWindows,
        ShowDesktop,
        StartScreenSaver,
        TurnOffMonitors,
        StartMenu,
        ActionCenter,
        HideOtherWindows,
        CustomExecutable
    }
}
