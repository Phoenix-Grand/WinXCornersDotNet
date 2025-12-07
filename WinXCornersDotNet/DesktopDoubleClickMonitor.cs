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
        }

        public void Start()
        {
            if (_hookHandle != IntPtr.Zero)
                return; // Already started

            // Keep a reference to prevent garbage collection
            _mouseProc = MouseHookCallback;

            using var curProcess = System.Diagnostics.Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule;
            
            if (curModule != null)
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
            if (nCode >= 0 && wParam == (IntPtr)NativeMethods.WM_LBUTTONDBLCLK)
            {
                var hookStruct = Marshal.PtrToStructure<NativeMethods.MSLLHOOKSTRUCT>(lParam);
                
                if (IsDoubleClickOnEmptyDesktop(hookStruct.pt))
                {
                    try
                    {
                        _onDesktopDoubleClick?.Invoke();
                    }
                    catch
                    {
                        // Ignore exceptions from callback to prevent hook issues
                    }
                }
            }

            return NativeMethods.CallNextHookEx(_hookHandle, nCode, wParam, lParam);
        }

        private bool IsDoubleClickOnEmptyDesktop(NativeMethods.POINT point)
        {
            // Get the window at the click point
            IntPtr hWnd = NativeMethods.WindowFromPoint(point);
            if (hWnd == IntPtr.Zero)
                return false;

            // Check if it's the desktop ListView (SysListView32)
            var className = new StringBuilder(256);
            NativeMethods.GetClassName(hWnd, className, className.Capacity);
            
            if (className.ToString() != "SysListView32")
                return false;

            // Now check if the click is on an empty area (not on an icon)
            // We need to do a hit test on the ListView
            var lvHitTest = new NativeMethods.LVHITTESTINFO
            {
                pt = point,
                flags = 0,
                iItem = -1
            };

            // Allocate memory for the structure
            IntPtr pLvHitTest = Marshal.AllocHGlobal(Marshal.SizeOf(lvHitTest));
            try
            {
                Marshal.StructureToPtr(lvHitTest, pLvHitTest, false);
                
                // Send the hit test message
                NativeMethods.SendMessage(
                    hWnd,
                    NativeMethods.LVM_HITTEST,
                    IntPtr.Zero,
                    pLvHitTest);

                // Read back the result
                lvHitTest = Marshal.PtrToStructure<NativeMethods.LVHITTESTINFO>(pLvHitTest);
            }
            finally
            {
                Marshal.FreeHGlobal(pLvHitTest);
            }

            // If iItem is -1 or flags has LVHT_NOWHERE, it means empty space
            return lvHitTest.iItem == -1 || (lvHitTest.flags & NativeMethods.LVHT_NOWHERE) != 0;
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
