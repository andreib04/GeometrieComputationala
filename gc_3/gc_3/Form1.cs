using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           // Problema1(e);
            Problema2(e);
        }

        private void Problema1(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random rnd = new Random();
            int n = rnd.Next(70, 100);
            Pen pointsPen = new Pen(Color.Black, 2);
            Pen linePen = new Pen(Color.Red, 1);

            Point[] points = new Point[n];
            int x = 0, y = 0;
            for (int k = 0; k < n; k++)
            {
                x = rnd.Next(10, this.ClientSize.Width - 10);
                y = rnd.Next(10, this.ClientSize.Height - 10);
                points[k] = new Point(x, y);

                g.DrawEllipse(pointsPen, x, y, 2, 2);
            }

            int i, j;
            bool sorted;
            do
            {
                sorted = true;
                for (i = 0; i < points.Length - 1; i++)
                {
                    if (points[i].X > points[i + 1].X)
                    {
                        (points[i], points[i + 1]) = (points[i + 1], points[i]);
                        sorted = false;
                    }
                }
            }
            while (!sorted);

            bool[] used = new bool[points.Length];

            int pozA = new int();
            int pozB = new int();

            i = 0;

            while (i < points.Length - 1)
            {
                j = i + 1;
                double min = this.ClientSize.Width;

                while (j < points.Length)
                {
                    if (!(used[i] || used[j]))
                    {
                        double distance = Math.Sqrt(Math.Pow(x - points[j].X, 2) + Math.Pow(y - points[j].Y, 2));

                        if (distance < min)
                        {
                            min = distance;
                            pozA = i;
                            pozB = j;
                        }
                    }
                    j++;
                }
                g.DrawLine(linePen, points[pozA], points[pozB]);
                used[pozA] = true;
                used[pozB] = true;
                i++;
            }
            }

        private void Problema2(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            Random rnd = new Random();
            int n = rnd.Next(10, 15);
            Pen pointsPen = new Pen(Color.Black, 2);
            Pen linePen = new Pen(Color.Red, 1);
            Pen interPointsPen = new Pen(Color.Green);

            Point[] points = new Point[n];
            
            for (int l = 0; l < n; l++)
            {
                int x = rnd.Next(10, this.ClientSize.Width - 10);
                int y = rnd.Next(10, this.ClientSize.Height - 10);
                points[l] = new Point(x, y);

                g.DrawEllipse(pointsPen, x, y, 2, 2);
            }

            Segment[] segments = new Segment[n];

            int k = 0;
            for (int i = 0; i < n - 1; i += 2)
            {
                segments[k] = new Segment(points[i], points[i + 1]);
                g.DrawLine(linePen, points[i], points[i + 1]);
                k++;
            }

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    float? x, y;
                    if (get_line_intersection(segments[i].A.X, segments[i].A.Y, segments[i].B.X, segments[i].B.Y, segments[j].A.X, segments[j].A.Y, segments[j].B.X, segments[j].B.Y, out x, out y))
                    {
                        g.DrawEllipse(interPointsPen, (float)x - 5, (float)y - 5, 10, 10);
                    }
                }
            }

            
        }

        bool get_line_intersection(float p0_x, float p0_y, float p1_x, float p1_y,
        float p2_x, float p2_y, float p3_x, float p3_y, out float? i_x, out float? i_y)
        {
            i_x = 0;
            i_y = 0;
            float s1_x, s1_y, s2_x, s2_y;
            s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
            s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

            float s, t;
            s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
            t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                // Collision detected
                if (i_x != null)
                    i_x = (p0_x + (t * s1_x));
                if (i_y != null)
                    i_y = (p0_y + (t * s1_y));
                return true;
            }

            return false; // No collision
        }
        

        public struct Segment
        {
            public Point A, B;

            public Segment(Point a, Point b)
            {
                A = a;  
                B = b;
            }
        }

      
    }
}


