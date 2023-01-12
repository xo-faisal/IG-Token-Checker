using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokenChecker
{
    public partial class Form2 : Form
    {
        bool drag = false;
        Point s_p = new Point(0, 0);
        public Form2()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/xofaisal");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://instagram.com/dramaticspace/");
        }

        

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            s_p = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - s_p.X, p.Y - s_p.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.No;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Cursor = Cursors.Cross;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            Cursor = Cursors.Cross;
        }
    }
}
