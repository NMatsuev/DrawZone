using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyRightPolygon : MySingleShape
    {
        public override void Draw(MyPoint currentPoint)
        {
            int numberOfSides = 6;
            double radius = currentPoint.FindDistanse(startPoint);
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
            Canvas.SetLeft(shape, startPoint.X - Width / 2);
            Canvas.SetTop(shape, startPoint.Y - Height / 2);
        }

        public MyRightPolygon(MyPoint startPoint, SolidColorBrush brush, double thickness)
        {
            shape = new Polygon
            {
                Stroke = brush,
                StrokeThickness = thickness
            };
            this.startPoint = startPoint;
        }
    }
}
