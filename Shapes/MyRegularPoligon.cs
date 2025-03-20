using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyRegularPolygon : MyShape
    {
        public override void Draw(Point currentPoint)
        {
            int numberOfSides = 6;
            double radius = PointExtensions.FindDistanse(startPoint, currentPoint);
            double centerX = startPoint.X;
            double centerY = startPoint.Y;

            PointCollection points = new PointCollection();
            for (int i = 0; i < numberOfSides; i++)
            {
                double angle = 2 * Math.PI / numberOfSides * i - Math.PI/2;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                points.Add(new Point(x, y));
            }

            ((Polygon)shape).Points = points;
            RectangleGeometry clipGeometry = new RectangleGeometry(new Rect(0, 0, ((Canvas)shape.Parent).MaxWidth, ((Canvas)shape.Parent).MaxHeight));
            shape.Clip = clipGeometry;
            Canvas.SetLeft(shape, startPoint.X - Width / 2);
            Canvas.SetTop(shape, startPoint.Y - Height / 2);
        }

        public MyRegularPolygon(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            shape = new Polygon();
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = strokeThickness;
        }
    }
}
