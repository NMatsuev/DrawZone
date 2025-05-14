using System.Windows.Controls;
using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Media.Media3D;

namespace DrawZone.Shapes
{
    class CustomRectangle : CustomShape
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

        public CustomRectangle(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            rect = new Rectangle();
            rect.Width = 0;
            rect.Height = 0;
            rect.Stroke = stroke;
            rect.Fill = fill;
            rect.StrokeThickness = strokeThickness;
        }

        public CustomRectangle() : this(new Point(0, 0), Brushes.Black, Brushes.Black, 0) { }

        public override Shape GetShape()
        {
            return rect;
        }

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                Left = startPoint.X,
                Top = startPoint.Y,
                Fill = rect.Fill.ToString(),
                Stroke = rect.Stroke.ToString(),
                StrokeThickness = rect.StrokeThickness,
                Width = rect.Width,
                Height = rect.Height
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

            rect.Fill = new BrushConverter().ConvertFromString(baseData.Fill) as Brush;
            rect.Stroke = new BrushConverter().ConvertFromString(baseData.Stroke) as Brush;
            rect.StrokeThickness = baseData.StrokeThickness;
            rect.Width = baseData.Width;
            rect.Height = baseData.Height;
            Canvas.SetLeft(rect, baseData.Left);
            Canvas.SetTop(rect, baseData.Top);
            startPoint = new Point(baseData.Left, baseData.Top);
        }
    }
}
