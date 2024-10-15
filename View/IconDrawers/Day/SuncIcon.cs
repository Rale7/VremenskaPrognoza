
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VremenskaPrognoza.View.IconDrawers.Day 
{
    public class SunIcon : IconPainter
    {
        private readonly Brush SUN_COLOR = new SolidColorBrush(Color.FromRgb(255, 223, 34));
        private readonly Brush OUTSUN_COLOR = new SolidColorBrush(Color.FromArgb(100, 255, 223, 34));

        private double scale;
        private double outlineScale;
        private double x;
        private double y;
        private double increment;

        public SunIcon(Canvas canvas, double scale, double x, double y, IconPainter? next = null) : base(canvas, next)
        {
            this.scale = scale;
            this.x = x;
            this.y = y;

            outlineScale = scale * 1.05;
            increment = scale * 0.00125;
        }

        protected override void MyPaint()
        {
            DrawSunIcon(scale, outlineScale, x, y);

            outlineScale += increment;

            if (outlineScale >= scale * 1.075 || outlineScale <= scale * 1.05) {
                increment = -increment;
            }
        }

        private void DrawSunIcon(double scale, double scaleOutline, double x, double y)
        {

            double centerX = x * CanvasWidth;
            double centerY = y * CanvasHeight;
            double radius = Math.Min(CanvasHeight, CanvasWidth) * scale;
            double outRadius = Math.Min(CanvasHeight, CanvasWidth) * scaleOutline;

            DrawCircle(centerX, centerY, radius, SUN_COLOR, Brushes.Transparent);
            DrawCircle(centerX, centerY, outRadius, OUTSUN_COLOR, Brushes.Transparent);
        }
    }
}