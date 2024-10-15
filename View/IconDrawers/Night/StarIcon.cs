
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VremenskaPrognoza.View.IconDrawers.Night 
{
    public class StarIcon : IconPainter
    {
        private readonly Brush STAR_COLOR = new SolidColorBrush(Color.FromRgb(255, 251, 32));

        private double scale;
        private double x;
        private double y;
        private double increment;

        private double upperSize;
        private double downSize;

        public StarIcon(Canvas canvas, double scale, 
            double x, double y, double increment, IconPainter? next = null) : base(canvas, next)
        {
            this.scale = scale;
            this.x = x;
            this.y = y;
            this.increment = increment;

            upperSize = scale * 1.05;
            downSize = scale * 0.95;
        }

        protected override void MyPaint() {
            DrawStar(scale, x, y);
            
            scale += increment;
            if (scale >= upperSize || scale <= downSize) {
                increment = -increment;
            }
        }

        void DrawStar(double scale, double x, double y)
        {
            double centerX = x * CanvasWidth;
            double centerY = y * CanvasHeight;
            double radius = scale * Math.Min(CanvasWidth, CanvasHeight);

            PointCollection points = new PointCollection();

            for (int i = 0; i < 4; i++)
            {
                double angle = i * Math.PI / 2;
                double smallAngle = angle + Math.PI / 4;

                double x1 = centerX + radius * Math.Cos(angle);
                double y1 = centerY + radius * Math.Sin(angle);
                points.Add(new Point(x1, y1));
                double x2 = centerX + radius * Math.Cos(smallAngle) / 3;
                double y2 = centerY + radius * Math.Sin(smallAngle) / 3;
                points.Add(new Point(x2, y2));
            }

            MyCanvas.Children.Add(new Polygon() {
                Points = points,
                Fill = STAR_COLOR
            });
        }

    }
}