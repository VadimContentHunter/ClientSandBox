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
            components = new System.ComponentModel.Container();
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
            groupGeneralControl = new GroupBox();
            flowLayoutGeneralControl = new FlowLayoutPanel();
            btnCheckConfig = new Button();
            btnOpenFolder = new Button();
            btnOpenConfig = new Button();
            groupSingBox = new GroupBox();
            flowLayoutSingBox = new FlowLayoutPanel();
            btnStartSingBox = new Button();
            btnStopSingBox = new Button();
            btnRestartSingBox = new Button();
            tableStatusControl = new TableLayoutPanel();
            lblStatusSingBox = new Label();
            lblVersionCaption = new Label();
            lblVersion = new Label();
            labelStatusSingBoxCaption = new Label();
            lblPidInf = new Label();
            labelPIDCaption = new Label();
            logsPage = new TabPage();
            notifyIcon = new NotifyIcon(components);
            tabMain.SuspendLayout();
            settingPage.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableSettingPaths.SuspendLayout();
            controlPage.SuspendLayout();
            groupGeneralControl.SuspendLayout();
            flowLayoutGeneralControl.SuspendLayout();
            groupSingBox.SuspendLayout();
            flowLayoutSingBox.SuspendLayout();
            tableStatusControl.SuspendLayout();
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
            tabMain.Size = new Size(884, 561);
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
            settingPage.Size = new Size(876, 533);
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
            txtSingBox.KeyDown += PathTextBox_KeyDown;
            txtSingBox.Leave += PathTextBox_Leave;
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
            txtConfig.KeyDown += PathTextBox_KeyDown;
            txtConfig.Leave += PathTextBox_Leave;
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
            controlPage.Controls.Add(groupGeneralControl);
            controlPage.Controls.Add(groupSingBox);
            controlPage.Controls.Add(tableStatusControl);
            controlPage.Location = new Point(4, 24);
            controlPage.Name = "controlPage";
            controlPage.Padding = new Padding(3);
            controlPage.Size = new Size(876, 533);
            controlPage.TabIndex = 1;
            controlPage.Text = "Управление";
            controlPage.UseVisualStyleBackColor = true;
            // 
            // groupGeneralControl
            // 
            groupGeneralControl.AutoSize = true;
            groupGeneralControl.Controls.Add(flowLayoutGeneralControl);
            groupGeneralControl.Dock = DockStyle.Top;
            groupGeneralControl.Location = new Point(3, 236);
            groupGeneralControl.Name = "groupGeneralControl";
            groupGeneralControl.Padding = new Padding(3, 8, 3, 20);
            groupGeneralControl.Size = new Size(870, 91);
            groupGeneralControl.TabIndex = 2;
            groupGeneralControl.TabStop = false;
            groupGeneralControl.Text = "Общее управление";
            // 
            // flowLayoutGeneralControl
            // 
            flowLayoutGeneralControl.AutoSize = true;
            flowLayoutGeneralControl.Controls.Add(btnCheckConfig);
            flowLayoutGeneralControl.Controls.Add(btnOpenFolder);
            flowLayoutGeneralControl.Controls.Add(btnOpenConfig);
            flowLayoutGeneralControl.Dock = DockStyle.Fill;
            flowLayoutGeneralControl.Location = new Point(3, 24);
            flowLayoutGeneralControl.Name = "flowLayoutGeneralControl";
            flowLayoutGeneralControl.Padding = new Padding(0, 8, 0, 8);
            flowLayoutGeneralControl.Size = new Size(864, 47);
            flowLayoutGeneralControl.TabIndex = 0;
            // 
            // btnCheckConfig
            // 
            btnCheckConfig.AutoSize = true;
            btnCheckConfig.Location = new Point(16, 11);
            btnCheckConfig.Margin = new Padding(16, 3, 16, 3);
            btnCheckConfig.Name = "btnCheckConfig";
            btnCheckConfig.Size = new Size(137, 25);
            btnCheckConfig.TabIndex = 2;
            btnCheckConfig.Text = "Проверить конфиг";
            btnCheckConfig.UseVisualStyleBackColor = true;
            btnCheckConfig.Click += btnCheckConfig_Click;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.AutoSize = true;
            btnOpenFolder.Location = new Point(185, 11);
            btnOpenFolder.Margin = new Padding(16, 3, 16, 3);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(142, 25);
            btnOpenFolder.TabIndex = 6;
            btnOpenFolder.Text = "Открыть папку";
            btnOpenFolder.UseVisualStyleBackColor = true;
            btnOpenFolder.Click += btnOpenSingBoxFolder_Click;
            // 
            // btnOpenConfig
            // 
            btnOpenConfig.AutoSize = true;
            btnOpenConfig.Location = new Point(359, 11);
            btnOpenConfig.Margin = new Padding(16, 3, 16, 3);
            btnOpenConfig.Name = "btnOpenConfig";
            btnOpenConfig.Size = new Size(142, 25);
            btnOpenConfig.TabIndex = 7;
            btnOpenConfig.Text = "Открыть config.json";
            btnOpenConfig.UseVisualStyleBackColor = true;
            btnOpenConfig.Click += btnOpenConfig_Click;
            // 
            // groupSingBox
            // 
            groupSingBox.Controls.Add(flowLayoutSingBox);
            groupSingBox.Dock = DockStyle.Top;
            groupSingBox.Location = new Point(3, 146);
            groupSingBox.Name = "groupSingBox";
            groupSingBox.Padding = new Padding(3, 8, 3, 20);
            groupSingBox.Size = new Size(870, 90);
            groupSingBox.TabIndex = 1;
            groupSingBox.TabStop = false;
            groupSingBox.Text = "Управление sing-box";
            // 
            // flowLayoutSingBox
            // 
            flowLayoutSingBox.AutoSize = true;
            flowLayoutSingBox.Controls.Add(btnStartSingBox);
            flowLayoutSingBox.Controls.Add(btnStopSingBox);
            flowLayoutSingBox.Controls.Add(btnRestartSingBox);
            flowLayoutSingBox.Dock = DockStyle.Fill;
            flowLayoutSingBox.Location = new Point(3, 24);
            flowLayoutSingBox.Name = "flowLayoutSingBox";
            flowLayoutSingBox.Padding = new Padding(0, 8, 0, 8);
            flowLayoutSingBox.Size = new Size(864, 46);
            flowLayoutSingBox.TabIndex = 0;
            // 
            // btnStartSingBox
            // 
            btnStartSingBox.AutoSize = true;
            btnStartSingBox.Location = new Point(16, 11);
            btnStartSingBox.Margin = new Padding(16, 3, 16, 3);
            btnStartSingBox.Name = "btnStartSingBox";
            btnStartSingBox.Size = new Size(137, 25);
            btnStartSingBox.TabIndex = 3;
            btnStartSingBox.Text = "Запустить";
            btnStartSingBox.UseVisualStyleBackColor = true;
            btnStartSingBox.Click += btnStartSingBox_Click;
            // 
            // btnStopSingBox
            // 
            btnStopSingBox.AutoSize = true;
            btnStopSingBox.Location = new Point(185, 11);
            btnStopSingBox.Margin = new Padding(16, 3, 16, 3);
            btnStopSingBox.Name = "btnStopSingBox";
            btnStopSingBox.Size = new Size(137, 25);
            btnStopSingBox.TabIndex = 4;
            btnStopSingBox.Text = "Остановить";
            btnStopSingBox.UseVisualStyleBackColor = true;
            btnStopSingBox.Click += btnStopSingBox_Click;
            // 
            // btnRestartSingBox
            // 
            btnRestartSingBox.AutoSize = true;
            btnRestartSingBox.Location = new Point(354, 11);
            btnRestartSingBox.Margin = new Padding(16, 3, 16, 3);
            btnRestartSingBox.Name = "btnRestartSingBox";
            btnRestartSingBox.Size = new Size(142, 25);
            btnRestartSingBox.TabIndex = 5;
            btnRestartSingBox.Text = "Перезапустить";
            btnRestartSingBox.UseVisualStyleBackColor = true;
            btnRestartSingBox.Click += btnRestartSingBox_Click;
            // 
            // tableStatusControl
            // 
            tableStatusControl.AutoSize = true;
            tableStatusControl.ColumnCount = 2;
            tableStatusControl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableStatusControl.ColumnStyles.Add(new ColumnStyle());
            tableStatusControl.Controls.Add(lblStatusSingBox, 1, 0);
            tableStatusControl.Controls.Add(lblVersionCaption, 0, 2);
            tableStatusControl.Controls.Add(lblVersion, 1, 2);
            tableStatusControl.Controls.Add(labelStatusSingBoxCaption, 0, 0);
            tableStatusControl.Controls.Add(lblPidInf, 1, 1);
            tableStatusControl.Controls.Add(labelPIDCaption, 0, 1);
            tableStatusControl.Dock = DockStyle.Top;
            tableStatusControl.Location = new Point(3, 3);
            tableStatusControl.Name = "tableStatusControl";
            tableStatusControl.Padding = new Padding(0, 20, 0, 30);
            tableStatusControl.RowCount = 3;
            tableStatusControl.RowStyles.Add(new RowStyle());
            tableStatusControl.RowStyles.Add(new RowStyle());
            tableStatusControl.RowStyles.Add(new RowStyle());
            tableStatusControl.Size = new Size(870, 143);
            tableStatusControl.TabIndex = 0;
            // 
            // lblStatusSingBox
            // 
            lblStatusSingBox.Dock = DockStyle.Fill;
            lblStatusSingBox.Location = new Point(143, 28);
            lblStatusSingBox.Margin = new Padding(3, 8, 3, 8);
            lblStatusSingBox.Name = "lblStatusSingBox";
            lblStatusSingBox.Size = new Size(740, 15);
            lblStatusSingBox.TabIndex = 0;
            lblStatusSingBox.Text = "Не запущен";
            lblStatusSingBox.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVersionCaption
            // 
            lblVersionCaption.Dock = DockStyle.Top;
            lblVersionCaption.Location = new Point(3, 90);
            lblVersionCaption.Margin = new Padding(3, 8, 3, 8);
            lblVersionCaption.Name = "lblVersionCaption";
            lblVersionCaption.Size = new Size(134, 15);
            lblVersionCaption.TabIndex = 2;
            lblVersionCaption.Text = "Версия sing-box:";
            lblVersionCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Dock = DockStyle.Fill;
            lblVersion.Location = new Point(143, 90);
            lblVersion.Margin = new Padding(3, 8, 3, 8);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(740, 15);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "—";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelStatusSingBoxCaption
            // 
            labelStatusSingBoxCaption.Dock = DockStyle.Fill;
            labelStatusSingBoxCaption.Location = new Point(3, 28);
            labelStatusSingBoxCaption.Margin = new Padding(3, 8, 3, 8);
            labelStatusSingBoxCaption.Name = "labelStatusSingBoxCaption";
            labelStatusSingBoxCaption.Size = new Size(134, 15);
            labelStatusSingBoxCaption.TabIndex = 1;
            labelStatusSingBoxCaption.Text = "Статус sing-box:";
            labelStatusSingBoxCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPidInf
            // 
            lblPidInf.Dock = DockStyle.Fill;
            lblPidInf.Location = new Point(143, 59);
            lblPidInf.Margin = new Padding(3, 8, 3, 8);
            lblPidInf.Name = "lblPidInf";
            lblPidInf.Size = new Size(740, 15);
            lblPidInf.TabIndex = 5;
            lblPidInf.Text = "—";
            lblPidInf.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelPIDCaption
            // 
            labelPIDCaption.Dock = DockStyle.Fill;
            labelPIDCaption.Location = new Point(3, 59);
            labelPIDCaption.Margin = new Padding(3, 8, 3, 8);
            labelPIDCaption.Name = "labelPIDCaption";
            labelPIDCaption.Size = new Size(134, 15);
            labelPIDCaption.TabIndex = 4;
            labelPIDCaption.Text = "PID:";
            labelPIDCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // logsPage
            // 
            logsPage.Location = new Point(4, 24);
            logsPage.Name = "logsPage";
            logsPage.Padding = new Padding(3);
            logsPage.Size = new Size(876, 533);
            logsPage.TabIndex = 2;
            logsPage.Text = "Логи";
            logsPage.UseVisualStyleBackColor = true;
            // 
            // notifyIcon
            // 
            notifyIcon.Text = "Client SandBox";
            notifyIcon.MouseDoubleClick += notifyIcon_DoubleClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(tabMain);
            MaximumSize = new Size(1200, 800);
            MinimumSize = new Size(900, 600);
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
            controlPage.ResumeLayout(false);
            controlPage.PerformLayout();
            groupGeneralControl.ResumeLayout(false);
            groupGeneralControl.PerformLayout();
            flowLayoutGeneralControl.ResumeLayout(false);
            flowLayoutGeneralControl.PerformLayout();
            groupSingBox.ResumeLayout(false);
            groupSingBox.PerformLayout();
            flowLayoutSingBox.ResumeLayout(false);
            flowLayoutSingBox.PerformLayout();
            tableStatusControl.ResumeLayout(false);
            tableStatusControl.PerformLayout();
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
        private TableLayoutPanel tableStatusControl;
        private Label lblStatusSingBox;
        private Label labelStatusSingBoxCaption;
        private Label lblVersionCaption;
        private Label lblVersion;
        private GroupBox groupSingBox;
        private FlowLayoutPanel flowLayoutSingBox;
        private GroupBox groupGeneralControl;
        private FlowLayoutPanel flowLayoutGeneralControl;
        private Button btnCheckConfig;
        private Button btnStartSingBox;
        private Button btnStopSingBox;
        private Button btnRestartSingBox;
        private Button btnOpenFolder;
        private Button btnOpenConfig;
        private Label lblPidInf;
        private Label labelPIDCaption;
        private NotifyIcon notifyIcon;
    }
}