using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VremenskaPrognoza.View.CanvasDraw
{
    public class CanvasPainter
    {
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }
        public double Radius {  get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public Canvas MyCanvas { get; set; }

        public CanvasPainter(Canvas canvas)
        {
            MyCanvas = canvas;
        }

        public void RecalculateDimensions()
        {
            MyCanvas.Children.Clear();

            CanvasWidth = MyCanvas.ActualWidth;
            CanvasHeight = MyCanvas.ActualHeight;

            Radius = Math.Max(5 * CanvasWidth / 12, CanvasHeight / 2);

            CenterX = CanvasWidth / 2;
            CenterY = 11 * CanvasHeight / 12;
        }

        public void DrawHalfCircle(int tickness, Brush scb,
            double percentage = 100.0)
        {

            Point startPoint = new Point(CanvasWidth / 12, 11 * CanvasHeight / 12);

            double sweepAngle = 180 * (percentage / 100.0);

            double endX = CenterX - Radius * Math.Cos(sweepAngle * Math.PI / 180);
            double endY = CenterY - Radius * Math.Sin(sweepAngle * Math.PI / 180);

            Point endPoint = new Point(endX, endY);

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            ArcSegment arcSegment = new ArcSegment
            {
                Point = endPoint,
                Size = new Size(Radius, Radius),
                SweepDirection = SweepDirection.Clockwise,
            };

            pathFigure.Segments.Add(arcSegment);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            Path path = new Path
            {
                Stroke = scb,
                StrokeThickness = tickness,
                Data = pathGeometry,
                Fill = Brushes.Transparent
            };

            MyCanvas.Children.Add(path);
        }

        public void DrawPoints<T>(T[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                double angle = Math.PI * i / (points.Length - 1);

                double x = CenterX - (Radius - 25) * Math.Cos(angle);
                double y = CenterY - (Radius - 25) * Math.Sin(angle);

                TextBlock textBlock = new TextBlock
                {
                    Text = points[i].ToString(),
                    FontSize = 12,
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Verdana")
                };

                textBlock.Measure(new Size(CanvasWidth, CanvasHeight));
                Size textSize = textBlock.DesiredSize;

                Canvas.SetTop(textBlock, y - textSize.Height);
                Canvas.SetLeft(textBlock, x - textSize.Width / 2);

                MyCanvas.Children.Add(textBlock);
            }
        }

        public void DrawCircle(double circleX,
            double circleY,
            double radius,
            Brush fill,
            Brush stroke)
        {

            Ellipse circle = new Ellipse
            {
                Fill = fill,
                Stroke = stroke,
                Width = 2 * radius,
                Height = 2 * radius,
            };

            Canvas.SetLeft(circle, circleX - radius);
            Canvas.SetTop(circle, circleY - radius);

            MyCanvas.Children.Add(circle);
        }
    }
}
