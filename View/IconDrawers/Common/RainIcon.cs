using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class RainIcon : DropIcon
    {
        protected override Brush COLOR => new SolidColorBrush(Color.FromRgb(197, 226, 247));
    
        public RainIcon(Canvas canvas, double x, double y, double width, double height, IconPainter? next = null) : base(canvas, x, y, width, height, next)
        {
        }

        protected override Geometry DropGeometry(double x, double y)
        {
            return new RectangleGeometry {
                    Rect = new Rect
                    {
                        X = x * CanvasWidth,
                        Y = y * CanvasHeight,
                        Width = 5,
                        Height = rainDropHeight * CanvasHeight,
                    },
                    RadiusX = 5,
                    RadiusY = 5,
                };

        }
    }
}
