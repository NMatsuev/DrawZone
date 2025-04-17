using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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
        public override Shape GetShape()
        {
            return polygon;
        }
    }
}
