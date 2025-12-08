using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    public class CountdownForm : Form
    {
        private readonly Label _label;

        public CountdownForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.FromArgb(40, 40, 40); // Dark gray
            TransparencyKey = Color.Magenta; // For transparency if needed, but we want a background
            Size = new Size(100, 50);
            
            _label = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Text = "0.0s"
            };
            
            Controls.Add(_label);
        }

        protected override bool ShowWithoutActivation => true;

        public void UpdateProgress(double remainingMs)
        {
            _label.Text = $"{Math.Max(0, remainingMs / 1000.0):F1}s";
        }

        public void ShowAt(Point cursorPosition, HotCorner corner)
        {
            // Position the form near the cursor but not under it
            // We want it visible near the corner being triggered
            
            int offset = 20;
            int x = cursorPosition.X;
            int y = cursorPosition.Y;

            // Adjust position based on corner to keep it on screen and visible
            switch (corner)
            {
                case HotCorner.TopLeft:
                    x += offset;
                    y += offset;
                    break;
                case HotCorner.TopRight:
                    x -= Width + offset;
                    y += offset;
                    break;
                case HotCorner.BottomLeft:
                    x += offset;
                    y -= Height + offset;
                    break;
                case HotCorner.BottomRight:
                    x -= Width + offset;
                    y -= Height + offset;
                    break;
            }

            Location = new Point(x, y);
            
            if (!Visible)
            {
                Show();
            }
        }
    }
}
