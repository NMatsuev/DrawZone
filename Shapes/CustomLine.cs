using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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

        public override Shape GetShape()
        {
            return line;
        }
    }
}
