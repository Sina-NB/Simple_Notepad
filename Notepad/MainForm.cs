using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MainForm : Form
    {
        String path = String.Empty;

        public MainForm() => InitializeComponent();

        private void exitPrompt()
        {
            DialogResult = MessageBox.Show("Do you want to save current file?",
            "Notepad",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox.Text))
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                    saveToolStripMenuItem_Click(sender, e);

                textBox.Text = String.Empty;
                path = String.Empty;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = File.ReadAllText(path = openFileDialog.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(path = saveFileDialog.FileName, textBox.Text);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                File.WriteAllText(path, textBox.Text);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => textBox.SelectAll();

        private void cutToolStripMenuItem_Click(object sender, EventArgs e) => textBox.Cut();

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) => textBox.Copy();

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) => textBox.Paste();

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) => textBox.SelectedText = String.Empty;

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                textBox.WordWrap = false;
                textBox.ScrollBars = ScrollBars.Both;
                wordWrapToolStripMenuItem.Checked = false;
            }
            else
            {
                textBox.WordWrap = true;
                textBox.ScrollBars = ScrollBars.Vertical;
                wordWrapToolStripMenuItem.Checked = true;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Font = new Font(fontDialog.Font, fontDialog.Font.Style);
                textBox.ForeColor = fontDialog.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.ForeColor = Color.Black;
            textBox.BackColor = Color.White;
            this.BackColor = Color.White;
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.ForeColor = Color.White;
            textBox.BackColor = Color.Black;
            this.BackColor = Color.Gray;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size+1);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float newSize = textBox.Font.Size - 1;
            if (newSize>=2)
                textBox.Font = new Font(textBox.Font.FontFamily, newSize);
        }
    }
}
