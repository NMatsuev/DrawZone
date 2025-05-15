
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawZone.Shapes;
using DrawZone.Utils;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Trapezoid
    {

        public class Trapezoid:CustomShape
        {
            private Polygon trapezoid;
            private double topWidthRatio = 0.6;

        public override void Draw(Point currentPoint)
        {
            double width = Math.Abs(currentPoint.X - startPoint.X);
            double height = Math.Abs(currentPoint.Y - startPoint.Y);

            double topWidth = width * topWidthRatio;
            double bottomWidth = width;

            double topCenterX = startPoint.X + bottomWidth / 2;
            double bottomCenterX = startPoint.X + bottomWidth / 2;

            double topLeftX = topCenterX - topWidth / 2;
            double topRightX = topCenterX + topWidth / 2;

            double bottomLeftX = startPoint.X;
            double bottomRightX = startPoint.X + bottomWidth;

            PointCollection points = new PointCollection
    {
        new Point(topLeftX, startPoint.Y),
        new Point(topRightX, startPoint.Y),
        new Point(bottomRightX, currentPoint.Y),
        new Point(bottomLeftX, currentPoint.Y)
    };

            trapezoid.Points = points;

            if (trapezoid.Parent is Canvas canvas)
            {
                RectangleGeometry clipGeometry = new RectangleGeometry(
                    new Rect(0, 0, canvas.MaxWidth, canvas.MaxHeight));
                trapezoid.Clip = clipGeometry;
            }
        }

        public Trapezoid(Point startPoint, Brush stroke, Brush fill, double strokeThickness)
                : base(startPoint)
            {
                trapezoid = new Polygon
                {
                    Stroke = stroke,
                    Fill = fill,
                    StrokeThickness = strokeThickness
                };
            }

            public Trapezoid() : this(new Point(0, 0), Brushes.Black, Brushes.Transparent, 1) { }

            public override Shape GetShape()
            {
                return trapezoid;
            }

            public override string Serialize()
            {
                List<PointData> serializedPoints = new List<PointData>();
                if (trapezoid.Points != null)
                {
                    foreach (var point in trapezoid.Points)
                    {
                        serializedPoints.Add(new PointData { X = point.X, Y = point.Y });
                    }
                }

                var data = new
                {
                    Type = nameof(Trapezoid),
                    StartPoint = new PointData { X = startPoint.X, Y = startPoint.Y },
                    Stroke = trapezoid.Stroke?.ToString() ?? "",
                    Fill = trapezoid.Fill?.ToString() ?? "",
                    StrokeThickness = trapezoid.StrokeThickness,
                    Points = serializedPoints,
                    TopWidthRatio = topWidthRatio
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
                        TopWidthRatio = 0.6
                    });

                    startPoint = new Point(data.StartPoint.X, data.StartPoint.Y);
                    topWidthRatio = data.TopWidthRatio;

                    var brushConverter = new BrushConverter();
                    trapezoid.Stroke = !string.IsNullOrEmpty(data.Stroke)
                        ? (Brush)brushConverter.ConvertFromString(data.Stroke)
                        : Brushes.Black;

                    trapezoid.Fill = !string.IsNullOrEmpty(data.Fill)
                        ? (Brush)brushConverter.ConvertFromString(data.Fill)
                        : Brushes.Transparent;

                    trapezoid.StrokeThickness = data.StrokeThickness;

                    trapezoid.Points = new PointCollection();
                    foreach (var pointData in data.Points)
                    {
                        trapezoid.Points.Add(new Point(pointData.X, pointData.Y));
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Ошибка десериализации трапеции", ex);
                }
            }
        }
    }

