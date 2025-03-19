using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MyPolygon(Point startPoint, SolidColorBrush brush, double thickness)
        {
            shape = new Polygon
            {
                Stroke = brush,
                StrokeThickness = thickness
            };
            this.startPoint = startPoint;
            isPolyMode = false;
            points = new PointCollection();
            points.Add(startPoint);

        }
    }
}
