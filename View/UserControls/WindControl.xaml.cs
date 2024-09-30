using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    public partial class WindControl : UserControl
    {
        private String[] cardinalDirections = new String[] { "E", "S", "W", "N" };

        private const double sideScale = 0.05;
        private const int circleTickness = 6;

        private double centerX;
        private double centerY;
        private double radius;
        private double circleRadius;
        private double circleX;
        private double circleY;

        private ResponseViewModel rvm;

        public WindControl()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalAction += DrawWindCompas;
        }

        public void DrawWindCompas()
        {
            windCompas.Children.Clear();

            centerX = windCompas.ActualWidth / 2;
            centerY = windCompas.ActualHeight / 2;

            radius = Math.Min(centerX, centerY) * 0.8;
            circleRadius = radius * 0.8;
            
            circleX = centerX - circleRadius;
            circleY = centerY - circleRadius;

            double windDegree;

            try
            {
                windDegree = rvm.Response.CurrentWeather.WindDegree;
            } catch(NullReferenceException ex)
            {
                windDegree = 0;
            }

            DrawCircle();
            DrawCardinalDirection();
            DrawCompasArrow(windDegree);
        }

        private void DrawCardinalDirection()
        {
            for (int i = 0; i < cardinalDirections.Length; i++)
            {
                double angle = 2 * Math.PI * i / (cardinalDirections.Length);

                double x = centerX + Math.Cos(angle) * radius;
                double y = centerY + Math.Sin(angle) * radius;

                TextBlock textBlock = new TextBlock()
                {
                    FontFamily = new FontFamily("Verdana"),
                    Foreground = Brushes.White,
                    Text = cardinalDirections[i],
                    FontSize = 15
                };

                textBlock.Measure(new Size(windCompas.ActualWidth, windCompas.ActualHeight));
                Size textSize = textBlock.DesiredSize;

                Canvas.SetLeft(textBlock, x - textSize.Width / 2);
                Canvas.SetTop(textBlock, y - textSize.Height / 2);

                windCompas.Children.Add(textBlock);
            }
        }

        private void DrawCircle()
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 2 * circleRadius,
                Height = 2 * circleRadius,
                Fill = Brushes.Transparent,
                Stroke = Brushes.White,
                StrokeThickness = 2,
            };

            Canvas.SetLeft(ellipse, centerX - circleRadius);
            Canvas.SetTop(ellipse, centerY - circleRadius);

            windCompas.Children.Add(ellipse);
        }

        private void DrawCompasArrow(double degrees)
        {            
            double radians = Math.PI * degrees / 180;

            double x1 = centerX - Math.Cos(radians) * (circleRadius - circleTickness);
            double y1 = centerY + Math.Sin(radians) * (circleRadius - circleTickness);

            double x2 = centerX - Math.Cos(radians + Math.PI / 2) * circleRadius * sideScale;
            double y2 = centerY + Math.Sin(radians + Math.PI / 2) * circleRadius * sideScale;

            double x3 = centerX - Math.Cos(radians - Math.PI / 2) * circleRadius * sideScale;
            double y3 = centerY + Math.Sin(radians - Math.PI / 2) * circleRadius * sideScale;

            Point[] points = new Point[]
            {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x3, y3),
            };            

            Polygon arrow = new Polygon()
            {
                Points = new PointCollection(points),
                Stroke = Brushes.Red,
                Fill = Brushes.Red,
            };

            windCompas.Children.Add(arrow);
        }

        private void windCompas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawWindCompas();
        }
    }
}
