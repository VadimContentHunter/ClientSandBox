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
            controlPage = new TabPage();
            logsPage = new TabPage();
            tabMain.SuspendLayout();
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
            settingPage.Location = new Point(4, 24);
            settingPage.Name = "settingPage";
            settingPage.Padding = new Padding(3);
            settingPage.Size = new Size(876, 583);
            settingPage.TabIndex = 0;
            settingPage.Text = "Настройки";
            settingPage.UseVisualStyleBackColor = true;
            settingPage.Click += tabPage1_Click;
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
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage settingPage;
        private TabPage controlPage;
        private TabPage logsPage;
    }
}