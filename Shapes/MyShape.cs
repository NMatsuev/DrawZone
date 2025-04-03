using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    public abstract class MyShape
    {
        protected Point startPoint;
        
        public MyShape(Point startPoint)
        {
            this.startPoint = startPoint;
        }

        abstract public void Draw(Point currentPoint);
        abstract public Shape GetShape();

    }
}
