using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    class MyLine : MySingleShape
    {
        public override void Draw(MyPoint currentPoint)
        {
            ((Line) shape).X1 = startPoint.X;
            ((Line) shape).Y1 = startPoint.Y;
            ((Line) shape).X2 = currentPoint.X;
            ((Line) shape).Y2 = currentPoint.Y;
        }

        public MyLine(MyPoint startPoint, SolidColorBrush brush, double thickness)
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
