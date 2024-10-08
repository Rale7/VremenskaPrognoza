using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VremenskaPrognoza.View.CanvasDraw;

namespace VremenskaPrognoza.View.IconDrawers
{
    public class CanvasIconPainter : CanvasPainter
    {        

        private readonly Brush SUN_COLOR = new SolidColorBrush(Color.FromRgb(255, 223, 34));
        private readonly Brush OUTSUN_COLOR = new SolidColorBrush(Color.FromArgb(100, 255, 223, 34));

        private readonly Brush MOON_COLOR = new SolidColorBrush(Color.FromRgb(176, 212, 236));

        private readonly Brush STAR_COLOR = new SolidColorBrush(Color.FromRgb(255, 251, 32));

        private double width;
        private double height;

        public CanvasIconPainter(Canvas canvas) : base(canvas) { }

        public void DrawSunIcon(double scale, double scaleOutline, double x, double y)
        {

            double centerX = x * CanvasWidth;
            double centerY = y * CanvasHeight;
            double radius = Math.Min(CanvasHeight, CanvasWidth) * scale;
            double outRadius = Math.Min(CanvasHeight, CanvasWidth) * scaleOutline;

            DrawCircle(centerX, centerY, radius, SUN_COLOR, Brushes.Transparent);
            DrawCircle(centerX, centerY, outRadius, OUTSUN_COLOR, Brushes.Transparent);
        }

        public void DrawMoonIcon(double scale, double x, double y, double angle)
        {

            double radians = Math.PI * angle / 180;

            double centerX1 = x * CanvasWidth;
            double centerY1 = y * CanvasHeight;
            double radius = Math.Min(CanvasWidth, CanvasHeight) * scale;
            double centerX2 = centerX1 - radius * Math.Cos(radians);
            double centerY2 = centerY1 + radius * Math.Sin(radians);

            GeometryGroup geometryGroup = new GeometryGroup();

            EllipseGeometry moon = new EllipseGeometry()
            {
                RadiusX = radius,
                RadiusY = radius,
                Center = new Point(centerX1, centerY1),
            };

            geometryGroup.Children.Add(moon);

            EllipseGeometry exclude = new EllipseGeometry()
            {
                RadiusX = radius,
                RadiusY = radius,
                Center = new Point(centerX2, centerY2)
            };

            MyCanvas.Children.Add(new Path()
            {
                Data = new CombinedGeometry(GeometryCombineMode.Exclude, 
                exclude, geometryGroup),
                Fill = MOON_COLOR
            });
        }

        public void DrawStar(double scale, double x, double y)
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

        public void DrawCloud(double scale, double x, double y) {

            double radius1 = Math.Min(CanvasHeight, CanvasWidth) * scale;
            double centerX1 = x * CanvasWidth;
            double centerY1 = y * CanvasHeight;

            double radius2 = radius1 / 2;
            double centerX2 = centerX1 - radius1;
            double centerY2 = centerY1 + radius1 - radius2;

            double radius3 = radius1 * 2 / 3;
            double centerX3 = centerX1 + radius1;
            double centerY3 = centerY1 + radius1 - radius3;

            DrawCircle(centerX1, centerY1, radius1, Brushes.White, Brushes.Transparent);
            DrawCircle(centerX2, centerY2, radius2, Brushes.White, Brushes.Transparent);
            DrawCircle(centerX3, centerY3, radius3, Brushes.White, Brushes.Transparent);
            
            Rectangle rect = new Rectangle() {
                Width = 2 * radius1,
                Height = radius2,
                Fill = Brushes.White
            };

            Canvas.SetLeft(rect, centerX2);
            Canvas.SetTop(rect, centerY2);

            MyCanvas.Children.Add(rect);

        }

    }
}
