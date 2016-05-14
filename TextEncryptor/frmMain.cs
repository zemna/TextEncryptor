using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Zemna.Security.Cryptography;
using Zemna.TextEncryptor.Enums;
using Zemna.TextEncryptor.Properties;

namespace Zemna.TextEncryptor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbConnStr.TextLength < 1)
            {
                MessageBox.Show("Please input connection string.", "Make", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbConnStr.Focus();
                return;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var frmInputKey = new frmInputKey(InputKeyModes.Save);
                if (frmInputKey.ShowDialog() != DialogResult.OK)
                    return;

                var key = frmInputKey.Key;

                string enc = DESCryptography.Encrypt(tbConnStr.Text, key);

                using (StreamWriter writer = File.CreateText(saveFileDialog1.FileName))
                {
                    writer.WriteLine(enc);

                    writer.Flush();
                    writer.Close();
                }

                MessageBox.Show("Save successfully.");
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var frmInputKey = new frmInputKey(InputKeyModes.Open);
                if (frmInputKey.ShowDialog() != DialogResult.OK)
                    return;

                var key = frmInputKey.Key;

                string dec = "";

                using (StreamReader reader = File.OpenText(openFileDialog1.FileName))
                {
                    dec = reader.ReadLine();
                    reader.Close();
                }

                try
                {
                    dec = DESCryptography.Decrypt(dec, key);

                    tbConnStr.Text = dec;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Incorrect file", "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (CryptographicException)
                {
                    MessageBox.Show("Wrong key", "Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.Default.help_url);
        }

        private void tbConnStr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                tbConnStr.SelectAll();
            }
        }
    }
}
