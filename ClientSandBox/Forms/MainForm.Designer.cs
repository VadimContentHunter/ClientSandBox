namespace ClientSandBox.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabMain = new TabControl();
            settingPage = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            chkCloseToTray = new CheckBox();
            tableSettingPaths = new TableLayoutPanel();
            btnBrowseSingBox = new Button();
            txtSingBox = new TextBox();
            lblConfig = new Label();
            btnBrowseConfig = new Button();
            txtConfig = new TextBox();
            lblSingBox = new Label();
            controlPage = new TabPage();
            logsPage = new TabPage();
            tabMain.SuspendLayout();
            settingPage.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableSettingPaths.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(settingPage);
            tabMain.Controls.Add(controlPage);
            tabMain.Controls.Add(logsPage);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(884, 611);
            tabMain.TabIndex = 0;
            // 
            // settingPage
            // 
            settingPage.AccessibleName = "";
            settingPage.Controls.Add(tableLayoutPanel1);
            settingPage.Controls.Add(tableSettingPaths);
            settingPage.Location = new Point(4, 24);
            settingPage.Name = "settingPage";
            settingPage.Padding = new Padding(3);
            settingPage.Size = new Size(876, 583);
            settingPage.TabIndex = 0;
            settingPage.Text = "Настройки";
            settingPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(chkCloseToTray, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 109);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 20, 0, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(870, 45);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // chkCloseToTray
            // 
            chkCloseToTray.AutoSize = true;
            chkCloseToTray.Dock = DockStyle.Left;
            chkCloseToTray.Location = new Point(3, 23);
            chkCloseToTray.Name = "chkCloseToTray";
            chkCloseToTray.Size = new Size(151, 19);
            chkCloseToTray.TabIndex = 3;
            chkCloseToTray.Text = "Закрывать окно в трей";
            chkCloseToTray.UseVisualStyleBackColor = true;
            // 
            // tableSettingPaths
            // 
            tableSettingPaths.AutoSize = true;
            tableSettingPaths.ColumnCount = 3;
            tableSettingPaths.ColumnStyles.Add(new ColumnStyle());
            tableSettingPaths.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.28872F));
            tableSettingPaths.ColumnStyles.Add(new ColumnStyle());
            tableSettingPaths.Controls.Add(btnBrowseSingBox, 2, 0);
            tableSettingPaths.Controls.Add(txtSingBox, 1, 0);
            tableSettingPaths.Controls.Add(lblConfig, 0, 1);
            tableSettingPaths.Controls.Add(btnBrowseConfig, 2, 1);
            tableSettingPaths.Controls.Add(txtConfig, 1, 1);
            tableSettingPaths.Controls.Add(lblSingBox, 0, 0);
            tableSettingPaths.Dock = DockStyle.Top;
            tableSettingPaths.Location = new Point(3, 3);
            tableSettingPaths.Name = "tableSettingPaths";
            tableSettingPaths.Padding = new Padding(0, 20, 0, 0);
            tableSettingPaths.RowCount = 2;
            tableSettingPaths.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableSettingPaths.RowStyles.Add(new RowStyle());
            tableSettingPaths.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSettingPaths.Size = new Size(870, 106);
            tableSettingPaths.TabIndex = 4;
            // 
            // btnBrowseSingBox
            // 
            btnBrowseSingBox.Location = new Point(792, 30);
            btnBrowseSingBox.Margin = new Padding(3, 10, 3, 10);
            btnBrowseSingBox.Name = "btnBrowseSingBox";
            btnBrowseSingBox.Size = new Size(75, 23);
            btnBrowseSingBox.TabIndex = 2;
            btnBrowseSingBox.Text = "Обзор...";
            btnBrowseSingBox.UseVisualStyleBackColor = true;
            btnBrowseSingBox.Click += btnBrowseSingBox_Click;
            // 
            // txtSingBox
            // 
            txtSingBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSingBox.Location = new Point(120, 30);
            txtSingBox.Margin = new Padding(3, 10, 3, 10);
            txtSingBox.Name = "txtSingBox";
            txtSingBox.Size = new Size(666, 23);
            txtSingBox.TabIndex = 1;
            // 
            // lblConfig
            // 
            lblConfig.AutoSize = true;
            lblConfig.Dock = DockStyle.Fill;
            lblConfig.Location = new Point(3, 73);
            lblConfig.Margin = new Padding(3, 10, 3, 10);
            lblConfig.Name = "lblConfig";
            lblConfig.Size = new Size(111, 23);
            lblConfig.TabIndex = 0;
            lblConfig.Text = "Путь к config.json";
            lblConfig.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBrowseConfig
            // 
            btnBrowseConfig.Location = new Point(792, 73);
            btnBrowseConfig.Margin = new Padding(3, 10, 3, 10);
            btnBrowseConfig.Name = "btnBrowseConfig";
            btnBrowseConfig.Size = new Size(75, 23);
            btnBrowseConfig.TabIndex = 2;
            btnBrowseConfig.Text = "Обзор...";
            btnBrowseConfig.UseVisualStyleBackColor = true;
            btnBrowseConfig.Click += btnBrowseConfig_Click;
            // 
            // txtConfig
            // 
            txtConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtConfig.Location = new Point(120, 73);
            txtConfig.Margin = new Padding(3, 10, 3, 10);
            txtConfig.Name = "txtConfig";
            txtConfig.Size = new Size(666, 23);
            txtConfig.TabIndex = 1;
            // 
            // lblSingBox
            // 
            lblSingBox.AutoSize = true;
            lblSingBox.Dock = DockStyle.Fill;
            lblSingBox.Location = new Point(3, 30);
            lblSingBox.Margin = new Padding(3, 10, 3, 10);
            lblSingBox.Name = "lblSingBox";
            lblSingBox.Size = new Size(111, 23);
            lblSingBox.TabIndex = 0;
            lblSingBox.Text = "Путь к sing-box.exe";
            lblSingBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // controlPage
            // 
            controlPage.Location = new Point(4, 24);
            controlPage.Name = "controlPage";
            controlPage.Padding = new Padding(3);
            controlPage.Size = new Size(876, 583);
            controlPage.TabIndex = 1;
            controlPage.Text = "Управление";
            controlPage.UseVisualStyleBackColor = true;
            // 
            // logsPage
            // 
            logsPage.Location = new Point(4, 24);
            logsPage.Name = "logsPage";
            logsPage.Padding = new Padding(3);
            logsPage.Size = new Size(876, 583);
            logsPage.TabIndex = 2;
            logsPage.Text = "Логи";
            logsPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 611);
            Controls.Add(tabMain);
            MinimumSize = new Size(900, 650);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SingBox Manager";
            tabMain.ResumeLayout(false);
            settingPage.ResumeLayout(false);
            settingPage.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableSettingPaths.ResumeLayout(false);
            tableSettingPaths.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage settingPage;
        private TabPage controlPage;
        private TabPage logsPage;
        private TextBox txtSingBox;
        private Label lblSingBox;
        private Button btnBrowseSingBox;
        private Button btnBrowseConfig;
        private TextBox txtConfig;
        private Label lblConfig;
        private CheckBox chkCloseToTray;
        private TableLayoutPanel tableSettingPaths;
        private TableLayoutPanel tableLayoutPanel1;
    }
}