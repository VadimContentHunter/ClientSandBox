using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientSandBox.Forms
{
    public partial class ConnectionEditForm : Form
    {
        public ConnectionEditForm()
        {
            InitializeComponent();

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        /// <summary>
        /// JSON подключения.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ConnectionJson
        {
            get => txtJson.Text;
            set => txtJson.Text = value;
        }

        /// <summary>
        /// Подтверждает редактирование.
        /// </summary>
        private void BtnSave_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Отменяет редактирование.
        /// </summary>
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
