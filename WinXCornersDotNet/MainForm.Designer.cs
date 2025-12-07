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

        private Label labelDelay;
        private NumericUpDown numDelay;
        private Label labelCornerSize;
        private NumericUpDown numCornerSize;

        private CheckBox chkEnabled;
        private CheckBox chkDisableFullscreen;
        private CheckBox chkRunOnStartup;

        private Button btnSave;
        private Button btnCancel;

        // Global hotkey controls
        private Label labelHotkey;
        private CheckBox chkHotkeyEnabled;
        private CheckBox chkHotkeyCtrl;
        private CheckBox chkHotkeyAlt;
        private CheckBox chkHotkeyShift;
        private CheckBox chkHotkeyWin;
        private ComboBox comboHotkeyKey;

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

            labelDelay = new Label();
            numDelay = new NumericUpDown();
            labelCornerSize = new Label();
            numCornerSize = new NumericUpDown();

            chkEnabled = new CheckBox();
            chkDisableFullscreen = new CheckBox();
            chkRunOnStartup = new CheckBox();

            btnSave = new Button();
            btnCancel = new Button();

            // Hotkey controls
            labelHotkey = new Label();
            chkHotkeyEnabled = new CheckBox();
            chkHotkeyCtrl = new CheckBox();
            chkHotkeyAlt = new CheckBox();
            chkHotkeyShift = new CheckBox();
            chkHotkeyWin = new CheckBox();
            comboHotkeyKey = new ComboBox();

            ((System.ComponentModel.ISupportInitialize)numDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCornerSize).BeginInit();
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

            // Buttons (moved down)
            btnSave.Location = new Point(500, 325);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 27);
            btnSave.Text = "Save && Hide";
            btnSave.UseVisualStyleBackColor = true;

            btnCancel.Location = new Point(600, 325);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 27);
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;

            // MainForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(710, 310);
            Controls.Add(labelTitle);
            Controls.Add(labelHeaderCorner);
            Controls.Add(labelHeaderAction);
            Controls.Add(labelHeaderCommand);
            Controls.Add(labelHeaderArgs);

            Controls.Add(labelTopLeft);
            Controls.Add(comboTopLeft);
            Controls.Add(txtTopLeftCommand);
            Controls.Add(btnBrowseTopLeft);
            Controls.Add(txtTopLeftArgs);

            Controls.Add(labelTopRight);
            Controls.Add(comboTopRight);
            Controls.Add(txtTopRightCommand);
            Controls.Add(btnBrowseTopRight);
            Controls.Add(txtTopRightArgs);

            Controls.Add(labelBottomLeft);
            Controls.Add(comboBottomLeft);
            Controls.Add(txtBottomLeftCommand);
            Controls.Add(btnBrowseBottomLeft);
            Controls.Add(txtBottomLeftArgs);

            Controls.Add(labelBottomRight);
            Controls.Add(comboBottomRight);
            Controls.Add(txtBottomRightCommand);
            Controls.Add(btnBrowseBottomRight);
            Controls.Add(txtBottomRightArgs);

            Controls.Add(labelDelay);
            Controls.Add(numDelay);
            Controls.Add(labelCornerSize);
            Controls.Add(numCornerSize);

            Controls.Add(chkEnabled);
            Controls.Add(chkDisableFullscreen);
            Controls.Add(chkRunOnStartup);

            Controls.Add(labelHotkey);
            Controls.Add(chkHotkeyEnabled);
            Controls.Add(chkHotkeyCtrl);
            Controls.Add(chkHotkeyAlt);
            Controls.Add(chkHotkeyShift);
            Controls.Add(chkHotkeyWin);
            Controls.Add(comboHotkeyKey);

            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WinXCorners.NET";
            ClientSize = new Size(710, 365);

            ((System.ComponentModel.ISupportInitialize)numDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCornerSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
