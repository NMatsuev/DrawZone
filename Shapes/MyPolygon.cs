using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyPolygon:MyPolyShape
    {
        public override void Draw(Point currentPoint)
        {
            if (!isPolyMode && points.Count != 1)
                points.RemoveAt(points.Count - 1);
            points.Add(currentPoint);
            ((Polygon)shape).Points = points;
        }

        public MyPolygon(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            shape = new Polygon();
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = strokeThickness;
        }
    }
}
