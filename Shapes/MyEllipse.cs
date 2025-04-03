using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyEllipse : MyShape
    {
        private Ellipse ellipse;
        public override void Draw(Point currentPoint)
        {
            double x = Math.Min(startPoint.X, currentPoint.X);
            double y = Math.Min(startPoint.Y, currentPoint.Y);
            ellipse.Width = Math.Abs(startPoint.X - currentPoint.X);
            ellipse.Height = Math.Abs(startPoint.Y - currentPoint.Y);
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
        }

        public MyEllipse(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            ellipse = new Ellipse();
            ellipse.Width = 0;
            ellipse.Height = 0;
            ellipse.Stroke = stroke;
            ellipse.Fill = fill;
            ellipse.StrokeThickness = strokeThickness;
        }

        public override Shape GetShape()
        {
            return ellipse;
        }
    }
}

