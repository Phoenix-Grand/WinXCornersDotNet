using System.Drawing;
using System.Windows.Forms;

namespace WinXCornersDotNet
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label labelTitle;
        private Label labelHeaderCorner;
        private Label labelHeaderAction;
        private Label labelHeaderCommand;
        private Label labelHeaderArgs;
        private Label labelHeaderCommand;
        private Label labelHeaderArgs;
        private Label labelHeaderDelay;
        private Label labelHeaderCountdown;

        private Label labelTopLeft;
        private ComboBox comboTopLeft;
        private TextBox txtTopLeftCommand;
        private TextBox txtTopLeftArgs;
        private Button btnBrowseTopLeft;

        private Label labelTopRight;
        private ComboBox comboTopRight;
        private TextBox txtTopRightCommand;
        private TextBox txtTopRightArgs;
        private Button btnBrowseTopRight;

        private Label labelBottomLeft;
        private ComboBox comboBottomLeft;
        private TextBox txtBottomLeftCommand;
        private TextBox txtBottomLeftArgs;
        private Button btnBrowseBottomLeft;

        private Label labelBottomRight;
        private ComboBox comboBottomRight;
        private TextBox txtBottomRightCommand;
        private TextBox txtBottomRightArgs;
        private Button btnBrowseBottomRight;

        // Per-corner delay controls
        private NumericUpDown numTopLeftDelay;
        private NumericUpDown numTopRightDelay;
        private NumericUpDown numBottomLeftDelay;
        private NumericUpDown numBottomLeftDelay;
        private NumericUpDown numBottomRightDelay;

        // Per-corner countdown checkboxes
        private CheckBox chkTopLeftCountdown;
        private CheckBox chkTopRightCountdown;
        private CheckBox chkBottomLeftCountdown;
        private CheckBox chkBottomRightCountdown;

        private Label labelDelay;
        private NumericUpDown numDelay;
        private Label labelCornerSize;
        private NumericUpDown numCornerSize;

        private CheckBox chkEnabled;
        private CheckBox chkDisableFullscreen;
        private CheckBox chkRunOnStartup;

        private Button btnSave;
        private Button btnCancel;
        private Button btnApply;

        // Global hotkey controls
        private Label labelHotkey;
        private CheckBox chkHotkeyEnabled;
        private CheckBox chkHotkeyCtrl;
        private CheckBox chkHotkeyAlt;
        private CheckBox chkHotkeyShift;
        private CheckBox chkHotkeyWin;
        private ComboBox comboHotkeyKey;

        // Toggle hot corners hotkey controls
        private Label labelToggleHotkey;
        private CheckBox chkToggleHotkeyEnabled;
        private CheckBox chkToggleHotkeyCtrl;
        private CheckBox chkToggleHotkeyAlt;
        private CheckBox chkToggleHotkeyShift;
        private CheckBox chkToggleHotkeyWin;
        private ComboBox comboToggleHotkeyKey;

        // Version label
        private Label labelVersion;

        // Double-click toggle
        private CheckBox chkDoubleClickToggle;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            labelTitle = new Label();
            labelHeaderCorner = new Label();
            labelHeaderAction = new Label();
            labelHeaderCommand = new Label();
            labelHeaderArgs = new Label();
            labelHeaderCommand = new Label();
            labelHeaderArgs = new Label();
            labelHeaderDelay = new Label();
            labelHeaderCountdown = new Label();

            labelTopLeft = new Label();
            comboTopLeft = new ComboBox();
            txtTopLeftCommand = new TextBox();
            txtTopLeftArgs = new TextBox();
            btnBrowseTopLeft = new Button();

            labelTopRight = new Label();
            comboTopRight = new ComboBox();
            txtTopRightCommand = new TextBox();
            txtTopRightArgs = new TextBox();
            btnBrowseTopRight = new Button();

            labelBottomLeft = new Label();
            comboBottomLeft = new ComboBox();
            txtBottomLeftCommand = new TextBox();
            txtBottomLeftArgs = new TextBox();
            btnBrowseBottomLeft = new Button();

            labelBottomRight = new Label();
            comboBottomRight = new ComboBox();
            txtBottomRightCommand = new TextBox();
            txtBottomRightArgs = new TextBox();
            btnBrowseBottomRight = new Button();

            // Per-corner delays
            numTopLeftDelay = new NumericUpDown();
            numTopRightDelay = new NumericUpDown();
            numBottomLeftDelay = new NumericUpDown();
            numBottomLeftDelay = new NumericUpDown();
            numBottomRightDelay = new NumericUpDown();

            // Per-corner countdown checkboxes
            chkTopLeftCountdown = new CheckBox();
            chkTopRightCountdown = new CheckBox();
            chkBottomLeftCountdown = new CheckBox();
            chkBottomRightCountdown = new CheckBox();

            labelDelay = new Label();
            numDelay = new NumericUpDown();
            labelCornerSize = new Label();
            numCornerSize = new NumericUpDown();

            chkEnabled = new CheckBox();
            chkDisableFullscreen = new CheckBox();
            chkRunOnStartup = new CheckBox();

            btnSave = new Button();
            btnCancel = new Button();
            btnApply = new Button();

            // Hotkey controls
            labelHotkey = new Label();
            chkHotkeyEnabled = new CheckBox();
            chkHotkeyCtrl = new CheckBox();
            chkHotkeyAlt = new CheckBox();
            chkHotkeyShift = new CheckBox();
            chkHotkeyWin = new CheckBox();
            comboHotkeyKey = new ComboBox();

            // Toggle hotkey controls
            labelToggleHotkey = new Label();
            chkToggleHotkeyEnabled = new CheckBox();
            chkToggleHotkeyCtrl = new CheckBox();
            chkToggleHotkeyAlt = new CheckBox();
            chkToggleHotkeyShift = new CheckBox();
            chkToggleHotkeyWin = new CheckBox();
            comboToggleHotkeyKey = new ComboBox();

            labelVersion = new Label();

            chkDoubleClickToggle = new CheckBox();

            ((System.ComponentModel.ISupportInitialize)numDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCornerSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTopLeftDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTopRightDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBottomLeftDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBottomRightDelay).BeginInit();
            SuspendLayout();

            // labelTitle
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.Location = new Point(12, 9);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(206, 19);
            labelTitle.Text = "WinXCorners.NET – Settings";

            // Header labels
            labelHeaderCorner.AutoSize = true;
            labelHeaderCorner.Location = new Point(12, 40);
            labelHeaderCorner.Name = "labelHeaderCorner";
            labelHeaderCorner.Size = new Size(45, 15);
            labelHeaderCorner.Text = "Corner";

            labelHeaderAction.AutoSize = true;
            labelHeaderAction.Location = new Point(80, 40);
            labelHeaderAction.Name = "labelHeaderAction";
            labelHeaderAction.Size = new Size(43, 15);
            labelHeaderAction.Text = "Action";

            labelHeaderCommand.AutoSize = true;
            labelHeaderCommand.Location = new Point(250, 40);
            labelHeaderCommand.Name = "labelHeaderCommand";
            labelHeaderCommand.Size = new Size(163, 15);
            labelHeaderCommand.Text = "Custom executable (optional)";

            labelHeaderArgs.AutoSize = true;
            labelHeaderArgs.Location = new Point(550, 40);
            labelHeaderArgs.Name = "labelHeaderArgs";
            labelHeaderArgs.Size = new Size(60, 15);
            labelHeaderArgs.Text = "Arguments";

            labelHeaderDelay.AutoSize = true;
            labelHeaderDelay.Location = new Point(710, 40);
            labelHeaderDelay.Name = "labelHeaderDelay";
            labelHeaderDelay.Size = new Size(100, 15);
            labelHeaderDelay.Text = "Delay (ms, 0=default)";

            labelHeaderCountdown.AutoSize = true;
            labelHeaderCountdown.Location = new Point(790, 40);
            labelHeaderCountdown.Name = "labelHeaderCountdown";
            labelHeaderCountdown.Size = new Size(70, 15);
            labelHeaderCountdown.Text = "Show Timer";

            int row1Y = 65;
            int rowSpacing = 35;

            // Top Left row
            labelTopLeft.AutoSize = true;
            labelTopLeft.Location = new Point(12, row1Y);
            labelTopLeft.Name = "labelTopLeft";
            labelTopLeft.Size = new Size(52, 15);
            labelTopLeft.Text = "Top Left";

            comboTopLeft.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTopLeft.Location = new Point(80, row1Y - 3);
            comboTopLeft.Name = "comboTopLeft";
            comboTopLeft.Size = new Size(160, 23);

            txtTopLeftCommand.Location = new Point(250, row1Y - 3);
            txtTopLeftCommand.Name = "txtTopLeftCommand";
            txtTopLeftCommand.Size = new Size(260, 23);

            btnBrowseTopLeft.Location = new Point(516, row1Y - 4);
            btnBrowseTopLeft.Name = "btnBrowseTopLeft";
            btnBrowseTopLeft.Size = new Size(28, 25);
            btnBrowseTopLeft.Text = "...";
            btnBrowseTopLeft.UseVisualStyleBackColor = true;

            txtTopLeftArgs.Location = new Point(550, row1Y - 3);
            txtTopLeftArgs.Name = "txtTopLeftArgs";
            txtTopLeftArgs.Size = new Size(150, 23);

            numTopLeftDelay.Location = new Point(710, row1Y - 3);
            numTopLeftDelay.Name = "numTopLeftDelay";
            numTopLeftDelay.Size = new Size(70, 23);
            numTopLeftDelay.Maximum = 5000;
            numTopLeftDelay.Minimum = 0;
            numTopLeftDelay.Value = 0;

            chkTopLeftCountdown.AutoSize = true;
            chkTopLeftCountdown.Location = new Point(790, row1Y);
            chkTopLeftCountdown.Name = "chkTopLeftCountdown";
            chkTopLeftCountdown.Size = new Size(15, 14);
            chkTopLeftCountdown.UseVisualStyleBackColor = true;
            chkTopLeftCountdown.Checked = true;

            // Top Right row
            int row2Y = row1Y + rowSpacing;

            labelTopRight.AutoSize = true;
            labelTopRight.Location = new Point(12, row2Y);
            labelTopRight.Name = "labelTopRight";
            labelTopRight.Size = new Size(60, 15);
            labelTopRight.Text = "Top Right";

            comboTopRight.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTopRight.Location = new Point(80, row2Y - 3);
            comboTopRight.Name = "comboTopRight";
            comboTopRight.Size = new Size(160, 23);

            txtTopRightCommand.Location = new Point(250, row2Y - 3);
            txtTopRightCommand.Name = "txtTopRightCommand";
            txtTopRightCommand.Size = new Size(260, 23);

            btnBrowseTopRight.Location = new Point(516, row2Y - 4);
            btnBrowseTopRight.Name = "btnBrowseTopRight";
            btnBrowseTopRight.Size = new Size(28, 25);
            btnBrowseTopRight.Text = "...";
            btnBrowseTopRight.UseVisualStyleBackColor = true;

            txtTopRightArgs.Location = new Point(550, row2Y - 3);
            txtTopRightArgs.Name = "txtTopRightArgs";
            txtTopRightArgs.Size = new Size(150, 23);

            numTopRightDelay.Location = new Point(710, row2Y - 3);
            numTopRightDelay.Name = "numTopRightDelay";
            numTopRightDelay.Size = new Size(70, 23);
            numTopRightDelay.Maximum = 5000;
            numTopRightDelay.Minimum = 0;
            numTopRightDelay.Value = 0;

            chkTopRightCountdown.AutoSize = true;
            chkTopRightCountdown.Location = new Point(790, row2Y);
            chkTopRightCountdown.Name = "chkTopRightCountdown";
            chkTopRightCountdown.Size = new Size(15, 14);
            chkTopRightCountdown.UseVisualStyleBackColor = true;
            chkTopRightCountdown.Checked = true;

            // Bottom Left row
            int row3Y = row2Y + rowSpacing;

            labelBottomLeft.AutoSize = true;
            labelBottomLeft.Location = new Point(12, row3Y);
            labelBottomLeft.Name = "labelBottomLeft";
            labelBottomLeft.Size = new Size(71, 15);
            labelBottomLeft.Text = "Bottom Left";

            comboBottomLeft.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBottomLeft.Location = new Point(80, row3Y - 3);
            comboBottomLeft.Name = "comboBottomLeft";
            comboBottomLeft.Size = new Size(160, 23);

            txtBottomLeftCommand.Location = new Point(250, row3Y - 3);
            txtBottomLeftCommand.Name = "txtBottomLeftCommand";
            txtBottomLeftCommand.Size = new Size(260, 23);

            btnBrowseBottomLeft.Location = new Point(516, row3Y - 4);
            btnBrowseBottomLeft.Name = "btnBrowseBottomLeft";
            btnBrowseBottomLeft.Size = new Size(28, 25);
            btnBrowseBottomLeft.Text = "...";
            btnBrowseBottomLeft.UseVisualStyleBackColor = true;

            txtBottomLeftArgs.Location = new Point(550, row3Y - 3);
            txtBottomLeftArgs.Name = "txtBottomLeftArgs";
            txtBottomLeftArgs.Size = new Size(150, 23);

            numBottomLeftDelay.Location = new Point(710, row3Y - 3);
            numBottomLeftDelay.Name = "numBottomLeftDelay";
            numBottomLeftDelay.Size = new Size(70, 23);
            numBottomLeftDelay.Maximum = 5000;
            numBottomLeftDelay.Minimum = 0;
            numBottomLeftDelay.Value = 0;

            chkBottomLeftCountdown.AutoSize = true;
            chkBottomLeftCountdown.Location = new Point(790, row3Y);
            chkBottomLeftCountdown.Name = "chkBottomLeftCountdown";
            chkBottomLeftCountdown.Size = new Size(15, 14);
            chkBottomLeftCountdown.UseVisualStyleBackColor = true;
            chkBottomLeftCountdown.Checked = true;

            // Bottom Right row
            int row4Y = row3Y + rowSpacing;

            labelBottomRight.AutoSize = true;
            labelBottomRight.Location = new Point(12, row4Y);
            labelBottomRight.Name = "labelBottomRight";
            labelBottomRight.Size = new Size(79, 15);
            labelBottomRight.Text = "Bottom Right";

            comboBottomRight.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBottomRight.Location = new Point(80, row4Y - 3);
            comboBottomRight.Name = "comboBottomRight";
            comboBottomRight.Size = new Size(160, 23);

            txtBottomRightCommand.Location = new Point(250, row4Y - 3);
            txtBottomRightCommand.Name = "txtBottomRightCommand";
            txtBottomRightCommand.Size = new Size(260, 23);

            btnBrowseBottomRight.Location = new Point(516, row4Y - 4);
            btnBrowseBottomRight.Name = "btnBrowseBottomRight";
            btnBrowseBottomRight.Size = new Size(28, 25);
            btnBrowseBottomRight.Text = "...";
            btnBrowseBottomRight.UseVisualStyleBackColor = true;

            txtBottomRightArgs.Location = new Point(550, row4Y - 3);
            txtBottomRightArgs.Name = "txtBottomRightArgs";
            txtBottomRightArgs.Size = new Size(150, 23);

            numBottomRightDelay.Location = new Point(710, row4Y - 3);
            numBottomRightDelay.Name = "numBottomRightDelay";
            numBottomRightDelay.Size = new Size(70, 23);
            numBottomRightDelay.Maximum = 5000;
            numBottomRightDelay.Minimum = 0;
            numBottomRightDelay.Value = 0;

            chkBottomRightCountdown.AutoSize = true;
            chkBottomRightCountdown.Location = new Point(790, row4Y);
            chkBottomRightCountdown.Name = "chkBottomRightCountdown";
            chkBottomRightCountdown.Size = new Size(15, 14);
            chkBottomRightCountdown.UseVisualStyleBackColor = true;
            chkBottomRightCountdown.Checked = true;

            // Delay
            labelDelay.AutoSize = true;
            labelDelay.Location = new Point(12, 220);
            labelDelay.Name = "labelDelay";
            labelDelay.Size = new Size(68, 15);
            labelDelay.Text = "Delay (ms):";

            numDelay.Location = new Point(100, 218);
            numDelay.Maximum = 5000;
            numDelay.Minimum = 0;
            numDelay.Name = "numDelay";
            numDelay.Size = new Size(80, 23);
            numDelay.Value = 300;

            // Corner size
            labelCornerSize.AutoSize = true;
            labelCornerSize.Location = new Point(200, 220);
            labelCornerSize.Name = "labelCornerSize";
            labelCornerSize.Size = new Size(99, 15);
            labelCornerSize.Text = "Corner size (px):";

            numCornerSize.Location = new Point(310, 218);
            numCornerSize.Maximum = 50;
            numCornerSize.Minimum = 1;
            numCornerSize.Name = "numCornerSize";
            numCornerSize.Size = new Size(60, 23);
            numCornerSize.Value = 3;

            // Checkboxes
            chkEnabled.AutoSize = true;
            chkEnabled.Location = new Point(12, 250);
            chkEnabled.Name = "chkEnabled";
            chkEnabled.Size = new Size(124, 19);
            chkEnabled.Text = "Enable hot corners";
            chkEnabled.Checked = true;
            chkEnabled.UseVisualStyleBackColor = true;

            chkDisableFullscreen.AutoSize = true;
            chkDisableFullscreen.Location = new Point(200, 250);
            chkDisableFullscreen.Name = "chkDisableFullscreen";
            chkDisableFullscreen.Size = new Size(234, 19);
            chkDisableFullscreen.Text = "Disable while full-screen app is active";
            chkDisableFullscreen.Checked = true;
            chkDisableFullscreen.UseVisualStyleBackColor = true;

            chkRunOnStartup.AutoSize = true;
            chkRunOnStartup.Location = new Point(12, 275);
            chkRunOnStartup.Name = "chkRunOnStartup";
            chkRunOnStartup.Size = new Size(140, 19);
            chkRunOnStartup.Text = "Run at Windows start";
            chkRunOnStartup.UseVisualStyleBackColor = true;

            // Global hotkey configuration
            labelHotkey.AutoSize = true;
            labelHotkey.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelHotkey.Location = new Point(12, 305);
            labelHotkey.Name = "labelHotkey";
            labelHotkey.Size = new Size(200, 15);
            labelHotkey.Text = "Global Hotkey (Toggle Desktop Icons):";

            chkHotkeyEnabled.AutoSize = true;
            chkHotkeyEnabled.Checked = true;
            chkHotkeyEnabled.Location = new Point(12, 328);
            chkHotkeyEnabled.Name = "chkHotkeyEnabled";
            chkHotkeyEnabled.Size = new Size(118, 19);
            chkHotkeyEnabled.Text = "Enable hotkey";
            chkHotkeyEnabled.UseVisualStyleBackColor = true;

            chkHotkeyCtrl.AutoSize = true;
            chkHotkeyCtrl.Location = new Point(150, 328);
            chkHotkeyCtrl.Name = "chkHotkeyCtrl";
            chkHotkeyCtrl.Size = new Size(45, 19);
            chkHotkeyCtrl.Text = "Ctrl";
            chkHotkeyCtrl.UseVisualStyleBackColor = true;

            chkHotkeyAlt.AutoSize = true;
            chkHotkeyAlt.Location = new Point(205, 328);
            chkHotkeyAlt.Name = "chkHotkeyAlt";
            chkHotkeyAlt.Size = new Size(41, 19);
            chkHotkeyAlt.Text = "Alt";
            chkHotkeyAlt.UseVisualStyleBackColor = true;

            chkHotkeyShift.AutoSize = true;
            chkHotkeyShift.Location = new Point(255, 328);
            chkHotkeyShift.Name = "chkHotkeyShift";
            chkHotkeyShift.Size = new Size(51, 19);
            chkHotkeyShift.Text = "Shift";
            chkHotkeyShift.UseVisualStyleBackColor = true;

            chkHotkeyWin.AutoSize = true;
            chkHotkeyWin.Location = new Point(315, 328);
            chkHotkeyWin.Name = "chkHotkeyWin";
            chkHotkeyWin.Size = new Size(47, 19);
            chkHotkeyWin.Text = "Win";
            chkHotkeyWin.UseVisualStyleBackColor = true;

            comboHotkeyKey.DropDownStyle = ComboBoxStyle.DropDownList;
            comboHotkeyKey.Location = new Point(375, 326);
            comboHotkeyKey.Name = "comboHotkeyKey";
            comboHotkeyKey.Size = new Size(100, 23);
            comboHotkeyKey.DropDownHeight = 200;

            // Toggle hot corners hotkey configuration
            labelToggleHotkey.AutoSize = true;
            labelToggleHotkey.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelToggleHotkey.Location = new Point(12, 358);
            labelToggleHotkey.Name = "labelToggleHotkey";
            labelToggleHotkey.Size = new Size(220, 15);
            labelToggleHotkey.Text = "Global Hotkey (Toggle Hot Corners):";

            chkToggleHotkeyEnabled.AutoSize = true;
            chkToggleHotkeyEnabled.Checked = true;
            chkToggleHotkeyEnabled.Location = new Point(12, 381);
            chkToggleHotkeyEnabled.Name = "chkToggleHotkeyEnabled";
            chkToggleHotkeyEnabled.Size = new Size(118, 19);
            chkToggleHotkeyEnabled.Text = "Enable hotkey";
            chkToggleHotkeyEnabled.UseVisualStyleBackColor = true;

            chkToggleHotkeyCtrl.AutoSize = true;
            chkToggleHotkeyCtrl.Location = new Point(150, 381);
            chkToggleHotkeyCtrl.Name = "chkToggleHotkeyCtrl";
            chkToggleHotkeyCtrl.Size = new Size(45, 19);
            chkToggleHotkeyCtrl.Text = "Ctrl";
            chkToggleHotkeyCtrl.UseVisualStyleBackColor = true;

            chkToggleHotkeyAlt.AutoSize = true;
            chkToggleHotkeyAlt.Location = new Point(205, 381);
            chkToggleHotkeyAlt.Name = "chkToggleHotkeyAlt";
            chkToggleHotkeyAlt.Size = new Size(41, 19);
            chkToggleHotkeyAlt.Text = "Alt";
            chkToggleHotkeyAlt.UseVisualStyleBackColor = true;

            chkToggleHotkeyShift.AutoSize = true;
            chkToggleHotkeyShift.Location = new Point(255, 381);
            chkToggleHotkeyShift.Name = "chkToggleHotkeyShift";
            chkToggleHotkeyShift.Size = new Size(51, 19);
            chkToggleHotkeyShift.Text = "Shift";
            chkToggleHotkeyShift.UseVisualStyleBackColor = true;

            chkToggleHotkeyWin.AutoSize = true;
            chkToggleHotkeyWin.Location = new Point(315, 381);
            chkToggleHotkeyWin.Name = "chkToggleHotkeyWin";
            chkToggleHotkeyWin.Size = new Size(47, 19);
            chkToggleHotkeyWin.Text = "Win";
            chkToggleHotkeyWin.UseVisualStyleBackColor = true;

            comboToggleHotkeyKey.DropDownStyle = ComboBoxStyle.DropDownList;
            comboToggleHotkeyKey.Location = new Point(375, 379);
            comboToggleHotkeyKey.Name = "comboToggleHotkeyKey";
            comboToggleHotkeyKey.Size = new Size(100, 23);
            comboToggleHotkeyKey.DropDownHeight = 200;

            // Version label
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(12, 455);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(80, 15);
            labelVersion.Text = "Version 1.3.4";

            // Double-click toggle
            chkDoubleClickToggle.AutoSize = true;
            chkDoubleClickToggle.Checked = true;
            chkDoubleClickToggle.Location = new Point(200, 275);
            chkDoubleClickToggle.Name = "chkDoubleClickToggle";
            chkDoubleClickToggle.Size = new Size(252, 19);
            chkDoubleClickToggle.Text = "Double-click empty desktop to toggle icons (disabled for now due to compatibility issues)";
            chkDoubleClickToggle.UseVisualStyleBackColor = true;

            // Buttons (moved down to avoid overlap with hotkey controls)
            btnApply.Location = new Point(390, 420);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(90, 27);
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;

            btnSave.Location = new Point(490, 420);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 27);
            btnSave.Text = "Save && Hide";
            btnSave.UseVisualStyleBackColor = true;

            btnCancel.Location = new Point(600, 420);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 27);
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;

            // MainForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 480);
            Controls.Add(labelTitle);
            Controls.Add(labelHeaderCorner);
            Controls.Add(labelHeaderAction);
            Controls.Add(labelHeaderCommand);
            Controls.Add(labelHeaderArgs);
            Controls.Add(labelHeaderDelay);
            Controls.Add(labelHeaderCountdown);
            Controls.Add(labelTitle);
            Controls.Add(chkTopLeftCountdown);
            Controls.Add(chkTopRightCountdown);
            Controls.Add(chkBottomLeftCountdown);
            Controls.Add(chkBottomRightCountdown);
            Controls.Add(labelTopLeft);
            Controls.Add(comboTopLeft);
            Controls.Add(txtTopLeftCommand);
            Controls.Add(btnBrowseTopLeft);
            Controls.Add(txtTopLeftArgs);
            Controls.Add(numTopLeftDelay);

            Controls.Add(labelTopRight);
            Controls.Add(comboTopRight);
            Controls.Add(txtTopRightCommand);
            Controls.Add(btnBrowseTopRight);
            Controls.Add(txtTopRightArgs);
            Controls.Add(numTopRightDelay);

            Controls.Add(labelBottomLeft);
            Controls.Add(comboBottomLeft);
            Controls.Add(txtBottomLeftCommand);
            Controls.Add(btnBrowseBottomLeft);
            Controls.Add(txtBottomLeftArgs);
            Controls.Add(numBottomLeftDelay);

            Controls.Add(labelBottomRight);
            Controls.Add(comboBottomRight);
            Controls.Add(txtBottomRightCommand);
            Controls.Add(btnBrowseBottomRight);
            Controls.Add(txtBottomRightArgs);
            Controls.Add(numBottomRightDelay);

            Controls.Add(labelDelay);
            Controls.Add(numDelay);
            Controls.Add(labelCornerSize);
            Controls.Add(numCornerSize);

            Controls.Add(chkEnabled);
            Controls.Add(chkDisableFullscreen);
            Controls.Add(chkRunOnStartup);

            Controls.Add(chkDoubleClickToggle);

            Controls.Add(labelHotkey);
            Controls.Add(chkHotkeyEnabled);
            Controls.Add(chkHotkeyCtrl);
            Controls.Add(chkHotkeyAlt);
            Controls.Add(chkHotkeyShift);
            Controls.Add(chkHotkeyWin);
            Controls.Add(comboHotkeyKey);

            Controls.Add(labelToggleHotkey);
            Controls.Add(chkToggleHotkeyEnabled);
            Controls.Add(chkToggleHotkeyCtrl);
            Controls.Add(chkToggleHotkeyAlt);
            Controls.Add(chkToggleHotkeyShift);
            Controls.Add(chkToggleHotkeyWin);
            Controls.Add(comboToggleHotkeyKey);

            Controls.Add(labelVersion);

            Controls.Add(btnApply);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinXCorners.NET";
            ClientSize = new Size(800, 480);  // Increased width for delay column, height for toggle hotkey

            ((System.ComponentModel.ISupportInitialize)numDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCornerSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTopLeftDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTopRightDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBottomLeftDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBottomRightDelay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
