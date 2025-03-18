using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyRectangle:MySingleShape
    {
        public override void Draw(MyPoint currentPoint) 
        {
            double x = Math.Min(startPoint.X, currentPoint.X);
            double y = Math.Min(startPoint.Y, currentPoint.Y);
            Width = Math.Abs(startPoint.X - currentPoint.X);
            Height = Math.Abs(startPoint.Y - currentPoint.Y);
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
        }

        public MyRectangle(MyPoint startPoint, SolidColorBrush brush, double thickness)
        {
            shape = new Rectangle
            {
                Stroke = brush,
                StrokeThickness = thickness
            };
            Width = 0;
            Height = 0;
            this.startPoint = startPoint;
        }
    }
}
