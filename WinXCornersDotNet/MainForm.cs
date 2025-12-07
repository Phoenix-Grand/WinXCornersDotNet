using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinXCornersDotNet.Properties;

namespace WinXCornersDotNet
{
    public partial class MainForm : Form
    {
        private AppSettings _settings;
        private HotCornerManager _hotCornerManager = null!;
        private NotifyIcon _notifyIcon = null!;
        private ContextMenuStrip _trayMenu = null!;
        private bool _allowClose;

        // Tray icon variants + state for desktop icons
        private Icon _trayIconDesktopVisible = null!;
        private Icon _trayIconDesktopHidden = null!;
        private bool _desktopIconsHidden;

        // Global hotkey ID for Ctrl+Alt+D
        private const int HOTKEY_ID = 1;

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

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            
            // Register hotkey after handle is created
            RegisterGlobalHotkey();
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

            // Wire hotkey change events
            chkHotkeyEnabled.CheckedChanged += HotkeySettings_Changed;
            chkHotkeyCtrl.CheckedChanged += HotkeySettings_Changed;
            chkHotkeyAlt.CheckedChanged += HotkeySettings_Changed;
            chkHotkeyShift.CheckedChanged += HotkeySettings_Changed;
            chkHotkeyWin.CheckedChanged += HotkeySettings_Changed;
            comboHotkeyKey.SelectedIndexChanged += HotkeySettings_Changed;

            PopulateHotkeyKeys();

            // Start minimized to tray
            Hide();
            ShowInTaskbar = false;
        }

        private void HotkeySettings_Changed(object? sender, EventArgs e)
        {
            // Enable/disable modifier checkboxes based on hotkey enabled state
            bool enabled = chkHotkeyEnabled.Checked;
            chkHotkeyCtrl.Enabled = enabled;
            chkHotkeyAlt.Enabled = enabled;
            chkHotkeyShift.Enabled = enabled;
            chkHotkeyWin.Enabled = enabled;
            comboHotkeyKey.Enabled = enabled;
        }

