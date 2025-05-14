using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawZone.Utils;
using Newtonsoft.Json;

namespace DrawZone.Shapes
{
    class CustomPolygon:CustomPolyShape
    {
        Polygon polygon;
        public override void Draw(Point currentPoint)
        {
            if (!IsPolyMode && points.Count != 1)
                points.RemoveAt(points.Count - 1);
            points.Add(currentPoint);
            polygon.Points = points;
        }

        public CustomPolygon(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            polygon = new Polygon();
            polygon.Stroke = stroke;
            polygon.Fill = fill;
            polygon.StrokeThickness = strokeThickness;
        }

        public CustomPolygon() : this(new Point(0, 0), Brushes.Black, Brushes.Black, 0) { }

        public override Shape GetShape()
        {
            return polygon;
        }
        public override string Serialize()
        {
            List<PointData> serializedPoints = new List<PointData>();
            if (polygon.Points != null)
            {
                foreach (var point in polygon.Points)
                {
                    serializedPoints.Add(new PointData { X = point.X, Y = point.Y });
                }
            }

            var data = new
            {
                StartPoint = new PointData { X = startPoint.X, Y = startPoint.Y },
                Stroke = polygon.Stroke.ToString(),
                Fill = polygon.Fill.ToString(),
                StrokeThickness = polygon.StrokeThickness,
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
                polygon.Stroke = !string.IsNullOrEmpty(data.Stroke)
                    ? (Brush)brushConverter.ConvertFromString(data.Stroke)
                    : Brushes.Black;

                polygon.Fill = !string.IsNullOrEmpty(data.Fill)
                    ? (Brush)brushConverter.ConvertFromString(data.Fill)
                    : Brushes.Transparent;

                polygon.StrokeThickness = data.StrokeThickness;

                polygon.Points = new PointCollection();
                foreach (var pointData in data.Points)
                {
                    polygon.Points.Add(new Point(pointData.X, pointData.Y));
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка десериализации полигона", ex);
            }
        }
    }
}
