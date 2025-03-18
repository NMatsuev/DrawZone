using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyPoint
    {
        public double X, Y;

        public MyPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public MyPoint(System.Windows.Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public double FindDistanse(MyPoint point)
        {
            return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2));
        }

    }

    abstract class MyShape
    {
        protected Shape shape;
        public double Width { get { return shape.Width; } set { shape.Width = value; } }
        public double Height { get { return shape.Height; } set { shape.Height = value; } }

        abstract public void Draw(MyPoint currentPoint);
        public Shape GetShape()
        {
            return shape;
        }

    }

    abstract class MySingleShape : MyShape
    {
        protected MyPoint startPoint;
    }

    abstract class MyPolyShape : MyShape
    {
        protected List<MyPoint> points;
    }
}
