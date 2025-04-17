using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawZone.Shapes
{
    public abstract class CustomShape
    {
        protected Point startPoint;
        public bool IsDrawed { get; set; }

        public CustomShape(Point startPoint)
        {
            this.startPoint = startPoint;
        }

        public abstract void Draw(Point currentPoint);
        public abstract Shape GetShape();
        public virtual bool SupportsPolyMode => false;
        public virtual void EnterPolyMode() { }
        public virtual void ExitPolyMode() { }
    }
}
