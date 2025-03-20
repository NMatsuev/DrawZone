using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace DrawZone.Shapes
{
    class MyRectangle : MyShape
    {
        public override void Draw(Point currentPoint) 
        {
            double x = Math.Min(startPoint.X, currentPoint.X);
            double y = Math.Min(startPoint.Y, currentPoint.Y);
            Width = Math.Abs(startPoint.X - currentPoint.X);
            Height = Math.Abs(startPoint.Y - currentPoint.Y);
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
        }

        public MyRectangle(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            shape = new Rectangle();
            Width = 0;
            Height = 0;
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = strokeThickness;
        }
    }
}
