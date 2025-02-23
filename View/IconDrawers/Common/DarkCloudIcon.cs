using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class DarkCloudIcon : CloudIcon
    {
        protected override Brush COLOR => new SolidColorBrush(Color.FromRgb(123, 159, 165));

        public DarkCloudIcon(Canvas canvas, double scale, double x, double y, IconPainter? next = null) : base(canvas, scale, x, y, next)
        {
        }
    }
}
