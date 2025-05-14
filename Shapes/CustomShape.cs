using System;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;

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
        public bool IsPolyMode { get; set; }
        public virtual void EnterPolyMode() { }
        public virtual void ExitPolyMode() { }
        public abstract string Serialize();
        public abstract void Deserialize(string json);
    }
}
