using System;
using System.Threading;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var mutex = new Mutex(true, "WinXCornersDotNet_SingleInstance", out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show(
                    "WinXCorners is already running.",
                    "WinXCorners",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            Application.Run(new MainForm());
        }
    }
}
