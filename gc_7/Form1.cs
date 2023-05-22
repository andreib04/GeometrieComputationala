using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_7
{
    public partial class Form1 : Form
    {
        int n = 0;
        bool enableDrawing;
        Bitmap btm;
        Graphics g;
        public List<PointF> points = new List<PointF>();
        List<Segmente> diagonale = new List<Segmente>();
        List<Segmente> frontiera = new List<Segmente>();

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (enableDrawing)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                PointF aux = me.Location;
                points.Add(aux);
                n++;
                if (n > 1)
                    g.DrawLine(new Pen(Color.Black), points[points.Count - 2], points[points.Count - 1]);
                //g.FillEllipse(new SolidBrush(Color.Pink), aux.X, aux.Y, 20, 20);
                g.DrawString(n.ToString(), new Font(FontFamily.GenericSerif, 5), new SolidBrush(Color.Black), aux.X, aux.Y);
                pictureBox1.Image = btm;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(btm);
            enableDrawing = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (n > 1)
            {
                g.DrawLine(new Pen(Color.Black), points[points.Count - 1], points[0]);
                frontiera.Add(new Segmente(points[points.Count - 2], points[points.Count - 1]));
                pictureBox1.Image = btm;
                enableDrawing = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int diagonaleCount = 0;
            bool run = true;
            for (int i = 0; i < points.Count - 2; i++)
            {
                if (run)
                    for (int j = i + 2; j < points.Count; j++)
                    {
                        if (i == 0 && j == points.Count - 1) break;
                        Segmente aux = new Segmente(points[i], points[j]);
                        if (!IntersecteazaLaturaNeincidenta(aux))
                            if (!IntersecteazaDiagonaleAnterioare(aux))
                                if (EsteInPoligon(i, j))
                                {
                                    diagonaleCount++;
                                    diagonale.Add(aux);
                                    g.DrawLine(new Pen(Color.Blue), points[i], points[j]);
                                }
                        if (diagonaleCount == points.Count - 3)
                        {
                            run = false;
                            break;
                        }

                    }
                else break;
            }
            pictureBox1.Image = btm;
        }

        private bool MergeSpreStanga(List<PointF> points)
        {
            if (points[2].X > points[0].X)
                return false;
            return true;
        }

        private bool EsteInPoligon(int i, int j)
        {
            if (EsteVarfConvex(i))
            {
                if (MergeSpreStanga(points))
                {
                    if (Determinant(points[i], points[j], points[i + 1]) < 0 && Determinant(points[i], points[(i - 1 + points.Count) % points.Count], points[j]) < 0)
                        return true;
                }
                else
                    if (Determinant(points[i], points[j], points[i + 1]) > 0 && Determinant(points[i], points[(i - 1 + points.Count) % points.Count], points[j]) > 0)
                    return true;
            }
            else
            {
                if (MergeSpreStanga(points))
                {
                    if (Determinant(points[i], points[j], points[i + 1]) < 0 || Determinant(points[i], points[(i - 1 + points.Count) % points.Count], points[j]) < 0)
                        return true;
                }
                else
                {
                    if (Determinant(points[i], points[j], points[i + 1]) > 0 || Determinant(points[i], points[(i - 1 + points.Count) % points.Count], points[j]) > 0)
                        return true;
                }
            }
            return false;
        }

        private bool EsteVarfConvex(int i)
        {
            if (MergeSpreStanga(points))
            {
                if (Determinant(points[(i - 1 + points.Count) % points.Count], points[i], points[(i + 1 + points.Count) % points.Count]) > 0)
                    return true;
                return false;
            }
            else
            {
                if (Determinant(points[(i - 1 + points.Count) % points.Count], points[i], points[(i + 1 + points.Count) % points.Count]) > 0)
                    return false;
                return true;
            }
        }

        private bool IntersecteazaDiagonaleAnterioare(Segmente aux)
        {
            for (int i = 0; i < diagonale.Count; i++)
            {
                if (aux != diagonale[i])
                    if (IntersectWith(aux, diagonale[i]))
                        return true;
            }
            return false;
        }

        private bool IntersecteazaLaturaNeincidenta(Segmente aux)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (aux.Start != points[i] && aux.Start != points[i + 1] && aux.End != points[i] && aux.End != points[i + 1])
                    if (IntersectWith(aux, new Segmente(points[i], points[i + 1])))
                        return true;
            }

            if (aux.Start != points[0] && aux.Start != points[0] && aux.End != points[points.Count - 1] && aux.End != points[points.Count - 1])
                if (IntersectWith(aux, new Segmente(points[0], points[points.Count - 1])))
                    return true;
            return false;
        }
        float Determinant(PointF A, PointF B, PointF C)
        {
            return A.X * B.Y + B.X * C.Y + A.Y * C.X - B.Y * C.X - A.X * C.Y - A.Y * B.X;
        }
        private bool IntersectWith(Segmente seg, Segmente segment)
        {
            if (seg.Start != segment.Start && seg.Start != segment.End && seg.End != segment.Start && seg.End != segment.End)
                if (Determinant(segment.Start, segment.End, seg.Start) * Determinant(segment.Start, segment.End, seg.End) <= 0)
                    if (Determinant(seg.Start, seg.End, segment.Start) * Determinant(seg.Start, seg.End, segment.End) <= 0)
                        return true;
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            points.Clear();
            g.Clear(Color.White);
            pictureBox1.Image = btm;
            enableDrawing = true;
            diagonale.Clear();
            n = 0;
        }
    }
}
