using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DrawZone.Shapes
{
    public abstract class MyPolyShape : MyShape
    {
        protected PointCollection points;
        public bool IsPolyMode { get; set; }

        public MyPolyShape(Point startPoint) : base(startPoint)
        {
            IsPolyMode = false;
            points = new PointCollection
            {
                startPoint
            };
        }

    }
}
