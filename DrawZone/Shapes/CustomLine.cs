using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace DrawZone.Shapes
{
    class CustomLine : CustomShape
    {
        private Line line;
        public override void Draw(Point currentPoint)
        {
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = currentPoint.X;
            line.Y2 = currentPoint.Y;
        }

        public CustomLine(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            line = new Line();
            line.Stroke = stroke;
            line.Fill = fill;
            line.StrokeThickness = strokeThickness;
        }

        public CustomLine() : this(new Point(0, 0), Brushes.Black, Brushes.Black, 0) { }

        public override Shape GetShape()
        {
            return line;
        }

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(new
            {
                X1 = line.X1,
                Y1 = line.Y1,
                X2 = line.X2,
                Y2 = line.Y2,
                Fill = line.Fill.ToString(),
                Stroke = line.Stroke.ToString(),
                StrokeThickness = line.StrokeThickness,
            });
        }

        public override void Deserialize(string json)
        {
            var baseData = JsonConvert.DeserializeAnonymousType(json, new
            {
                X1 = 0.0,
                Y1 = 0.0,
                X2 = 0.0,
                Y2 = 0.0,
                Fill = "",
                Stroke = "",
                StrokeThickness = 0.0,
            });

            line.Fill = new BrushConverter().ConvertFromString(baseData.Fill) as Brush;
            line.Stroke = new BrushConverter().ConvertFromString(baseData.Stroke) as Brush;
            line.StrokeThickness = baseData.StrokeThickness;
            line.X1 = baseData.X1;
            line.Y1 = baseData.Y1;
            line.X2 = baseData.X2;
            line.Y2 = baseData.Y2;
            startPoint = new Point(baseData.X1, baseData.Y1);
        }
    }
}
