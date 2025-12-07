using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinXCornersDotNet
{
    /// <summary>
    /// Monitors double-clicks on empty desktop space and triggers an action.
    /// </summary>
    internal class DesktopDoubleClickMonitor : IDisposable
    {
        private readonly Action _onDesktopDoubleClick;
        private IntPtr _hookHandle;
        private NativeMethods.LowLevelMouseProc? _mouseProc;
        private bool _disposed;

        public DesktopDoubleClickMonitor(Action onDesktopDoubleClick)
        {
            _onDesktopDoubleClick = onDesktopDoubleClick ?? throw new ArgumentNullException(nameof(onDesktopDoubleClick));
            System.Diagnostics.Debug.WriteLine("DesktopDoubleClickMonitor created");
        }

        public void Start()
        {
            System.Diagnostics.Debug.WriteLine("DesktopDoubleClickMonitor.Start() called");
            
            if (_hookHandle != IntPtr.Zero)
            {
                System.Diagnostics.Debug.WriteLine("Hook already installed, skipping");
                return; // Already started
            }

            // Keep a reference to prevent garbage collection
            _mouseProc = MouseHookCallback;
            System.Diagnostics.Debug.WriteLine("Mouse proc delegate created");

            // For low-level hooks (WH_MOUSE_LL), the hMod parameter should be IntPtr.Zero
            _hookHandle = NativeMethods.SetWindowsHookEx(
                NativeMethods.WH_MOUSE_LL,
                _mouseProc,
                IntPtr.Zero,  // For LL hooks, use IntPtr.Zero instead of module handle
                0);

            if (_hookHandle == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
                string msg = $"Failed to set mouse hook, error code: {error}";
                System.Diagnostics.Debug.WriteLine(msg);
                
                // Also show message box so user can see the error
                System.Windows.Forms.MessageBox.Show(
                    msg,
                    "WinXCorners - Hook Installation Failed",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                string successMsg = $"Mouse hook installed successfully, handle: {_hookHandle}";
                System.Diagnostics.Debug.WriteLine(successMsg);
                System.Windows.Forms.MessageBox.Show(successMsg, "Debug", System.Windows.Forms.MessageBoxButtons.OK);
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
                // Debug: Log all mouse events to help diagnose
                System.Diagnostics.Debug.WriteLine($"Mouse hook: nCode={nCode}, wParam={wParam}");
                
                if (nCode >= 0 && wParam == (IntPtr)NativeMethods.WM_LBUTTONDBLCLK)
                {
                    // VISIBLE TEST: Show message box to prove callback is triggered
                    System.Windows.Forms.MessageBox.Show(
                        "Double-click detected by hook!", 
                        "Debug - Hook Working",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Information);
                    
                    System.Diagnostics.Debug.WriteLine("Double-click detected!");
                    
                    var hookStruct = Marshal.PtrToStructure<NativeMethods.MSLLHOOKSTRUCT>(lParam);
                    System.Diagnostics.Debug.WriteLine($"Double-click at: X={hookStruct.pt.X}, Y={hookStruct.pt.Y}");
                    
                    if (IsDoubleClickOnEmptyDesktop(hookStruct.pt))
                    {
                        System.Diagnostics.Debug.WriteLine("Double-click on empty desktop - triggering action!");
                        try
                        {
                            _onDesktopDoubleClick?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Exception in callback: {ex}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Double-click not on empty desktop");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in MouseHookCallback: {ex}");
            }

            return NativeMethods.CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        private bool IsDoubleClickOnEmptyDesktop(NativeMethods.POINT point)
        {
            System.Diagnostics.Debug.WriteLine($"Checking if click at ({point.X}, {point.Y}) is on empty desktop");
            
            // Get the window at the click point
            IntPtr hWnd = NativeMethods.WindowFromPoint(point);
            System.Diagnostics.Debug.WriteLine($"Window handle: {hWnd}");
            
            if (hWnd == IntPtr.Zero)
            {
                System.Diagnostics.Debug.WriteLine("Window handle is zero");
                return false;
            }

            // Check if it's the desktop ListView (SysListView32)
            var className = new StringBuilder(256);
            NativeMethods.GetClassName(hWnd, className, className.Capacity);
            string classNameStr = className.ToString();
            System.Diagnostics.Debug.WriteLine($"Window class name: {classNameStr}");
            
            if (classNameStr != "SysListView32")
            {
                System.Diagnostics.Debug.WriteLine("Not SysListView32, skipping");
                return false;
            }

            System.Diagnostics.Debug.WriteLine("Found SysListView32!");

            // TEMPORARY: Trigger on ANY desktop double-click for testing
            System.Diagnostics.Debug.WriteLine("TEMPORARILY SKIPPING EMPTY SPACE CHECK - will trigger on ANY desktop double-click");
            return true;

            /* ORIGINAL CODE - commented out for testing
            // Convert screen coordinates to client coordinates for the hit test
            var clientPoint = point;
            if (!NativeMethods.ScreenToClient(hWnd, ref clientPoint))
            {
                System.Diagnostics.Debug.WriteLine("ScreenToClient failed");
                return false;
            }

            System.Diagnostics.Debug.WriteLine($"Client coordinates: ({clientPoint.X}, {clientPoint.Y})");

            // Now check if the click is on an empty area (not on an icon)
            // We need to do a hit test on the ListView
            var lvHitTest = new NativeMethods.LVHITTESTINFO
            {
                pt = clientPoint,  // Use client coordinates
                flags = 0,
                iItem = -1
            };

            // Allocate memory for the structure
            IntPtr pLvHitTest = Marshal.AllocHGlobal(Marshal.SizeOf(lvHitTest));
            try
            {
                Marshal.StructureToPtr(lvHitTest, pLvHitTest, false);
                
                // Send the hit test message
                IntPtr result = NativeMethods.SendMessage(
                    hWnd,
                    NativeMethods.LVM_HITTEST,
                    IntPtr.Zero,
                    pLvHitTest);

                System.Diagnostics.Debug.WriteLine($"LVM_HITTEST result: {result}");

                // Read back the result
                lvHitTest = Marshal.PtrToStructure<NativeMethods.LVHITTESTINFO>(pLvHitTest);
                
                System.Diagnostics.Debug.WriteLine($"Hit test result: iItem={lvHitTest.iItem}, flags=0x{lvHitTest.flags:X8}");
            }
            finally
            {
                Marshal.FreeHGlobal(pLvHitTest);
            }

            // If iItem is -1 or flags has LVHT_NOWHERE, it means empty space
            bool isEmpty = lvHitTest.iItem == -1 || (lvHitTest.flags & NativeMethods.LVHT_NOWHERE) != 0;
            System.Diagnostics.Debug.WriteLine($"Is empty desktop: {isEmpty}");
            
            return isEmpty;
            */
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Stop();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~DesktopDoubleClickMonitor()
        {
            Dispose();
        }
    }
}
