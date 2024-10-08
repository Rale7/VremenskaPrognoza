using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VremenskaPrognoza.View.IconDrawers.Day
{
    class SunnyIconPainter : IconPainter
    {
        private double outPart = 0.42;
        private double increment = 0.0005;

        public SunnyIconPainter(CanvasIconPainter canvasIconPainter) : base(canvasIconPainter)
        {
        }

        public override void Paint()
        {
            canvasIconPainter.RecalculateDimensions();
            canvasIconPainter.DrawSunIcon(0.4, outPart, 0.5, 0.5);

            outPart += increment;
            if (outPart >= 0.43 || outPart <= 0.42)
            {
                increment = -increment;
            }
        }
    }
}
