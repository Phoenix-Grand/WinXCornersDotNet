using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    public partial class MainForm : Form
    {
        private AppSettings _settings;
        private HotCornerManager _hotCornerManager = null!;
        private NotifyIcon _notifyIcon = null!;
        private ContextMenuStrip _trayMenu = null!;
        private bool _allowClose;

        // New: tray icon variants + state for desktop icons
        private Icon _trayIconDesktopVisible = null!;
        private Icon _trayIconDesktopHidden = null!;
        private bool _desktopIconsHidden;

        public MainForm()
        {
            InitializeComponent();

            _settings = SettingsService.Load();

            InitializeTrayIcon();
            PopulateActionCombos();
            ApplySettingsToUi();

            _hotCornerManager = new HotCornerManager(_settings, OnCornerTriggered);
            _hotCornerManager.Start();

            FormClosing += MainForm_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Wire events here to keep Designer cleaner
            comboTopLeft.SelectedIndexChanged += Combo_SelectedIndexChanged;
            comboTopRight.SelectedIndexChanged += Combo_SelectedIndexChanged;
            comboBottomLeft.SelectedIndexChanged += Combo_SelectedIndexChanged;
            comboBottomRight.SelectedIndexChanged += Combo_SelectedIndexChanged;

            btnBrowseTopLeft.Click += btnBrowseTopLeft_Click;
            btnBrowseTopRight.Click += btnBrowseTopRight_Click;
            btnBrowseBottomLeft.Click += btnBrowseBottomLeft_Click;
            btnBrowseBottomRight.Click += btnBrowseBottomRight_Click;

            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // Start minimized to tray
            Hide();
            ShowInTaskbar = false;
        }

        private void InitializeTrayIcon()
        {
            _trayMenu = new ContextMenuStrip();

            var settingsItem = new ToolStripMenuItem("Settings", null, (_, _) => ShowFromTray());

            var enabledItem = new ToolStripMenuItem("Enable hot corners")
            {
                CheckOnClick = true,
                Checked = _settings.Enabled
            };
            enabledItem.CheckedChanged += (_, _) =>
            {
                _settings.Enabled = enabledItem.Checked;
                SettingsService.Save(_settings);
                _hotCornerManager.UpdateSettings(_settings);
            };

            // New: toggle desktop icons menu item
            var toggleDesktopIconsItem = new ToolStripMenuItem(
                "Toggle desktop icons",
                null,
                (_, _) => ToggleDesktopIconsFromTray());

            var exitItem = new ToolStripMenuItem("Exit", null, (_, _) => ExitFromTray());

            _trayMenu.Items.Add(settingsItem);
            _trayMenu.Items.Add(new ToolStripSeparator());
            _trayMenu.Items.Add(enabledItem);
            _trayMenu.Items.Add(toggleDesktopIconsItem);
            _trayMenu.Items.Add(new ToolStripSeparator());
            _trayMenu.Items.Add(exitItem);

            // New: choose icons for visible/hidden states
            // (Replace with custom .ico files later if you want)
            _trayIconDesktopVisible = SystemIcons.Application;
            _trayIconDesktopHidden = SystemIcons.Warning;

            // New: detect current desktop icon state on startup
            _desktopIconsHidden = !NativeMethods.AreDesktopIconsVisible();

            _notifyIcon = new NotifyIcon
            {
                Visible = true,
                ContextMenuStrip = _trayMenu
            };

            UpdateTrayIconAppearance();

            _notifyIcon.DoubleClick += (_, _) => ShowFromTray();
        }

        private void UpdateTrayIconAppearance()
        {
            if (_notifyIcon == null)
                return;

            _notifyIcon.Icon = _desktopIconsHidden
                ? _trayIconDesktopHidden
                : _trayIconDesktopVisible;

            string state = _desktopIconsHidden
                ? "Desktop icons hidden"
                : "Desktop icons visible";

            // Tooltip text is limited to 63 chars in the Windows notification area
            _notifyIcon.Text = $"WinXCorners - {state}";
        }

        private void ToggleDesktopIconsFromTray()
        {
            // Ask Windows to toggle icons
            NativeMethods.ToggleDesktopIcons();

            // Re-read actual state from Explorer so we stay in sync
            _desktopIconsHidden = !NativeMethods.AreDesktopIconsVisible();

            UpdateTrayIconAppearance();
        }

        private void ShowFromTray()
        {
            ApplySettingsToUi();
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Activate();
        }

        private void ExitFromTray()
        {
            _allowClose = true;
            _hotCornerManager.Stop();
            _notifyIcon.Visible = false;
            Application.Exit();
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!_allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                ShowInTaskbar = false;
            }
        }

        private sealed class ComboItem
        {
            public string Text { get; }
            public HotCornerActionType Value { get; }

            public ComboItem(string text, HotCornerActionType value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString() => Text;
        }

        private void PopulateActionCombos()
        {
            var items = new[]
            {
                new ComboItem("None", HotCornerActionType.None),
                new ComboItem("Show all windows (Task View)", HotCornerActionType.ShowAllWindows),
                new ComboItem("Show desktop", HotCornerActionType.ShowDesktop),
                new ComboItem("Toggle desktop icons", HotCornerActionType.ToggleDesktopIcons),
                new ComboItem("Start screen saver", HotCornerActionType.StartScreenSaver),
                new ComboItem("Turn off monitors", HotCornerActionType.TurnOffMonitors),
                new ComboItem("Start menu", HotCornerActionType.StartMenu),
                new ComboItem("Action center / Quick settings", HotCornerActionType.ActionCenter),
                new ComboItem("Hide other windows", HotCornerActionType.HideOtherWindows),
                new ComboItem("Custom executable", HotCornerActionType.CustomExecutable)
            };

            void SetupCombo(ComboBox combo)
            {
                combo.DisplayMember = nameof(ComboItem.Text);
                combo.ValueMember = nameof(ComboItem.Value);
                combo.Items.Clear();

                foreach (var i in items)
                {
                    combo.Items.Add(i);
                }

                if (combo.Items.Count > 0)
                    combo.SelectedIndex = 0;
            }

            SetupCombo(comboTopLeft);
            SetupCombo(comboTopRight);
            SetupCombo(comboBottomLeft);
            SetupCombo(comboBottomRight);
        }

        private void ApplySettingsToUi()
        {
            SetComboSelection(comboTopLeft, _settings.TopLeft.Action);
            SetComboSelection(comboTopRight, _settings.TopRight.Action);
            SetComboSelection(comboBottomLeft, _settings.BottomLeft.Action);
            SetComboSelection(comboBottomRight, _settings.BottomRight.Action);

            txtTopLeftCommand.Text = _settings.TopLeft.CustomExecutablePath ?? string.Empty;
            txtTopLeftArgs.Text = _settings.TopLeft.CustomArguments ?? string.Empty;

            txtTopRightCommand.Text = _settings.TopRight.CustomExecutablePath ?? string.Empty;
            txtTopRightArgs.Text = _settings.TopRight.CustomArguments ?? string.Empty;

            txtBottomLeftCommand.Text = _settings.BottomLeft.CustomExecutablePath ?? string.Empty;
            txtBottomLeftArgs.Text = _settings.BottomLeft.CustomArguments ?? string.Empty;

            txtBottomRightCommand.Text = _settings.BottomRight.CustomExecutablePath ?? string.Empty;
            txtBottomRightArgs.Text = _settings.BottomRight.CustomArguments ?? string.Empty;

            numDelay.Value = ClampNumeric(numDelay, _settings.GlobalDelayMs);
            numCornerSize.Value = ClampNumeric(numCornerSize, _settings.CornerSizePx);

            chkEnabled.Checked = _settings.Enabled;
            chkDisableFullscreen.Checked = _settings.DisableOnFullscreen;
            chkRunOnStartup.Checked = _settings.RunOnStartup;

            UpdateCustomControlsEnabled();
        }

        private static decimal ClampNumeric(NumericUpDown control, int value)
        {
            if (value < control.Minimum) return control.Minimum;
            if (value > control.Maximum) return control.Maximum;
            return value;
        }

        private void SetComboSelection(ComboBox combo, HotCornerActionType action)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i] is ComboItem item && item.Value == action)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private static HotCornerActionType GetSelectedAction(ComboBox combo)
        {
            return combo.SelectedItem is ComboItem item
                ? item.Value
                : HotCornerActionType.None;
        }

        private void ReadSettingsFromUi()
        {
            _settings.TopLeft.Action = GetSelectedAction(comboTopLeft);
            _settings.TopRight.Action = GetSelectedAction(comboTopRight);
            _settings.BottomLeft.Action = GetSelectedAction(comboBottomLeft);
            _settings.BottomRight.Action = GetSelectedAction(comboBottomRight);

            _settings.TopLeft.CustomExecutablePath = txtTopLeftCommand.Text.Trim();
            _settings.TopLeft.CustomArguments = txtTopLeftArgs.Text.Trim();

            _settings.TopRight.CustomExecutablePath = txtTopRightCommand.Text.Trim();
            _settings.TopRight.CustomArguments = txtTopRightArgs.Text.Trim();

            _settings.BottomLeft.CustomExecutablePath = txtBottomLeftCommand.Text.Trim();
            _settings.BottomLeft.CustomArguments = txtBottomLeftArgs.Text.Trim();

            _settings.BottomRight.CustomExecutablePath = txtBottomRightCommand.Text.Trim();
            _settings.BottomRight.CustomArguments = txtBottomRightArgs.Text.Trim();

            _settings.GlobalDelayMs = (int)numDelay.Value;
            _settings.CornerSizePx = (int)numCornerSize.Value;

            _settings.Enabled = chkEnabled.Checked;
            _settings.DisableOnFullscreen = chkDisableFullscreen.Checked;
            _settings.RunOnStartup = chkRunOnStartup.Checked;
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            ReadSettingsFromUi();
            SettingsService.Save(_settings);
            _hotCornerManager.UpdateSettings(_settings);
            StartupManager.UpdateStartup(_settings.RunOnStartup);

            Hide();
            ShowInTaskbar = false;
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            ApplySettingsToUi();
            Hide();
            ShowInTaskbar = false;
        }

        private void Combo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateCustomControlsEnabled();
        }

        private void UpdateCustomControlsEnabled()
        {
            EnableCustomForCorner(comboTopLeft, txtTopLeftCommand, txtTopLeftArgs, btnBrowseTopLeft);
            EnableCustomForCorner(comboTopRight, txtTopRightCommand, txtTopRightArgs, btnBrowseTopRight);
            EnableCustomForCorner(comboBottomLeft, txtBottomLeftCommand, txtBottomLeftArgs, btnBrowseBottomLeft);
            EnableCustomForCorner(comboBottomRight, txtBottomRightCommand, txtBottomRightArgs, btnBrowseBottomRight);
        }

        private static void EnableCustomForCorner(
            ComboBox combo,
            TextBox txtCmd,
            TextBox txtArgs,
            Button btnBrowse)
        {
            bool isCustom = combo.SelectedItem is ComboItem item &&
                            item.Value == HotCornerActionType.CustomExecutable;

            txtCmd.Enabled = isCustom;
            txtArgs.Enabled = isCustom;
            btnBrowse.Enabled = isCustom;
        }

        private void btnBrowseTopLeft_Click(object? sender, EventArgs e) =>
            BrowseForExecutable(txtTopLeftCommand);

        private void btnBrowseTopRight_Click(object? sender, EventArgs e) =>
            BrowseForExecutable(txtTopRightCommand);

        private void btnBrowseBottomLeft_Click(object? sender, EventArgs e) =>
            BrowseForExecutable(txtBottomLeftCommand);

        private void btnBrowseBottomRight_Click(object? sender, EventArgs e) =>
            BrowseForExecutable(txtBottomRightCommand);

        private void BrowseForExecutable(TextBox target)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Programs (*.exe)|*.exe|All files (*.*)|*.*",
                CheckFileExists = true,
                Title = "Select executable"
            };

            if (dlg.ShowDialog(this) == DialogResult.OK)
                target.Text = dlg.FileName;
        }

        private void OnCornerTriggered(HotCorner corner)
        {
            CornerSettings cfg = corner switch
            {
                HotCorner.TopLeft => _settings.TopLeft,
                HotCorner.TopRight => _settings.TopRight,
                HotCorner.BottomLeft => _settings.BottomLeft,
                HotCorner.BottomRight => _settings.BottomRight,
                _ => _settings.TopLeft
            };

            switch (cfg.Action)
            {
                case HotCornerActionType.None:
                    break;

                case HotCornerActionType.ShowAllWindows:
                    NativeMethods.ShowAllWindowsTaskView();
                    break;

                case HotCornerActionType.ShowDesktop:
                    NativeMethods.ShowDesktop();
                    break;

                case HotCornerActionType.ToggleDesktopIcons:
                    ToggleDesktopIconsFromTray();
                    break;

                case HotCornerActionType.StartScreenSaver:
                    NativeMethods.StartScreenSaver();
                    break;

                case HotCornerActionType.TurnOffMonitors:
                    NativeMethods.TurnOffMonitors();
                    break;

                case HotCornerActionType.StartMenu:
                    NativeMethods.ShowStartMenu();
                    break;

                case HotCornerActionType.ActionCenter:
                    NativeMethods.ToggleActionCenter();
                    break;

                case HotCornerActionType.HideOtherWindows:
                    NativeMethods.HideOtherWindows();
                    break;

                case HotCornerActionType.CustomExecutable:
                    RunCustomExecutable(cfg);
                    break;
            }
        }

        private static void RunCustomExecutable(CornerSettings cfg)
        {
            if (string.IsNullOrWhiteSpace(cfg.CustomExecutablePath))
                return;

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = cfg.CustomExecutablePath,
                    Arguments = cfg.CustomArguments ?? string.Empty,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to start custom command:\n{ex.Message}",
                    "WinXCorners",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
