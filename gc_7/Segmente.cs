using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gc_7
{
    internal class Segmente
    {
        public PointF Start;
        public PointF End;
        public double lenght;
        public Segmente(Point S, Point E)
        {
            Start = S; End = E;
            lenght = Math.Sqrt(Math.Pow(E.X - S.X, 2) + Math.Pow(E.Y - S.Y, 2));
        }
        public Segmente(PointF S, PointF E)
        {
            Start = S; End = E;
            lenght = Math.Sqrt(Math.Pow(E.X - S.X, 2) + Math.Pow(E.Y - S.Y, 2));
        }
    }
}
