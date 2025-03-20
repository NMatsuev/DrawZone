using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyPolyline:MyPolyShape
    {
        public override void Draw(Point currentPoint)
        {
            if (!isPolyMode && points.Count != 1)
                points.RemoveAt(points.Count - 1);
            points.Add(currentPoint);
            ((Polyline)shape).Points = points;
        }

        public MyPolyline(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            shape = new Polyline();
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = strokeThickness;
        }

    }
}
