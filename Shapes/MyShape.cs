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

    public abstract class MyShape
    {
        protected Shape shape;
        protected Point startPoint;
        public double Width { get { return shape.Width; } set { shape.Width = value; } }
        public double Height { get { return shape.Height; } set { shape.Height = value; } }

        public Brush Stroke { get { return shape.Stroke; } set { shape.Stroke = value; } }
        public Brush Fill { get { return shape.Fill; } set { shape.Fill = value; } }
        public double StrokeThickness { get { return shape.StrokeThickness; } set { shape.StrokeThickness = value; } }

        public MyShape(Point startPoint)
        {
            this.startPoint = startPoint;
        }

        abstract public void Draw(Point currentPoint);
        public Shape GetShape()
        {
            return shape;
        }

    }

    public abstract class MyPolyShape : MyShape
    {
        protected PointCollection points;

        protected bool isPolyMode;
        public bool IsPolyMode { get { return isPolyMode; } set { isPolyMode = value; } }

        public MyPolyShape(Point startPoint):base(startPoint)
        {
            isPolyMode = false;
            points = new PointCollection
            {
                startPoint
            };
        }

    }
}
