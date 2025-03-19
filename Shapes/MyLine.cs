using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyLine : MySingleShape
    {
        public override void Draw(Point currentPoint)
        {
            ((Line) shape).X1 = startPoint.X;
            ((Line) shape).Y1 = startPoint.Y;
            ((Line) shape).X2 = currentPoint.X;
            ((Line) shape).Y2 = currentPoint.Y;
        }

        public MyLine(Point startPoint, SolidColorBrush brush, double thickness)
        {
            shape = new Line
            {
                Stroke = brush,
                StrokeThickness = thickness
            };
            this.startPoint = startPoint;
        }
    }
}
