using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Random rnd = new Random();
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red, 2);

            int x, y;
            int n = rnd.Next(30, 50);
            Point[] points = new Point[n];

            for(int i = 0; i < n; i++)
            {
                x = rnd.Next(10, this.ClientSize.Width - 10);
                y = rnd.Next(10, this.ClientSize.Height - 10);
                points[i] = new Point(x, y);
                g.DrawEllipse(pen, x, y, 2, 2);
            }

            List<Point> invel = new List<Point>();

            int stanga = 0;
            for(int i = 1; i < n; i++)
            {
                if (points[i].X < points[stanga].X)
                    stanga = i;
            }

            int p = stanga, q;
            do
            {
                invel.Add(points[p]);

                q = (p + 1) % n;

                for(int i = 0; i < n; i++)
                {
                    if (orientation(points[p], points[i], points[q]) == 2)
                    {
                        q = i;
                    }
                }

                p = q;

            } while (p != stanga);

            Pen penn = new Pen(Color.Blue, 2);
            for(int i = 0; i < invel.Count - 1; i++)
            {
                g.DrawLine(penn, invel[i], invel[i + 1]);
            }
           
            g.DrawLine(penn, invel[0], invel[invel.Count - 1]);
        }

        public static int orientation(Point p, Point q, Point r)
        {
            int val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0; // collinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
    }
}
