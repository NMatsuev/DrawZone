using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    public static class PointExtensions
    {
        public static double FindDistanse(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

    }

    abstract class MyShape
    {
        protected Shape shape;
        public double Width { get { return shape.Width; } set { shape.Width = value; } }
        public double Height { get { return shape.Height; } set { shape.Height = value; } }

        abstract public void Draw(Point currentPoint);
        public Shape GetShape()
        {
            return shape;
        }

    }

    abstract class MySingleShape : MyShape
    {
        protected Point startPoint;
    }

    abstract class MyPolyShape : MySingleShape
    {
        protected PointCollection points;

        protected bool isPolyMode;
        public bool IsPolyMode { get { return isPolyMode; } set { isPolyMode = value; } }

    }
}
