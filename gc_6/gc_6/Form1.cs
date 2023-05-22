using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_6
{
    public partial class Form1 : Form
    {
        Pen pen = new Pen(Color.Red, 2);
        Pen penline = new Pen(Color.Blue, 2);
        List<Point> points = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();

            g.DrawEllipse(pen, e.X, e.Y, 5, 5);
            points.Add(e.Location);
            if (points.Count > 1)
            {
                g.DrawLine(penline, points[points.Count - 1], points[points.Count - 2]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Red, 2);
            int x, y;
            Random rnd = new Random();

            int n = rnd.Next(5, 20);
            Point[] points = new Point[n];

            for(int i = 0; i < n; i++)
            {
                x = rnd.Next(100, this.ClientSize.Width - 100);
                y = rnd.Next(100, this.ClientSize.Height - 100); 
                points[i] = new Point(x, y);
                g.DrawEllipse(pen, x, y, 2, 2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 New = new Form1();
            New.Show();
            this.Dispose(false);
        }
    }
}
