using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawZone.Utils;
using Newtonsoft.Json;

namespace DrawZone.Shapes
{
    class CustomPolyline:CustomPolyShape
    {
        private Polyline polyline;
        public override void Draw(Point currentPoint)
        {
            if (!IsPolyMode && points.Count != 1)
                points.RemoveAt(points.Count - 1);
            points.Add(currentPoint);
            polyline.Points = points;
        }

        public CustomPolyline(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            polyline = new Polyline();
            polyline.Stroke = stroke;
            polyline.StrokeThickness = strokeThickness;
            polyline.Fill = Brushes.White;
        }
        public CustomPolyline() : this(new Point(0, 0), Brushes.Black, Brushes.Black, 0) { }

        public override Shape GetShape()
        {
            return polyline;
        }

        public override string Serialize()
        {
            List<PointData> serializedPoints = new List<PointData>();
            if (polyline.Points != null)
            {
                foreach (var point in polyline.Points)
                {
                    serializedPoints.Add(new PointData { X = point.X, Y = point.Y });
                }
            }

            var data = new
            {
                StartPoint = new PointData { X = startPoint.X, Y = startPoint.Y },
                Stroke = polyline.Stroke.ToString(),
                Fill = polyline.Fill.ToString(),
                StrokeThickness = polyline.StrokeThickness,
                Points = serializedPoints,
            };

            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }


        public override void Deserialize(string json)
        {
            try
            {
                var data = JsonConvert.DeserializeAnonymousType(json, new
                {
                    StartPoint = new PointData(),
                    Stroke = "",
                    Fill = "",
                    StrokeThickness = 0.0,
                    Points = new List<PointData>(),
                });

                startPoint = new Point(data.StartPoint.X, data.StartPoint.Y);

                var brushConverter = new BrushConverter();
                polyline.Stroke = !string.IsNullOrEmpty(data.Stroke)
                    ? (Brush)brushConverter.ConvertFromString(data.Stroke)
                    : Brushes.Black;

                polyline.Fill = !string.IsNullOrEmpty(data.Fill)
                    ? (Brush)brushConverter.ConvertFromString(data.Fill)
                    : Brushes.Transparent;

                polyline.StrokeThickness = data.StrokeThickness;

                polyline.Points = new PointCollection();
                foreach (var pointData in data.Points)
                {
                    polyline.Points.Add(new Point(pointData.X, pointData.Y));
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка десериализации ломаной", ex);
            }
        }

    }
}
