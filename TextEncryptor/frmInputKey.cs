using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zemna.TextEncryptor.Enums;

namespace Zemna.TextEncryptor
{
    public partial class frmInputKey : Form
    {
        /// <summary>
        /// Gets the security key
        /// </summary>
        public string Key { get; private set; }

        private InputKeyModes mode;

        public frmInputKey(InputKeyModes mode)
        {
            InitializeComponent();

            this.mode = mode;
        }

        private void frmInputKey_Load(object sender, EventArgs e)
        {
            switch (this.mode)
            {
                case InputKeyModes.Save:
                    lblDesc.Text = "You have to use this security key when open encrypted file.";
                    break;
                case InputKeyModes.Open:
                    lblDesc.Text = "Please input security key when you make this file.";
                    break;
            }

            txtKey.Focus();
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = txtKey.TextLength > 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Key = txtKey.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmInputKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
