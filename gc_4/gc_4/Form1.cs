using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen penred = new Pen(Color.Red, 1);
            Random rnd = new Random();
            int n = rnd.Next(10, 50);
            Point[] p = new Point[n];
            p[0].X = rnd.Next(10, this.ClientSize.Width - 10);
            p[0].Y = rnd.Next(10, this.ClientSize.Height - 10);
            e.Graphics.DrawEllipse(penred, p[0].X - 2, p[0].Y - 2, 4, 4);
            int nord = 0, vest = 0, est = 0, sud = 0;
            for (int i = 1; i < n; i++)
            {
                p[i].X = rnd.Next(10, this.ClientSize.Width - 10);
                p[i].Y = rnd.Next(10, this.ClientSize.Height - 10);
                e.Graphics.DrawEllipse(penred, p[i].X - 2, p[i].Y - 2, 4, 4);
            }
            //sortare dupa X//
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (p[i].X < p[j].X)
                        (p[i], p[j]) = (p[j], p[i]);
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (p[vest].X > p[i].X)
                    vest = i;
                if (p[est].X < p[i].X)
                    est = i;
                if (p[nord].Y > p[i].Y)
                    nord = i;
                if (p[sud].Y < p[i].Y)
                    sud = i;
            }
            e.Graphics.DrawEllipse(penred, p[nord].X - 4, p[nord].Y - 4, 8, 8);
            e.Graphics.DrawEllipse(penred, p[sud].X - 4, p[sud].Y - 4, 8, 8);
            e.Graphics.DrawEllipse(penred, p[est].X - 4, p[est].Y - 4, 8, 8);
            e.Graphics.DrawEllipse(penred, p[vest].X - 4, p[vest].Y - 4, 8, 8);

            Pen penblue = new Pen(Color.Blue, 2);
            Pen pengreen = new Pen(Color.Goldenrod, 2);

            int aux11 = vest, aux12 = 0;        
            for (int i = 0; i < n; i++)
                if (p[i].X > p[aux11].X)
                {
                    aux12 = i;
                    break;
                }
            int aux21 = est, aux22 = 0;         
            for (int i = 0; i < n; i++)
                if (p[i].X < p[aux21].X)
                {
                    aux22 = i;
                    break;
                }
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (p[i].X > p[aux11].X)    
                    {
                        float m1, m2;
                        m1 = (float)(p[aux11].Y - p[aux12].Y) / (p[aux11].X - p[aux12].X);
                        m2 = (float)(p[aux11].Y - p[i].Y) / (p[aux11].X - p[i].X);           //e - v
                        if (m1 < m2)
                            aux12 = i;
                        //e.Graphics.DrawLine(penred, p[aux1], p[i]);
                    }
                    if (p[i].X < p[aux21].X)    
                    {
                        float m1, m2;
                        m1 = (float)(p[aux21].Y - p[aux22].Y) / (p[aux21].X - p[aux22].X);
                        m2 = (float)(p[aux21].Y - p[i].Y) / (p[aux21].X - p[i].X);               //v - e
                        if (m1 < m2)
                            aux22 = i;
                        //e.Graphics.DrawLine(penred, p[aux1], p[i]);
                    }
                }
                e.Graphics.DrawLine(penblue, p[aux11], p[aux12]);
                aux11 = aux12;
                for (int i = 1; i < n; i++)     
                    if (p[i].X > p[aux11].X)
                    {
                        aux12 = i;
                        break;
                    }
                e.Graphics.DrawLine(pengreen, p[aux21], p[aux22]);
                aux21 = aux22;
                for (int i = 0; i < n; i++)     
                    if (p[i].X < p[aux21].X)
                    {
                        aux22 = i; 
                        break;
                    }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
        
    

