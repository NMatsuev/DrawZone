using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyLine : MyShape
    {
        public override void Draw(Point currentPoint)
        {
            ((Line) shape).X1 = startPoint.X;
            ((Line) shape).Y1 = startPoint.Y;
            ((Line) shape).X2 = currentPoint.X;
            ((Line) shape).Y2 = currentPoint.Y;
        }

        public MyLine(Point startPoint, Brush stroke, Brush fill, double strokeThickness) : base(startPoint)
        {
            shape = new Line();
            Stroke = stroke;
            Fill = fill;
            StrokeThickness = strokeThickness;
        }
    }
}
