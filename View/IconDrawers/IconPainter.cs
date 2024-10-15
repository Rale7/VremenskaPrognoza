using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using VremenskaPrognoza.View.CanvasDraw;

namespace VremenskaPrognoza.View.IconDrawers
{
    public abstract class IconPainter : CanvasPainter
    {
        protected IconPainter? next;

        public IconPainter(Canvas canvas, IconPainter? next = null) : base(canvas)
        {
            this.next = next;
        }

        public void RecalculateAndPaint() {
            ClearCanvas();
            Paint();
        }

        public void Paint() {
            next?.Paint();
            RecalculateDimensions();
            MyPaint();
        }

        abstract protected void MyPaint();
    }
}
