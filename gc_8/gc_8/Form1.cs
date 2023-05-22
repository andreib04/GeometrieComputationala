using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_8
{
    public partial class Form1 : Form
    {

        Bitmap bmp;
        Graphics g;
        List<Point> points = new List<Point>();
        public Form1()
        {
            InitializeComponent();
        }

        private void pcbDisplay_Click(object sender, EventArgs e)
        {
            GetInput();

            Color[] resourceColors = { Color.Yellow, Color.Green, Color.Purple };

            List<CustomGeometry.Segment> diagonals = new List<CustomGeometry.Segment>();
            List<CustomGeometry.Triangle> triangles = new List<CustomGeometry.Triangle>();
            Dictionary<Point, int> colors = new Dictionary<Point, int>();

            foreach(var p in points)
            {
                colors.Add(p, -1);
            }

            while (points.Count > 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    bool ok = true;
                    int iPlusUnu = (i + 1) % points.Count;
                    int iPlusDoi = (i + 2) % points.Count;

                    for (int k = 0; k < points.Count - 1; k++)
                    {
                        if (points[i] != points[k] && points[i] != points[k + 1] && points[iPlusDoi] != points[k] && points[iPlusDoi] != points[k + 1])
                        {
                            if (CustomGeometry.DoIntersect(points[i], points[iPlusDoi], points[k], points[k + 1]))
                            {
                                ok = false;
                                break;
                            }
                        }
                    }

                    int iMinusUnu = (points.Count + i - 1) % points.Count; //in loc de i - 1 ca sa nu am out of range exception
                    if (CustomGeometry.GetOrientation(points[iMinusUnu], points[i], points[iPlusUnu]) == 1)
                    {
                        int orientation1 = CustomGeometry.GetOrientation(points[i], points[iPlusDoi], points[iPlusUnu]);
                        int orientation2 = CustomGeometry.GetOrientation(points[i], points[iMinusUnu], points[iPlusDoi]);
                        if (orientation1 != -1 || orientation2 != -1)
                        {
                            ok = false;
                        }
                    }
                    else
                    {
                        int orientation1 = CustomGeometry.GetOrientation(points[i], points[iPlusDoi], points[iPlusUnu]);
                        int orientation2 = CustomGeometry.GetOrientation(points[i], points[iMinusUnu], points[iPlusDoi]);
                        if (orientation1 == 1 && orientation2 == 1)
                        {
                            ok = false;
                        }
                    }

                    if (ok)
                    {
                        diagonals.Add(new CustomGeometry.Segment(points[i], points[iPlusDoi]));
                        triangles.Add(new CustomGeometry.Triangle(points[i], points[iPlusUnu], points[iPlusDoi]));
                        points.RemoveAt(iPlusUnu);
                        break;
                    }
                }

                //colors[points[0]] = 0;
                //colors[points[1]] = 1;
                //colors[points[2]] = 2;

                //triangles.Reverse();

                //foreach (var t in triangles)
                //{
                //    if (colors[t.A] == -1)
                //    {
                //        colors[t.A] = 3 - (colors[t.B] + colors[t.C]);
                //    }
                //    if (colors[t.B] == -1)
                //    {
                //        colors[t.B] = 3 - (colors[t.A] + colors[t.C]);
                //    }
                //    if (colors[t.C] == -1)
                //    {
                //        colors[t.C] = 3 - (colors[t.A] + colors[t.B]);
                //    }
                //}

                //foreach (var c in colors)
                //{
                //    g.DrawEllipse(new Pen(resourceColors[c.Value]), c.Key.X - 6, c.Key.Y - 6, 13, 13);
                //}
            }

            foreach (var d in diagonals)
            {
                g.DrawLine(Pens.Green, d.A, d.B);
            }

            RefreshImage();

        }

        private void GetInput()
        {
            using (InputForm inputForm = new InputForm(pcbDisplay.Size))
            {
                inputForm.ShowDialog();
                bmp = inputForm.RetrieveBitmap();
                points = inputForm.RetrievePointsList();
            }
            g = Graphics.FromImage(bmp);
        }

        private void RefreshImage()
        {
            pcbDisplay.Image = bmp;
        }
    }
}
