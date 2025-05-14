using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace DrawZone.Shapes
{
    class CustomEllipse : CustomShape
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

        public CustomEllipse(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            ellipse = new Ellipse();
            ellipse.Width = 0;
            ellipse.Height = 0;
            ellipse.Stroke = stroke;
            ellipse.Fill = fill;
            ellipse.StrokeThickness = strokeThickness;
        }

        public CustomEllipse() : this(new Point(0,0), Brushes.Black, Brushes.Black, 0) {}

        public override Shape GetShape()
        {
            return ellipse;
        }

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                Left = startPoint.X,
                Top = startPoint.Y,
                Fill = ellipse.Fill.ToString(),
                Stroke = ellipse.Stroke.ToString(),
                StrokeThickness = ellipse.StrokeThickness,
                Width = ellipse.Width,
                Height = ellipse.Height
            });
        }

        public override void Deserialize(string json)
        {
            var baseData = JsonConvert.DeserializeAnonymousType(json, new
            {
                Left = 0.0,
                Top = 0.0,
                Fill = "",
                Stroke = "",
                StrokeThickness = 0.0,
                Width = 0.0,
                Height = 0.0
            });

            ellipse.Fill = new BrushConverter().ConvertFromString(baseData.Fill) as Brush;
            ellipse.Stroke = new BrushConverter().ConvertFromString(baseData.Stroke) as Brush;
            ellipse.StrokeThickness = baseData.StrokeThickness;
            ellipse.Width = baseData.Width;
            ellipse.Height = baseData.Height;
            Canvas.SetLeft(ellipse, baseData.Left);
            Canvas.SetTop(ellipse, baseData.Top);
            startPoint = new Point(baseData.Left, baseData.Top);
        }

    }
}

