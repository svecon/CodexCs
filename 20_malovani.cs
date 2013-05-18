using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Pen pero = Pens.Blue;
        int minX = 0;
        int minY = 0;
        Random r;

        public Form1()
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap(1000, 1000);
            platno.Image = bmp;
            r = new Random();
        }



        private void platno_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Graphics g = Graphics.FromImage(platno.Image);
                g.DrawLine(pero, minX, minY, e.X, e.Y);
                Refresh();
            }
            minX = e.X;
            minY = e.Y;
            pero = new Pen(pero.Color, r.Next(100));
        }

        private void konecToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ulozitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                platno.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void nacistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                platno.Image = new Bitmap(openFileDialog1.FileName);
                Refresh();
            }
        }

        private void paleta_MouseDown(object sender, MouseEventArgs e)
        {
            pero = new Pen(
                ((Bitmap)paleta.Image).GetPixel(e.X, e.Y)
                );
        }

    }
}
