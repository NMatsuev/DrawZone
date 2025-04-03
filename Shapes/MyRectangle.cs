using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace DrawZone.Shapes
{
    class MyRectangle : MyShape
    {
        private Rectangle rect;
        public override void Draw(Point currentPoint) 
        {
            double x = Math.Min(startPoint.X, currentPoint.X);
            double y = Math.Min(startPoint.Y, currentPoint.Y);
            rect.Width = Math.Abs(startPoint.X - currentPoint.X);
            rect.Height = Math.Abs(startPoint.Y - currentPoint.Y);
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }

        public MyRectangle(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            rect = new Rectangle();
            rect.Width = 0;
            rect.Height = 0;
            rect.Stroke = stroke;
            rect.Fill = fill;
            rect.StrokeThickness = strokeThickness;
        }

        public override Shape GetShape()
        {
            return rect;
        }
    }
}
