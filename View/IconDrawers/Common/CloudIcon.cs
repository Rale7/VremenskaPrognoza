
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VremenskaPrognoza.View.IconDrawers.Common
{
    public class CloudIcon : IconPainter
    {
        private double scale;
        private double x;
        private double y;

        protected virtual Brush COLOR
        {
            get => Brushes.White;
        }

        public CloudIcon(Canvas canvas, double scale,
            double x, double y, IconPainter? next = null) : base(canvas, next)
        {
            this.scale = scale;
            this.x = x;
            this.y = y;
        }

        protected override void MyPaint()
        {
            DrawCloud(scale, x, y);
        }

        private void DrawCloud(double scale, double x, double y) {

            double radius1 = Math.Min(CanvasHeight, CanvasWidth) * scale;
            double centerX1 = x * CanvasWidth;
            double centerY1 = y * CanvasHeight;

            double radius2 = radius1 / 2;
            double centerX2 = centerX1 - radius1;
            double centerY2 = centerY1 + radius1 - radius2;

            double radius3 = radius1 * 2 / 3;
            double centerX3 = centerX1 + radius1;
            double centerY3 = centerY1 + radius1 - radius3;

            DrawCircle(centerX1, centerY1, radius1, COLOR, Brushes.Transparent);
            DrawCircle(centerX2, centerY2, radius2, COLOR, Brushes.Transparent);
            DrawCircle(centerX3, centerY3, radius3, COLOR, Brushes.Transparent);
            
            Rectangle rect = new Rectangle() {
                Width = 2 * radius1,
                Height = radius2,
                Fill = COLOR
            };

            Canvas.SetLeft(rect, centerX2);
            Canvas.SetTop(rect, centerY2);

            MyCanvas.Children.Add(rect);
        }
    }
}