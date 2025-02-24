using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class IcePelletIcon : DropIcon
    {
        public IcePelletIcon(Canvas canvas, double x, double y, double width, double height, double offset = 0, IconPainter? next = null) : base(canvas, x, y, width, height, offset, next)
        {
        }

        protected override Brush COLOR => Brushes.White;

        protected override Geometry DropGeometry(double x, double y)
        {
            double radius = 0.1 * Math.Min(width, height) * Math.Min(CanvasHeight, CanvasHeight);

            return new EllipseGeometry
            {
                Center = new Point(x * CanvasWidth, y * CanvasHeight),
                RadiusX = radius,
                RadiusY = radius,
            }; 
        }
    }
}
