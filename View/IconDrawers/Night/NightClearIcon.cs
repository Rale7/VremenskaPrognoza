using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Day
{
    public class NightClearIcon : IconPainter
    {
        private double outPartBig = 0.1;
        private double incrementBig = 0.0005;

        private double outPartSmall = 0.05;
        private double incrementSmall = -0.00025;

        public NightClearIcon(CanvasIconPainter canvasIconPainter) : base(canvasIconPainter)
        {
        }

        public override void Paint()
        {
            canvasIconPainter.RecalculateDimensions();
            canvasIconPainter.DrawMoonIcon(0.4, 0.75, 0.25, 45);
            canvasIconPainter.DrawStar(outPartBig, 0.7, 0.45);
            canvasIconPainter.DrawStar(outPartSmall, 0.6, 0.3);

            outPartBig += incrementBig;
            if (outPartBig >= 0.105 || outPartBig <= 0.095)
            {
                incrementBig = -incrementBig;
            }

            outPartSmall += incrementSmall;
            if (outPartSmall >= 0.0525 || outPartSmall <= 0.0475)
            {
                incrementSmall = -incrementSmall;
            }
        }
    }
}
