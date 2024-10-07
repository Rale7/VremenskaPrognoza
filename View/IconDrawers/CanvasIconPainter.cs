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
            RecalculateDimensions();

            double centerX = x * CanvasWidth;
            double centerY = y * CanvasHeight;
            double radius = Math.Min(CanvasHeight, CanvasWidth) * scale;
            double outRadius = Math.Min(CanvasHeight, CanvasWidth) * scaleOutline;

            DrawCircle(centerX, centerY, radius, SUN_COLOR, Brushes.Transparent);
            DrawCircle(centerX, centerY, outRadius, OUTSUN_COLOR, Brushes.Transparent);
        }

        public void DrawMoonIcon(double scale, double x, double y, double angle)
        {
            RecalculateDimensions();

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

    }
}
