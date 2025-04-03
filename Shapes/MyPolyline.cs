using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyPolyline:MyPolyShape
    {
        private Polyline polyline;
        public override void Draw(Point currentPoint)
        {
            if (!IsPolyMode && points.Count != 1)
                points.RemoveAt(points.Count - 1);
            points.Add(currentPoint);
            polyline.Points = points;
        }

        public MyPolyline(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            polyline = new Polyline();
            polyline.Stroke = stroke;
            polyline.Fill = fill;
            polyline.StrokeThickness = strokeThickness;
        }

        public override Shape GetShape()
        {
            return polyline;
        }

    }
}
