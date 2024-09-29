using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VremenskaPrognoza.APICalling;
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    /// <summary>
    /// Interaction logic for UVChart.xaml
    /// </summary>
    public partial class UVChart : UserControl
    {
        private int[] scalePoints = { 0, 2, 4, 6, 8, 10, 12 };
        private double canvasWidth;
        private double canvasHeight;
        private double radius;
        private double centerX;
        private double centerY;

        private ResponseViewModel rvm;

        public UVChart()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalAction += RepaintChart;
        }

        private void CharCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RepaintChart();
        }

        public void RepaintChart()
        {
            ChartCanvas.Children.Clear();

            canvasWidth = ChartCanvas.ActualWidth;
            canvasHeight = ChartCanvas.ActualHeight;

            radius = Math.Max(5 * canvasWidth / 12, canvasHeight / 2);

            centerX = canvasWidth / 2;
            centerY = canvasHeight;

            double chartPercentage;

            try
            {
                chartPercentage = rvm.Response.CurrentWeather.UvIndex;
            } catch (NullReferenceException ex)
            {
                chartPercentage = 0;
            }
             
            DrawHalfCircle(20, createGradientBrush(), 
                (chartPercentage / 12.0) * 100);
            DrawHalfCircle(1, Brushes.White);
            DrawHalfCircle(6, new SolidColorBrush(Color.FromArgb(50, 255, 255, 255)));
            DrawPoints();
        }

        private Brush createGradientBrush()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0); 
            gradientBrush.EndPoint = new Point(1, 0);

            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(57, 75, 85), 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(80, 207, 233), 1.0));

            return gradientBrush;
        }

        private void DrawHalfCircle(int tickness, Brush scb,
            double percentage = 100.0)
        {            

            Point startPoint = new Point(canvasWidth / 12, canvasHeight);

            double sweepAngle = 180 * (percentage / 100.0);
            
            double endX = centerX - radius * Math.Cos(sweepAngle * Math.PI / 180);
            double endY = centerY - radius * Math.Sin(sweepAngle * Math.PI / 180);

            Point endPoint = new Point(endX, endY);

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };

            ArcSegment arcSegment = new ArcSegment
            {
                Point = endPoint,
                Size = new Size(radius, radius),
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

            ChartCanvas.Children.Add(path);
        }

        private void DrawPoints()
        {            

            for (int i = 0; i < scalePoints.Length; i++) 
            {
                double angle = Math.PI * i / (scalePoints.Length - 1);

                double x = centerX - (radius - 25) * Math.Cos(angle);
                double y = centerY - (radius - 25) * Math.Sin(angle);

                TextBlock textBlock = new TextBlock
                {
                    Text = (scalePoints[i]).ToString(),
                    FontSize = 12,
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Verdana")
                };

                textBlock.Measure(new Size(canvasWidth, canvasHeight));
                Size textSize = textBlock.DesiredSize;
                
                Canvas.SetTop(textBlock, y - textSize.Height);
                Canvas.SetLeft(textBlock, x - textSize.Width / 2);                

                ChartCanvas.Children.Add(textBlock);
            }
        }
    }
}
