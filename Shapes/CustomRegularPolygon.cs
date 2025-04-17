using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawZone.Utils;

namespace DrawZone.Shapes
{
    class CustomRegularPolygon : CustomShape
    {
        private Polygon polygon;
        public override void Draw(Point currentPoint)
        {
            int numberOfSides = 5;
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

            polygon.Points = points;
            RectangleGeometry clipGeometry = new RectangleGeometry(new Rect(0, 0, ((Canvas)polygon.Parent).MaxWidth, ((Canvas)polygon.Parent).MaxHeight));
            polygon.Clip = clipGeometry;
            Canvas.SetLeft(polygon, startPoint.X - polygon.Width / 2);
            Canvas.SetTop(polygon, startPoint.Y - polygon.Height / 2);
        }

        public CustomRegularPolygon(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            polygon = new Polygon();
            polygon.Stroke = stroke;
            polygon.Fill = fill;
            polygon.StrokeThickness = strokeThickness;
        }

        public override Shape GetShape()
        {
            return polygon;
        }
    }
}
