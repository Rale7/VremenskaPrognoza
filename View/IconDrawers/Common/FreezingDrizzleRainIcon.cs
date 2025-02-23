using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    internal class FreezingDrizzleRainIcon : RainIcon
    {
        protected override Brush COLOR => Brushes.White;

        public FreezingDrizzleRainIcon(Canvas canvas, double x, double y, double width, double height, double offset = 0, IconPainter? next = null) : base(canvas, x, y, width, height, offset, next)
        {
        }
    }
}
