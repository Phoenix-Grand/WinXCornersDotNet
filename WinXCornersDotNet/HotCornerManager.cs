using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    public sealed class HotCornerManager : IDisposable
    {
        // Explicitly use the WinForms timer to avoid ambiguity with System.Threading.Timer
        private readonly System.Windows.Forms.Timer _timer;
        private AppSettings _settings;
        private readonly Action<HotCorner> _onCornerTriggered;

        private HotCorner _currentCorner = HotCorner.None;
        private DateTime _enteredAt = DateTime.MinValue;
        private bool _triggered;
        private bool _disposed;
        
        private readonly CountdownForm _countdownForm;

        public HotCornerManager(AppSettings settings, Action<HotCorner> onCornerTriggered)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _onCornerTriggered = onCornerTriggered ?? throw new ArgumentNullException(nameof(onCornerTriggered));

            _timer = new System.Windows.Forms.Timer
            {
                Interval = 30 // ~33 Hz polling
            };
            _timer.Tick += TimerOnTick;
            
            _countdownForm = new CountdownForm();
        }

        public void UpdateSettings(AppSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        private void TimerOnTick(object? sender, EventArgs e)
        {
            if (!_settings.Enabled)
                return;

            if (_settings.DisableOnFullscreen && NativeMethods.IsForegroundWindowFullscreen())
                return;

            if (Control.MouseButtons != MouseButtons.None)
                return;

            if (!NativeMethods.GetCursorPos(out var pt))
                return;

            if (Screen.PrimaryScreen == null)
                return;

            var screen = Screen.PrimaryScreen.Bounds;
            int size = Math.Max(1, _settings.CornerSizePx);

            HotCorner newCorner = HotCorner.None;

            if (pt.X <= screen.Left + size && pt.Y <= screen.Top + size)
                newCorner = HotCorner.TopLeft;
            else if (pt.X >= screen.Right - size && pt.Y <= screen.Top + size)
                newCorner = HotCorner.TopRight;
            else if (pt.X <= screen.Left + size && pt.Y >= screen.Bottom - size)
                newCorner = HotCorner.BottomLeft;
            else if (pt.X >= screen.Right - size && pt.Y >= screen.Bottom - size)
                newCorner = HotCorner.BottomRight;

            if (newCorner != _currentCorner)
            {
                _currentCorner = newCorner;
                _enteredAt = DateTime.Now;
                _triggered = false;
                
                // Hide countdown if we switched corners or left a corner
                if (_currentCorner == HotCorner.None)
                {
                    _countdownForm.Hide();
                }
            }

            if (_currentCorner == HotCorner.None || _triggered)
                return;

            int delayMs = GetDelayForCorner(_currentCorner);
            if (delayMs <= 0)
                delayMs = _settings.GlobalDelayMs;

            if (delayMs < 0)
                delayMs = 0;

            var elapsed = (DateTime.Now - _enteredAt).TotalMilliseconds;
            double remaining = delayMs - elapsed;

            if (elapsed >= delayMs)
            {
                _triggered = true;
                _countdownForm.Hide();
                _onCornerTriggered(_currentCorner);
            }
            else if (delayMs > 0)
            {
                // Show countdown if there is a delay AND it's enabled for this corner
                if (GetShowCountdownForCorner(_currentCorner))
                {
                    _countdownForm.ShowAt(new Point(pt.X, pt.Y), _currentCorner);
                    _countdownForm.UpdateProgress(remaining);
                }
            }
        }

        private int GetDelayForCorner(HotCorner corner) =>
            corner switch
            {
                HotCorner.TopLeft => _settings.TopLeft.DelayMs,
                HotCorner.TopRight => _settings.TopRight.DelayMs,
                HotCorner.BottomLeft => _settings.BottomLeft.DelayMs,
                HotCorner.BottomRight => _settings.BottomRight.DelayMs,
                _ => _settings.GlobalDelayMs
            };

        private bool GetShowCountdownForCorner(HotCorner corner) =>
            corner switch
            {
                HotCorner.TopLeft => _settings.TopLeft.ShowCountdown,
                HotCorner.TopRight => _settings.TopRight.ShowCountdown,
                HotCorner.BottomLeft => _settings.BottomLeft.ShowCountdown,
                HotCorner.BottomRight => _settings.BottomRight.ShowCountdown,
                _ => true
            };

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _timer.Dispose();
            _countdownForm.Dispose();
        }
    }
}
