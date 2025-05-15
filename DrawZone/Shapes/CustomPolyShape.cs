using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DrawZone.Shapes
{
    public abstract class CustomPolyShape : CustomShape
    {
        protected PointCollection points;

        public CustomPolyShape(Point startPoint) : base(startPoint)
        {
            IsPolyMode = false;
            points = new PointCollection
            {
                startPoint
            };
        }

        public override bool SupportsPolyMode => true;
        public override void EnterPolyMode() => IsPolyMode = true;
        public override void ExitPolyMode() => IsPolyMode = false;

    }
}
