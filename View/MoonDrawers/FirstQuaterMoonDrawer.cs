using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.MoonDrawers
{
    public class FirstQuaterMoonDrawer : BaseMoonDrawer
    {
        public FirstQuaterMoonDrawer(Canvas canvas) : base(canvas) { }

        public override void DrawMoonType(double percentage)
        {
            RecalculatePoints();

            double scale = (50 - percentage) / 50;

            DrawLeftHalfCircle(1, Brushes.Black, Brushes.Black);
            DrawRightHalfCircle(1, Brushes.White, Brushes.White);
            DrawRightHalfCircle(1, Brushes.Black, Brushes.Black, scale);
            DrawLine(Brushes.Black);
        }
    }
}
