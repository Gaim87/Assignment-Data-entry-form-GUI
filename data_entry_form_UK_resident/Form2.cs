using System;
using System.Drawing;
using System.Windows.Forms;

namespace data_entry_form_UK_resident
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 nextForm = new Form1();
            nextForm.ShowDialog();
            this.Close();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(14, 73, 117)), 0, 1, ClientRectangle.Width, ClientRectangle.Height);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            buttonNext.BackColor = Color.FromArgb(14, 73, 117);
            buttonNext.ForeColor = Color.FromArgb(255, 196, 107);
            buttonExit.BackColor = Color.FromArgb(14, 73, 117);
            buttonExit.ForeColor = Color.FromArgb(255, 196, 107);
            label1.BackColor = Color.FromArgb(14, 73, 117);
            label1.ForeColor = Color.FromArgb(255, 196, 107);
            menuStrip1.BackColor = Color.FromArgb(14, 73, 117);
            menuStrip1.ForeColor = Color.FromArgb(255, 196, 107);
            exitToolStripMenuItem.BackColor = Color.FromArgb(14, 73, 117);
            exitToolStripMenuItem.ForeColor = Color.FromArgb(255, 196, 107);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
