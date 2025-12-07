using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinXCornersDotNet
{
    /// <summary>
    /// SIMPLIFIED VERSION: Monitors double-clicks on desktop and triggers an action.
    /// This version triggers on ANY double-click on SysListView32 for testing.
    /// </summary>
    internal class DesktopDoubleClickMonitor_Simple : IDisposable
    {
        private readonly Action _onDesktopDoubleClick;
        private IntPtr _hookHandle;
        private NativeMethods.LowLevelMouseProc? _mouseProc;
        private bool _disposed;

        public DesktopDoubleClickMonitor_Simple(Action onDesktopDoubleClick)
        {
            _onDesktopDoubleClick = onDesktopDoubleClick ?? throw new ArgumentNullException(nameof(onDesktopDoubleClick));
        }

        public void Start()
        {
            if (_hookHandle != IntPtr.Zero)
                return; // Already started

            // Keep a reference to prevent garbage collection
            _mouseProc = MouseHookCallback;

            using var curProcess = System.Diagnostics.Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;
            
            if (curModule?.ModuleName != null)
            {
                _hookHandle = NativeMethods.SetWindowsHookEx(
                    NativeMethods.WH_MOUSE_LL,
                    _mouseProc,
                    NativeMethods.GetModuleHandle(curModule.ModuleName),
                    0);
            }
        }

        public void Stop()
        {
            if (_hookHandle != IntPtr.Zero)
            {
                NativeMethods.UnhookWindowsHookEx(_hookHandle);
                _hookHandle = IntPtr.Zero;
            }
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (nCode >= 0 && wParam == (IntPtr)NativeMethods.WM_LBUTTONDBLCLK)
                {
                    var hookStruct = Marshal.PtrToStructure<NativeMethods.MSLLHOOKSTRUCT>(lParam);
                    
                    // Get the window at the click point
                    IntPtr hWnd = NativeMethods.WindowFromPoint(hookStruct.pt);
                    if (h Wnd != IntPtr.Zero)
                    {
                        // Check if it's the desktop ListView (SysListView32)
                        var className = new StringBuilder(256);
                        NativeMethods.GetClassName(hWnd, className, className.Capacity);
                        
                        // SIMPLIFIED: Just check if we're on the desktop, don't check empty space
                        if (className.ToString() == "SysListView32")
                        {
                            System.Diagnostics.Debug.WriteLine($"SIMPLE: Double-click on desktop at ({hookStruct.pt.X}, {hookStruct.pt.Y}) - triggering!");
                            
                            try
                            {
                                _onDesktopDoubleClick?.Invoke();
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Exception in callback: {ex}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in MouseHookCallback: {ex}");
            }

            return NativeMethods.CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Stop();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~DesktopDoubleClickMonitor_Simple()
        {
            Dispose();
        }
    }
}
