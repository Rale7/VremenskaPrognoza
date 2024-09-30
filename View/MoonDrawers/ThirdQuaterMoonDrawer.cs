using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.MoonDrawers
{
    public class ThirdQuaterMoonDrawer : BaseMoonDrawer
    {
        public ThirdQuaterMoonDrawer(Canvas canvas) : base(canvas) { }

        public override void DrawMoonType(double percentage)
        {
            RecalculatePoints();

            double scale = (percentage - 50) / 50;

            DrawLeftHalfCircle(1, Brushes.White, Brushes.White);
            DrawRightHalfCircle(1, Brushes.Black, Brushes.Black);
            DrawRightHalfCircle(1, Brushes.White, Brushes.White, scale);
            DrawLine(Brushes.White);
        }
    }
}
