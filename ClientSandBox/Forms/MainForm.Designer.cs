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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabMain = new TabControl();
            settingPage = new TabPage();
            flowLayoutPanel14 = new FlowLayoutPanel();
            chkCloseToTray = new CheckBox();
            chkStartWithWindows = new CheckBox();
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
            btnRestoreNetwork = new Button();
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
            tabPageConnections = new TabPage();
            gridConnections = new DataGridView();
            colEnabled = new DataGridViewCheckBoxColumn();
            colTag = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colInfo = new DataGridViewTextBoxColumn();
            cmsConnections = new ContextMenuStrip(components);
            miSelectConnection = new ToolStripMenuItem();
            miEditConnection = new ToolStripMenuItem();
            miDeleteConnection = new ToolStripMenuItem();
            fLConnectionsControlPanel = new FlowLayoutPanel();
            btnAddConnection = new Button();
            logsPage = new TabPage();
            rtbLogViewer = new RichTextBox();
            flowLayoutPanel12 = new FlowLayoutPanel();
            flowLayoutPanel7 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            lblLogLevel = new Label();
            cmbLogLevel = new ComboBox();
            flowLayoutPanel4 = new FlowLayoutPanel();
            lblLogOutputPath = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            txtLogOutputPath = new TextBox();
            btnBrowseLogPath = new Button();
            flowLayoutPanel6 = new FlowLayoutPanel();
            lblLogOutput = new Label();
            cmbLogOutput = new ComboBox();
            flowLayoutPanel11 = new FlowLayoutPanel();
            flowLayoutPanel10 = new FlowLayoutPanel();
            lblKeepLastLines = new Label();
            numKeepLastLines = new NumericUpDown();
            flowLayoutPanel8 = new FlowLayoutPanel();
            lblAutoClear = new Label();
            numAutoClearMinutes = new NumericUpDown();
            flowLayoutPanel9 = new FlowLayoutPanel();
            lblTailLines = new Label();
            numTailLinesToShow = new NumericUpDown();
            flowLayoutPanel5 = new FlowLayoutPanel();
            chkClearLogsOnStart = new CheckBox();
            chkAutoScroll = new CheckBox();
            chkEnableLogging = new CheckBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblCurrentLogFile = new Label();
            flowLayoutPanel13 = new FlowLayoutPanel();
            lblTimestamp = new Label();
            btnOpenLog = new Button();
            btnClearLog = new Button();
            notifyIcon = new NotifyIcon(components);
            trayMenu = new ContextMenuStrip(components);
            miOpen = new ToolStripMenuItem();
            miStart = new ToolStripMenuItem();
            miStop = new ToolStripMenuItem();
            miRestart = new ToolStripMenuItem();
            miExit = new ToolStripMenuItem();
            tabMain.SuspendLayout();
            settingPage.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            tableSettingPaths.SuspendLayout();
            controlPage.SuspendLayout();
            groupGeneralControl.SuspendLayout();
            flowLayoutGeneralControl.SuspendLayout();
            groupSingBox.SuspendLayout();
            flowLayoutSingBox.SuspendLayout();
            tableStatusControl.SuspendLayout();
            tabPageConnections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridConnections).BeginInit();
            cmsConnections.SuspendLayout();
            fLConnectionsControlPanel.SuspendLayout();
            logsPage.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numKeepLastLines).BeginInit();
            flowLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoClearMinutes).BeginInit();
            flowLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTailLinesToShow).BeginInit();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            trayMenu.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(settingPage);
            tabMain.Controls.Add(controlPage);
            tabMain.Controls.Add(tabPageConnections);
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
            settingPage.Controls.Add(flowLayoutPanel14);
            settingPage.Controls.Add(tableSettingPaths);
            settingPage.Location = new Point(4, 24);
            settingPage.Name = "settingPage";
            settingPage.Padding = new Padding(3);
            settingPage.Size = new Size(876, 533);
            settingPage.TabIndex = 0;
            settingPage.Text = "Настройки";
            settingPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel14
            // 
            flowLayoutPanel14.AutoScroll = true;
            flowLayoutPanel14.AutoSize = true;
            flowLayoutPanel14.Controls.Add(chkCloseToTray);
            flowLayoutPanel14.Controls.Add(chkStartWithWindows);
            flowLayoutPanel14.Dock = DockStyle.Top;
            flowLayoutPanel14.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel14.Location = new Point(3, 109);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            flowLayoutPanel14.Padding = new Padding(12, 8, 12, 8);
            flowLayoutPanel14.Size = new Size(870, 102);
            flowLayoutPanel14.TabIndex = 5;
            flowLayoutPanel14.WrapContents = false;
            // 
            // chkCloseToTray
            // 
            chkCloseToTray.AutoSize = true;
            chkCloseToTray.Location = new Point(20, 20);
            chkCloseToTray.Margin = new Padding(8, 12, 8, 12);
            chkCloseToTray.Name = "chkCloseToTray";
            chkCloseToTray.Size = new Size(151, 19);
            chkCloseToTray.TabIndex = 3;
            chkCloseToTray.Text = "Закрывать окно в трей";
            chkCloseToTray.UseVisualStyleBackColor = true;
            // 
            // chkStartWithWindows
            // 
            chkStartWithWindows.AutoSize = true;
            chkStartWithWindows.Location = new Point(20, 63);
            chkStartWithWindows.Margin = new Padding(8, 12, 8, 12);
            chkStartWithWindows.Name = "chkStartWithWindows";
            chkStartWithWindows.Size = new Size(223, 19);
            chkStartWithWindows.TabIndex = 4;
            chkStartWithWindows.Text = "Запускать при включении Windows";
            chkStartWithWindows.UseVisualStyleBackColor = true;
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
            lblConfig.Anchor = AnchorStyles.Left;
            lblConfig.AutoSize = true;
            lblConfig.Location = new Point(3, 77);
            lblConfig.Margin = new Padding(3, 10, 3, 10);
            lblConfig.Name = "lblConfig";
            lblConfig.Size = new Size(104, 15);
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
            lblSingBox.Anchor = AnchorStyles.Left;
            lblSingBox.AutoSize = true;
            lblSingBox.Location = new Point(3, 34);
            lblSingBox.Margin = new Padding(3, 10, 3, 10);
            lblSingBox.Name = "lblSingBox";
            lblSingBox.Size = new Size(111, 15);
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
            groupGeneralControl.TabIndex = 3;
            groupGeneralControl.TabStop = false;
            groupGeneralControl.Text = "Общее управление";
            // 
            // flowLayoutGeneralControl
            // 
            flowLayoutGeneralControl.AutoSize = true;
            flowLayoutGeneralControl.Controls.Add(btnCheckConfig);
            flowLayoutGeneralControl.Controls.Add(btnOpenFolder);
            flowLayoutGeneralControl.Controls.Add(btnOpenConfig);
            flowLayoutGeneralControl.Controls.Add(btnRestoreNetwork);
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
            // btnRestoreNetwork
            // 
            btnRestoreNetwork.AutoSize = true;
            btnRestoreNetwork.Location = new Point(533, 11);
            btnRestoreNetwork.Margin = new Padding(16, 3, 16, 3);
            btnRestoreNetwork.Name = "btnRestoreNetwork";
            btnRestoreNetwork.Size = new Size(160, 25);
            btnRestoreNetwork.TabIndex = 8;
            btnRestoreNetwork.Text = "Восстановить сеть";
            btnRestoreNetwork.UseVisualStyleBackColor = true;
            btnRestoreNetwork.Click += btnRestoreNetwork_Click;
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
            tableStatusControl.Controls.Add(lblVersionCaption, 0, 3);
            tableStatusControl.Controls.Add(lblVersion, 1, 3);
            tableStatusControl.Controls.Add(labelStatusSingBoxCaption, 0, 0);
            tableStatusControl.Controls.Add(lblPidInf, 1, 2);
            tableStatusControl.Controls.Add(labelPIDCaption, 0, 2);
            tableStatusControl.Dock = DockStyle.Top;
            tableStatusControl.Location = new Point(3, 3);
            tableStatusControl.Name = "tableStatusControl";
            tableStatusControl.Padding = new Padding(0, 20, 0, 30);
            tableStatusControl.RowCount = 4;
            tableStatusControl.RowStyles.Add(new RowStyle());
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
            labelPIDCaption.Location = new Point(3, 59);
            labelPIDCaption.Margin = new Padding(3, 8, 3, 8);
            labelPIDCaption.Name = "labelPIDCaption";
            labelPIDCaption.Size = new Size(134, 15);
            labelPIDCaption.TabIndex = 4;
            labelPIDCaption.Text = "PID:";
            labelPIDCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tabPageConnections
            // 
            tabPageConnections.Controls.Add(gridConnections);
            tabPageConnections.Controls.Add(fLConnectionsControlPanel);
            tabPageConnections.Location = new Point(4, 24);
            tabPageConnections.Name = "tabPageConnections";
            tabPageConnections.Padding = new Padding(3);
            tabPageConnections.Size = new Size(876, 533);
            tabPageConnections.TabIndex = 3;
            tabPageConnections.Text = "Подключения";
            tabPageConnections.UseVisualStyleBackColor = true;
            // 
            // gridConnections
            // 
            gridConnections.AllowUserToAddRows = false;
            gridConnections.AllowUserToDeleteRows = false;
            gridConnections.AllowUserToResizeColumns = false;
            gridConnections.AllowUserToResizeRows = false;
            gridConnections.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridConnections.Columns.AddRange(new DataGridViewColumn[] { colEnabled, colTag, colType, colStatus, colInfo });
            gridConnections.ContextMenuStrip = cmsConnections;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            gridConnections.DefaultCellStyle = dataGridViewCellStyle3;
            gridConnections.Dock = DockStyle.Fill;
            gridConnections.Location = new Point(3, 50);
            gridConnections.MultiSelect = false;
            gridConnections.Name = "gridConnections";
            gridConnections.RowHeadersVisible = false;
            gridConnections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridConnections.Size = new Size(870, 480);
            gridConnections.TabIndex = 1;
            gridConnections.CellMouseDown += gridConnections_CellMouseDown;
            gridConnections.CurrentCellDirtyStateChanged += gridConnections_CurrentCellDirtyStateChanged;
            // 
            // colEnabled
            // 
            colEnabled.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colEnabled.FalseValue = false;
            colEnabled.HeaderText = "☑";
            colEnabled.Name = "colEnabled";
            colEnabled.TrueValue = true;
            colEnabled.Width = 40;
            // 
            // colTag
            // 
            colTag.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTag.HeaderText = "Tag";
            colTag.Name = "colTag";
            colTag.ReadOnly = true;
            colTag.Width = 180;
            // 
            // colType
            // 
            colType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colType.DefaultCellStyle = dataGridViewCellStyle1;
            colType.HeaderText = "Type";
            colType.Name = "colType";
            colType.ReadOnly = true;
            colType.Width = 90;
            // 
            // colStatus
            // 
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle = dataGridViewCellStyle2;
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 150;
            // 
            // colInfo
            // 
            colInfo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colInfo.HeaderText = "Info";
            colInfo.Name = "colInfo";
            colInfo.ReadOnly = true;
            // 
            // cmsConnections
            // 
            cmsConnections.Items.AddRange(new ToolStripItem[] { miSelectConnection, miEditConnection, miDeleteConnection });
            cmsConnections.Name = "cmsConnections";
            cmsConnections.Size = new Size(155, 106);
            // 
            // miSelectConnection
            // 
            miSelectConnection.Name = "miSelectConnection";
            miSelectConnection.Size = new Size(154, 34);
            miSelectConnection.Text = "Выбрать";
            miSelectConnection.Click += miSelectConnection_Click;
            // 
            // miEditConnection
            // 
            miEditConnection.Name = "miEditConnection";
            miEditConnection.Size = new Size(154, 34);
            miEditConnection.Text = "Редактировать\n";
            miEditConnection.Click += miEditConnection_Click;
            // 
            // miDeleteConnection
            // 
            miDeleteConnection.Name = "miDeleteConnection";
            miDeleteConnection.Size = new Size(154, 34);
            miDeleteConnection.Text = "Удалить";
            miDeleteConnection.Click += miDeleteConnection_Click;
            // 
            // fLConnectionsControlPanel
            // 
            fLConnectionsControlPanel.AutoSize = true;
            fLConnectionsControlPanel.Controls.Add(btnAddConnection);
            fLConnectionsControlPanel.Dock = DockStyle.Top;
            fLConnectionsControlPanel.Location = new Point(3, 3);
            fLConnectionsControlPanel.Name = "fLConnectionsControlPanel";
            fLConnectionsControlPanel.Padding = new Padding(0, 8, 0, 8);
            fLConnectionsControlPanel.Size = new Size(870, 47);
            fLConnectionsControlPanel.TabIndex = 0;
            // 
            // btnAddConnection
            // 
            btnAddConnection.AutoSize = true;
            btnAddConnection.Location = new Point(3, 11);
            btnAddConnection.Name = "btnAddConnection";
            btnAddConnection.Size = new Size(148, 25);
            btnAddConnection.TabIndex = 0;
            btnAddConnection.Text = "Добавить подключение";
            btnAddConnection.UseVisualStyleBackColor = true;
            btnAddConnection.Click += btnAddConnection_Click;
            // 
            // logsPage
            // 
            logsPage.Controls.Add(rtbLogViewer);
            logsPage.Controls.Add(flowLayoutPanel12);
            logsPage.Controls.Add(flowLayoutPanel5);
            logsPage.Controls.Add(flowLayoutPanel1);
            logsPage.Location = new Point(4, 24);
            logsPage.Name = "logsPage";
            logsPage.Padding = new Padding(3);
            logsPage.Size = new Size(876, 533);
            logsPage.TabIndex = 4;
            logsPage.Text = "Логи";
            logsPage.UseVisualStyleBackColor = true;
            // 
            // rtbLogViewer
            // 
            rtbLogViewer.Dock = DockStyle.Fill;
            rtbLogViewer.Location = new Point(3, 250);
            rtbLogViewer.Name = "rtbLogViewer";
            rtbLogViewer.ReadOnly = true;
            rtbLogViewer.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbLogViewer.Size = new Size(870, 280);
            rtbLogViewer.TabIndex = 1;
            rtbLogViewer.Text = "";
            rtbLogViewer.WordWrap = false;
            // 
            // flowLayoutPanel12
            // 
            flowLayoutPanel12.AutoSize = true;
            flowLayoutPanel12.Controls.Add(flowLayoutPanel7);
            flowLayoutPanel12.Controls.Add(flowLayoutPanel11);
            flowLayoutPanel12.Dock = DockStyle.Top;
            flowLayoutPanel12.Location = new Point(3, 116);
            flowLayoutPanel12.Margin = new Padding(8, 12, 8, 12);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            flowLayoutPanel12.Size = new Size(870, 134);
            flowLayoutPanel12.TabIndex = 29;
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.AutoSize = true;
            flowLayoutPanel7.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel7.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel7.Controls.Add(flowLayoutPanel6);
            flowLayoutPanel7.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel7.Location = new Point(3, 3);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            flowLayoutPanel7.Size = new Size(399, 128);
            flowLayoutPanel7.TabIndex = 24;
            flowLayoutPanel7.WrapContents = false;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(lblLogLevel);
            flowLayoutPanel2.Controls.Add(cmbLogLevel);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(393, 29);
            flowLayoutPanel2.TabIndex = 19;
            flowLayoutPanel2.WrapContents = false;
            // 
            // lblLogLevel
            // 
            lblLogLevel.Anchor = AnchorStyles.Left;
            lblLogLevel.AutoSize = true;
            lblLogLevel.Location = new Point(3, 7);
            lblLogLevel.Name = "lblLogLevel";
            lblLogLevel.Size = new Size(91, 15);
            lblLogLevel.TabIndex = 1;
            lblLogLevel.Text = "Уровень логов:";
            lblLogLevel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbLogLevel
            // 
            cmbLogLevel.Anchor = AnchorStyles.Left;
            cmbLogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLogLevel.Items.AddRange(new object[] { "trace", "debug", "info", "warn", "error" });
            cmbLogLevel.Location = new Point(100, 3);
            cmbLogLevel.Name = "cmbLogLevel";
            cmbLogLevel.Size = new Size(120, 23);
            cmbLogLevel.TabIndex = 2;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.Controls.Add(lblLogOutputPath);
            flowLayoutPanel4.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel4.Dock = DockStyle.Fill;
            flowLayoutPanel4.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel4.Location = new Point(3, 38);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(393, 52);
            flowLayoutPanel4.TabIndex = 21;
            flowLayoutPanel4.WrapContents = false;
            // 
            // lblLogOutputPath
            // 
            lblLogOutputPath.Anchor = AnchorStyles.Left;
            lblLogOutputPath.AutoSize = true;
            lblLogOutputPath.Location = new Point(3, 0);
            lblLogOutputPath.Name = "lblLogOutputPath";
            lblLogOutputPath.Size = new Size(180, 15);
            lblLogOutputPath.TabIndex = 5;
            lblLogOutputPath.Text = "Путь вывода (если file/custom):";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(txtLogOutputPath);
            flowLayoutPanel3.Controls.Add(btnBrowseLogPath);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(3, 18);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(387, 31);
            flowLayoutPanel3.TabIndex = 20;
            // 
            // txtLogOutputPath
            // 
            txtLogOutputPath.Anchor = AnchorStyles.Left;
            txtLogOutputPath.Location = new Point(3, 4);
            txtLogOutputPath.Name = "txtLogOutputPath";
            txtLogOutputPath.Size = new Size(300, 23);
            txtLogOutputPath.TabIndex = 6;
            txtLogOutputPath.Visible = false;
            // 
            // btnBrowseLogPath
            // 
            btnBrowseLogPath.Anchor = AnchorStyles.Left;
            btnBrowseLogPath.AutoSize = true;
            btnBrowseLogPath.Location = new Point(309, 3);
            btnBrowseLogPath.Name = "btnBrowseLogPath";
            btnBrowseLogPath.Size = new Size(75, 25);
            btnBrowseLogPath.TabIndex = 7;
            btnBrowseLogPath.Text = "Обзор...";
            btnBrowseLogPath.Visible = false;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.Controls.Add(lblLogOutput);
            flowLayoutPanel6.Controls.Add(cmbLogOutput);
            flowLayoutPanel6.Dock = DockStyle.Fill;
            flowLayoutPanel6.Location = new Point(3, 96);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(393, 29);
            flowLayoutPanel6.TabIndex = 23;
            flowLayoutPanel6.WrapContents = false;
            // 
            // lblLogOutput
            // 
            lblLogOutput.Anchor = AnchorStyles.Left;
            lblLogOutput.AutoSize = true;
            lblLogOutput.Location = new Point(3, 7);
            lblLogOutput.Name = "lblLogOutput";
            lblLogOutput.Size = new Size(80, 15);
            lblLogOutput.TabIndex = 3;
            lblLogOutput.Text = "Вывод логов:";
            lblLogOutput.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cmbLogOutput
            // 
            cmbLogOutput.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLogOutput.Items.AddRange(new object[] { "console", "file", "custom" });
            cmbLogOutput.Location = new Point(89, 3);
            cmbLogOutput.Name = "cmbLogOutput";
            cmbLogOutput.Size = new Size(120, 23);
            cmbLogOutput.TabIndex = 4;
            // 
            // flowLayoutPanel11
            // 
            flowLayoutPanel11.AutoSize = true;
            flowLayoutPanel11.Controls.Add(flowLayoutPanel10);
            flowLayoutPanel11.Controls.Add(flowLayoutPanel8);
            flowLayoutPanel11.Controls.Add(flowLayoutPanel9);
            flowLayoutPanel11.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel11.Location = new Point(408, 3);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            flowLayoutPanel11.Size = new Size(377, 105);
            flowLayoutPanel11.TabIndex = 28;
            flowLayoutPanel11.WrapContents = false;
            // 
            // flowLayoutPanel10
            // 
            flowLayoutPanel10.AutoSize = true;
            flowLayoutPanel10.Controls.Add(lblKeepLastLines);
            flowLayoutPanel10.Controls.Add(numKeepLastLines);
            flowLayoutPanel10.Dock = DockStyle.Fill;
            flowLayoutPanel10.Location = new Point(3, 3);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            flowLayoutPanel10.Size = new Size(371, 29);
            flowLayoutPanel10.TabIndex = 27;
            flowLayoutPanel10.WrapContents = false;
            // 
            // lblKeepLastLines
            // 
            lblKeepLastLines.Anchor = AnchorStyles.Left;
            lblKeepLastLines.AutoSize = true;
            lblKeepLastLines.Location = new Point(3, 7);
            lblKeepLastLines.Name = "lblKeepLastLines";
            lblKeepLastLines.Size = new Size(257, 15);
            lblKeepLastLines.TabIndex = 12;
            lblKeepLastLines.Text = "Сохранять последних строк при автоочистке:";
            lblKeepLastLines.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numKeepLastLines
            // 
            numKeepLastLines.Anchor = AnchorStyles.Left;
            numKeepLastLines.Location = new Point(266, 3);
            numKeepLastLines.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numKeepLastLines.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numKeepLastLines.Name = "numKeepLastLines";
            numKeepLastLines.Size = new Size(80, 23);
            numKeepLastLines.TabIndex = 13;
            numKeepLastLines.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.AutoSize = true;
            flowLayoutPanel8.Controls.Add(lblAutoClear);
            flowLayoutPanel8.Controls.Add(numAutoClearMinutes);
            flowLayoutPanel8.Dock = DockStyle.Fill;
            flowLayoutPanel8.Location = new Point(3, 38);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            flowLayoutPanel8.Size = new Size(371, 29);
            flowLayoutPanel8.TabIndex = 25;
            flowLayoutPanel8.WrapContents = false;
            // 
            // lblAutoClear
            // 
            lblAutoClear.Anchor = AnchorStyles.Left;
            lblAutoClear.AutoSize = true;
            lblAutoClear.Location = new Point(3, 7);
            lblAutoClear.Name = "lblAutoClear";
            lblAutoClear.Size = new Size(213, 15);
            lblAutoClear.TabIndex = 10;
            lblAutoClear.Text = "Автоочистка (минут), 0 = отключено:";
            lblAutoClear.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numAutoClearMinutes
            // 
            numAutoClearMinutes.Anchor = AnchorStyles.Left;
            numAutoClearMinutes.Location = new Point(222, 3);
            numAutoClearMinutes.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numAutoClearMinutes.Name = "numAutoClearMinutes";
            numAutoClearMinutes.Size = new Size(80, 23);
            numAutoClearMinutes.TabIndex = 11;
            // 
            // flowLayoutPanel9
            // 
            flowLayoutPanel9.AutoSize = true;
            flowLayoutPanel9.Controls.Add(lblTailLines);
            flowLayoutPanel9.Controls.Add(numTailLinesToShow);
            flowLayoutPanel9.Dock = DockStyle.Fill;
            flowLayoutPanel9.Location = new Point(3, 73);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            flowLayoutPanel9.Size = new Size(371, 29);
            flowLayoutPanel9.TabIndex = 26;
            flowLayoutPanel9.WrapContents = false;
            // 
            // lblTailLines
            // 
            lblTailLines.Anchor = AnchorStyles.Left;
            lblTailLines.AutoSize = true;
            lblTailLines.Location = new Point(3, 7);
            lblTailLines.Name = "lblTailLines";
            lblTailLines.Size = new Size(279, 15);
            lblTailLines.TabIndex = 14;
            lblTailLines.Text = "Показывать последних строк (по умолчанию 50):";
            lblTailLines.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numTailLinesToShow
            // 
            numTailLinesToShow.Anchor = AnchorStyles.Left;
            numTailLinesToShow.Location = new Point(288, 3);
            numTailLinesToShow.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numTailLinesToShow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTailLinesToShow.Name = "numTailLinesToShow";
            numTailLinesToShow.Size = new Size(80, 23);
            numTailLinesToShow.TabIndex = 15;
            numTailLinesToShow.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.Controls.Add(chkClearLogsOnStart);
            flowLayoutPanel5.Controls.Add(chkAutoScroll);
            flowLayoutPanel5.Controls.Add(chkEnableLogging);
            flowLayoutPanel5.Dock = DockStyle.Top;
            flowLayoutPanel5.Location = new Point(3, 81);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(870, 35);
            flowLayoutPanel5.TabIndex = 22;
            // 
            // chkClearLogsOnStart
            // 
            chkClearLogsOnStart.AutoSize = true;
            chkClearLogsOnStart.Location = new Point(12, 8);
            chkClearLogsOnStart.Margin = new Padding(12, 8, 12, 8);
            chkClearLogsOnStart.Name = "chkClearLogsOnStart";
            chkClearLogsOnStart.Size = new Size(224, 19);
            chkClearLogsOnStart.TabIndex = 9;
            chkClearLogsOnStart.Text = "Очищать логи при запуске sing-box";
            // 
            // chkAutoScroll
            // 
            chkAutoScroll.AutoSize = true;
            chkAutoScroll.Checked = true;
            chkAutoScroll.CheckState = CheckState.Checked;
            chkAutoScroll.Location = new Point(260, 8);
            chkAutoScroll.Margin = new Padding(12, 8, 12, 8);
            chkAutoScroll.Name = "chkAutoScroll";
            chkAutoScroll.Size = new Size(92, 19);
            chkAutoScroll.TabIndex = 1;
            chkAutoScroll.Text = "Автоскролл";
            // 
            // chkEnableLogging
            // 
            chkEnableLogging.AutoSize = true;
            chkEnableLogging.Location = new Point(376, 8);
            chkEnableLogging.Margin = new Padding(12, 8, 12, 8);
            chkEnableLogging.Name = "chkEnableLogging";
            chkEnableLogging.Size = new Size(156, 19);
            chkEnableLogging.TabIndex = 0;
            chkEnableLogging.Text = "Включить логирование";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(lblCurrentLogFile);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel13);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(870, 78);
            flowLayoutPanel1.TabIndex = 19;
            flowLayoutPanel1.WrapContents = false;
            // 
            // lblCurrentLogFile
            // 
            lblCurrentLogFile.AutoSize = true;
            lblCurrentLogFile.Dock = DockStyle.Fill;
            lblCurrentLogFile.Location = new Point(12, 8);
            lblCurrentLogFile.Margin = new Padding(12, 8, 12, 8);
            lblCurrentLogFile.Name = "lblCurrentLogFile";
            lblCurrentLogFile.Size = new Size(353, 15);
            lblCurrentLogFile.TabIndex = 16;
            lblCurrentLogFile.Text = "Текущий лог: —";
            // 
            // flowLayoutPanel13
            // 
            flowLayoutPanel13.AutoSize = true;
            flowLayoutPanel13.Controls.Add(lblTimestamp);
            flowLayoutPanel13.Controls.Add(btnOpenLog);
            flowLayoutPanel13.Controls.Add(btnClearLog);
            flowLayoutPanel13.Location = new Point(3, 34);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            flowLayoutPanel13.Size = new Size(371, 41);
            flowLayoutPanel13.TabIndex = 30;
            flowLayoutPanel13.WrapContents = false;
            // 
            // lblTimestamp
            // 
            lblTimestamp.Anchor = AnchorStyles.Left;
            lblTimestamp.AutoSize = true;
            lblTimestamp.Location = new Point(12, 13);
            lblTimestamp.Margin = new Padding(12, 8, 12, 8);
            lblTimestamp.Name = "lblTimestamp";
            lblTimestamp.Size = new Size(122, 15);
            lblTimestamp.TabIndex = 8;
            lblTimestamp.Text = "Timestamp: включён";
            // 
            // btnOpenLog
            // 
            btnOpenLog.AutoSize = true;
            btnOpenLog.Location = new Point(158, 8);
            btnOpenLog.Margin = new Padding(12, 8, 12, 8);
            btnOpenLog.Name = "btnOpenLog";
            btnOpenLog.Size = new Size(86, 25);
            btnOpenLog.TabIndex = 17;
            btnOpenLog.Text = "Открыть лог";
            // 
            // btnClearLog
            // 
            btnClearLog.AutoSize = true;
            btnClearLog.Location = new Point(268, 8);
            btnClearLog.Margin = new Padding(12, 8, 12, 8);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(91, 25);
            btnClearLog.TabIndex = 18;
            btnClearLog.Text = "Очистить лог";
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = trayMenu;
            notifyIcon.Text = "Client SandBox";
            notifyIcon.MouseDoubleClick += notifyIcon_DoubleClick;
            // 
            // trayMenu
            // 
            trayMenu.Items.AddRange(new ToolStripItem[] { miOpen, miStart, miStop, miRestart, miExit });
            trayMenu.Name = "trayMenu";
            trayMenu.Size = new Size(156, 114);
            trayMenu.Opening += trayMenu_Opening;
            // 
            // miOpen
            // 
            miOpen.Name = "miOpen";
            miOpen.Size = new Size(155, 22);
            miOpen.Text = "Открыть";
            miOpen.Click += miOpen_Click;
            // 
            // miStart
            // 
            miStart.Name = "miStart";
            miStart.Size = new Size(155, 22);
            miStart.Text = "Запустить";
            miStart.Click += miStart_Click;
            // 
            // miStop
            // 
            miStop.Name = "miStop";
            miStop.Size = new Size(155, 22);
            miStop.Text = "Остановить";
            miStop.Click += miStop_Click;
            // 
            // miRestart
            // 
            miRestart.Name = "miRestart";
            miRestart.Size = new Size(155, 22);
            miRestart.Text = "Перезапустить";
            miRestart.Click += miRestart_Click;
            // 
            // miExit
            // 
            miExit.Name = "miExit";
            miExit.Size = new Size(155, 22);
            miExit.Text = "Выход";
            miExit.Click += miExit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(tabMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1200, 800);
            MinimumSize = new Size(900, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SingBox Manager";
            tabMain.ResumeLayout(false);
            settingPage.ResumeLayout(false);
            settingPage.PerformLayout();
            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel14.PerformLayout();
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
            tabPageConnections.ResumeLayout(false);
            tabPageConnections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridConnections).EndInit();
            cmsConnections.ResumeLayout(false);
            fLConnectionsControlPanel.ResumeLayout(false);
            fLConnectionsControlPanel.PerformLayout();
            logsPage.ResumeLayout(false);
            logsPage.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numKeepLastLines).EndInit();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAutoClearMinutes).EndInit();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTailLinesToShow).EndInit();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel13.ResumeLayout(false);
            flowLayoutPanel13.PerformLayout();
            trayMenu.ResumeLayout(false);
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
        private Button btnRestoreNetwork;
        private Label lblPidInf;
        private Label labelPIDCaption;
        // Logs tab controls
        private TableLayoutPanel logsTable;
        // row panels removed; controls are placed directly on logsPage
        private Label lblCurrentLogFile;
        private Button btnOpenLog;
        private RichTextBox rtbLogViewer;
        private CheckBox chkAutoScroll;
        private CheckBox chkEnableLogging;
        private Label lblLogLevel;
        private ComboBox cmbLogLevel;
        private Label lblLogOutput;
        private ComboBox cmbLogOutput;
        private Label lblLogOutputPath;
        private TextBox txtLogOutputPath;
        private Button btnBrowseLogPath;
        private Label lblTimestamp;
        private CheckBox chkClearLogsOnStart;
        private Label lblAutoClear;
        private NumericUpDown numAutoClearMinutes;
        private Label lblKeepLastLines;
        private NumericUpDown numKeepLastLines;
        private Label lblTailLines;
        private NumericUpDown numTailLinesToShow;

        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem miOpen;
        private ToolStripMenuItem miStart;
        private ToolStripMenuItem miStop;
        private ToolStripMenuItem miRestart;
        private ToolStripMenuItem miExit;
        private TabPage tabPageConnections;
        private FlowLayoutPanel fLConnectionsControlPanel;
        private Button btnAddConnection;
        private DataGridView gridConnections;
        private DataGridViewCheckBoxColumn colEnabled;
        private DataGridViewTextBoxColumn colTag;
        private DataGridViewTextBoxColumn colType;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colInfo;
        private ContextMenuStrip cmsConnections;
        private ToolStripMenuItem miSelectConnection;
        private ToolStripMenuItem miEditConnection;
        private ToolStripMenuItem miDeleteConnection;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel7;
        private FlowLayoutPanel flowLayoutPanel6;
        private FlowLayoutPanel flowLayoutPanel10;
        private FlowLayoutPanel flowLayoutPanel9;
        private FlowLayoutPanel flowLayoutPanel8;
        private FlowLayoutPanel flowLayoutPanel12;
        private FlowLayoutPanel flowLayoutPanel11;
        private FlowLayoutPanel flowLayoutPanel13;
        private Button btnClearLog;
        private CheckBox chkStartWithWindows;
        private FlowLayoutPanel flowLayoutPanel14;
    }
}