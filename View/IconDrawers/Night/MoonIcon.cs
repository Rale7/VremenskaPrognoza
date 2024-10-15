
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VremenskaPrognoza.View.IconDrawers.Night 
{ 
    public class MoonIcon : IconPainter
    {
        private readonly Brush MOON_COLOR = new SolidColorBrush(Color.FromRgb(176, 212, 236));
        private readonly int ANGLE = 45;

        private double scale;
        private double x;
        private double y;

        public MoonIcon(Canvas canvas, double scale, double x, 
            double y, IconPainter? next = null) : base(canvas, next)
        {
            this.scale = scale;
            this.x = x;
            this.y = y;
        }

        protected override void MyPaint() {
            DrawMoonIcon(); 
        }

        private void DrawMoonIcon()
        {

            double radians = Math.PI * ANGLE / 180;

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
    }
}