namespace ClientSandBox.Forms
{
    partial class ConnectionEditForm
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
            groupBoxJsonConnecting = new GroupBox();
            txtJson = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();
            groupBoxJsonConnecting.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxJsonConnecting
            // 
            groupBoxJsonConnecting.Controls.Add(txtJson);
            groupBoxJsonConnecting.Dock = DockStyle.Fill;
            groupBoxJsonConnecting.Location = new Point(0, 0);
            groupBoxJsonConnecting.Name = "groupBoxJsonConnecting";
            groupBoxJsonConnecting.Size = new Size(434, 461);
            groupBoxJsonConnecting.TabIndex = 0;
            groupBoxJsonConnecting.TabStop = false;
            groupBoxJsonConnecting.Text = "JSON подключения";
            // 
            // txtJson
            // 
            txtJson.AcceptsReturn = true;
            txtJson.AcceptsTab = true;
            txtJson.Dock = DockStyle.Fill;
            txtJson.Font = new Font("Consolas", 10F);
            txtJson.Location = new Point(3, 19);
            txtJson.Multiline = true;
            txtJson.Name = "txtJson";
            txtJson.ScrollBars = ScrollBars.Both;
            txtJson.Size = new Size(428, 439);
            txtJson.TabIndex = 0;
            txtJson.WordWrap = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(btnSave);
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.Location = new Point(0, 432);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(434, 29);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(3, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 0;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(84, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // ConnectionEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 461);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(groupBoxJsonConnecting);
            MinimumSize = new Size(400, 200);
            Name = "ConnectionEditForm";
            Text = "ConnectionEditForm";
            groupBoxJsonConnecting.ResumeLayout(false);
            groupBoxJsonConnecting.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxJsonConnecting;
        private TextBox txtJson;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnSave;
        private Button btnCancel;
    }
}