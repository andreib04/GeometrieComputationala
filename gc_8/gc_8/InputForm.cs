using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gc_8
{
    public partial class InputForm : Form
    {

        Bitmap bmp;
        Graphics g;
        List<Point> inputPoints;
        bool MouseOnFirstPoint;
        Pen pointPen = new Pen(Color.Black, 2);
        Pen linePen = new Pen(Color.Red, 2);
        public InputForm(Size pictureBoxSize)
        {
            InitializeComponent();
            inputPoints = new List<Point>();

            pcbInputBox.Size = pictureBoxSize;

            this.Size = this.MinimumSize = this.MaximumSize = new Size(pictureBoxSize.Width + 40, pictureBoxSize.Height + 120);
            btnClear.Location = new Point(pcbInputBox.Width / 2 - btnClear.Width - 5, pcbInputBox.Height + 30);
            btnDone.Location = new Point(pcbInputBox.Width / 2 + 5, pcbInputBox.Height + 30);

            bmp = new Bitmap(pcbInputBox.Width, pcbInputBox.Height);
            g = Graphics.FromImage(bmp);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pcbInputBox.Enabled = true;
            inputPoints.Clear();
            MouseOnFirstPoint = false;
            btnDone.Enabled = false;
            ClearGraphics();
            RefreshImage();
        }


        private void pcbInputBox_MouseMove(object sender, MouseEventArgs e)
        {
            int offset = 10; //controleaza proximitatea din care se poate apasa ultimul punct

            MouseEventArgs me = (MouseEventArgs)e;
            Point mousePosition = me.Location;

            if (inputPoints.Count > 2)
            {
                Point firstPoint = inputPoints[0];
                if (mousePosition.X > firstPoint.X - offset && mousePosition.X < firstPoint.X + offset && mousePosition.Y > firstPoint.Y - offset && mousePosition.Y < firstPoint.Y + offset)
                {
                    Cursor.Current = Cursors.Hand;
                    MouseOnFirstPoint = true;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MouseOnFirstPoint = false;
                }
            }
        }

        public List<Point> RetrievePointsList()
        {
            return inputPoints;
        }

        public Bitmap RetrieveBitmap()
        {
            return bmp;
        }


        void RefreshImage()
        {
            pcbInputBox.Image = bmp;
        }

        void ClearGraphics()
        {
            g.Clear(Color.White);
        }

        private void pcbInputBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point mousePosition = me.Location;

            if (!MouseOnFirstPoint)
            {
                inputPoints.Add(mousePosition);
                CustomGraphics.DrawPoint(g, pointPen, mousePosition);

                int count = inputPoints.Count;

                if (count > 1)
                {
                    g.DrawLine(linePen, inputPoints[count - 2], inputPoints[count - 1]);
                }
            }
            else
            {
                g.DrawLine(linePen, inputPoints[0], inputPoints[inputPoints.Count - 1]);
                pcbInputBox.Enabled = false;
                btnDone.Enabled = true;
            }


            RefreshImage();
        }
    }
}