        private void PopulateHotkeyKeys()
        {
            var keys = new[]
            {
                new { Text = "A", Value = (int)Keys.A },
                new { Text = "B", Value = (int)Keys.B },
                new { Text = "C", Value = (int)Keys.C },
                new { Text = "D", Value = (int)Keys.D },
                new { Text = "E", Value = (int)Keys.E },
                new { Text = "F", Value = (int)Keys.F },
                new { Text = "G", Value = (int)Keys.G },
                new { Text = "H", Value = (int)Keys.H },
                new { Text = "I", Value = (int)Keys.I },
                new { Text = "J", Value = (int)Keys.J },
                new { Text = "K", Value = (int)Keys.K },
                new { Text = "L", Value = (int)Keys.L },
                new { Text = "M", Value = (int)Keys.M },
                new { Text = "N", Value = (int)Keys.N },
                new { Text = "O", Value = (int)Keys.O },
                new { Text = "P", Value = (int)Keys.P },
                new { Text = "Q", Value = (int)Keys.Q },
                new { Text = "R", Value = (int)Keys.R },
                new { Text = "S", Value = (int)Keys.S },
                new { Text = "T", Value = (int)Keys.T },
                new { Text = "U", Value = (int)Keys.U },
                new { Text = "V", Value = (int)Keys.V },
                new { Text = "W", Value = (int)Keys.W },
                new { Text = "X", Value = (int)Keys.X },
                new { Text = "Y", Value = (int)Keys.Y },
                new { Text = "Z", Value = (int)Keys.Z },
                new { Text = "F1", Value = (int)Keys.F1 },
                new { Text = "F2", Value = (int)Keys.F2 },
                new { Text = "F3", Value = (int)Keys.F3 },
                new { Text = "F4", Value = (int)Keys.F4 },
                new { Text = "F5", Value = (int)Keys.F5 },
                new { Text = "F6", Value = (int)Keys.F6 },
                new { Text = "F7", Value = (int)Keys.F7 },
                new { Text = "F8", Value = (int)Keys.F8 },
                new { Text = "F9", Value = (int)Keys.F9 },
                new { Text = "F10", Value = (int)Keys.F10 },
                new { Text = "F11", Value = (int)Keys.F11 },
                new { Text = "F12", Value = (int)Keys.F12 },
            };

            comboHotkeyKey.DisplayMember = "Text";
            comboHotkeyKey.ValueMember = "Value";
            comboHotkeyKey.Items.Clear();

            foreach (var k in keys)
            {
                comboHotkeyKey.Items.Add(k);
            }

            if (comboHotkeyKey.Items.Count > 0)
                comboHotkeyKey.SelectedIndex = 0;
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

            // Toggle desktop icons menu item
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

            // Use embedded .ico resources instead of SystemIcons
            _trayIconDesktopVisible = Resources.desktop_icons_visible;
            _trayIconDesktopHidden = Resources.desktop_icons_hidden;

            // Detect current desktop icon state on startup
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

        private void RegisterGlobalHotkey()
        {
            // Don't register if handle not created yet
            if (!IsHandleCreated)
                return;

            // Unregister any existing hotkey
            NativeMethods.UnregisterHotKey(Handle, HOTKEY_ID);

            // Only register if enabled
            if (!_settings.HotkeyEnabled)
                return;

            // Build modifier flags
            uint modifiers = 0;
            if (_settings.HotkeyCtrl) modifiers |= NativeMethods.MOD_CONTROL;
            if (_settings.HotkeyAlt) modifiers |= NativeMethods.MOD_ALT;
            if (_settings.HotkeyShift) modifiers |= NativeMethods.MOD_SHIFT;
            if (_settings.HotkeyWin) modifiers |= NativeMethods.MOD_WIN;

            // Register Ctrl+Alt+D to toggle desktop icons
            bool registered = NativeMethods.RegisterHotKey(
                Handle,
                HOTKEY_ID,
                modifiers,
                (uint)_settings.HotkeyKeyCode);

            if (!registered)
            {
                // Log or show warning if registration fails (key already in use)
                System.Diagnostics.Debug.WriteLine($"Failed to register global hotkey with modifiers {modifiers} and key {_settings.HotkeyKeyCode}");
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                // Ctrl+Alt+D was pressed
                ToggleDesktopIconsFromTray();
            }

            base.WndProc(ref m);
        }

        private void ExitFromTray()
        {
            _allowClose = true;
            _hotCornerManager.Stop();
            
            // Unregister the global hotkey
            NativeMethods.UnregisterHotKey(Handle, HOTKEY_ID);
            
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            _trayIconDesktopVisible?.Dispose();
            _trayIconDesktopHidden?.Dispose();
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

            // Hotkey settings
            chkHotkeyEnabled.Checked = _settings.HotkeyEnabled;
            chkHotkeyCtrl.Checked = _settings.HotkeyCtrl;
            chkHotkeyAlt.Checked = _settings.HotkeyAlt;
            chkHotkeyShift.Checked = _settings.HotkeyShift;
            chkHotkeyWin.Checked = _settings.HotkeyWin;
            SetHotkeyKeySelection(_settings.HotkeyKeyCode);

            UpdateCustomControlsEnabled();
        }

        private void SetHotkeyKeySelection(int keyCode)
        {
            for (int i = 0; i < comboHotkeyKey.Items.Count; i++)
            {
                var item = comboHotkeyKey.Items[i];
                var valueProp = item.GetType().GetProperty("Value");
                if (valueProp != null && (int)valueProp.GetValue(item)! == keyCode)
                {
                    comboHotkeyKey.SelectedIndex = i;
                    return;
                }
            }

            if (comboHotkeyKey.Items.Count > 0)
                comboHotkeyKey.SelectedIndex = 0;
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

            // Hotkey settings
            _settings.HotkeyEnabled = chkHotkeyEnabled.Checked;
            _settings.HotkeyCtrl = chkHotkeyCtrl.Checked;
            _settings.HotkeyAlt = chkHotkeyAlt.Checked;
            _settings.HotkeyShift = chkHotkeyShift.Checked;
            _settings.HotkeyWin = chkHotkeyWin.Checked;

            if (comboHotkeyKey.SelectedItem != null)
            {
                var valueProp = comboHotkeyKey.SelectedItem.GetType().GetProperty("Value");
                if (valueProp != null)
                {
                    _settings.HotkeyKeyCode = (int)valueProp.GetValue(comboHotkeyKey.SelectedItem)!;
                }
            }
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            ReadSettingsFromUi();
            SettingsService.Save(_settings);
            _hotCornerManager.UpdateSettings(_settings);
            StartupManager.UpdateStartup(_settings.RunOnStartup);

            // Re-register hotkey with new settings
            RegisterGlobalHotkey();

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
