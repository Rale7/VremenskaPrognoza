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
    /// Interaction logic for AirQualityControl.xaml
    /// </summary>
    public partial class AirQualityControl : UserControl
    {

        private ResponseViewModel rvm;

        public AirQualityControl()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalRealtimeAction += DrawScale;

        }        

        public async Task DrawScale()
        {

            airQualityScale.Children.Clear();

            Rectangle rect = new Rectangle()
            {
                Width = airQualityScale.ActualWidth,
                Height = airQualityScale.ActualHeight / 4,
                Fill = CreateRaindowGradient()
            };

            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, airQualityScale.ActualHeight / 2);

            airQualityScale.Children.Add(rect);

            DrawPointer();
        }

        private void DrawPointer()
        {
            int index;
            try
            {
                index = rvm.RealtimeResponse.CurrentWeather.AirQuality.UsEpaIndex;
            }
            catch (NullReferenceException)
            {
                index = 0;
            }

            double x = airQualityScale.ActualWidth * index / 5;            
            double increment = airQualityScale.ActualHeight * 0.25;
            double y = airQualityScale.ActualHeight * 0.6;

            Polygon polygon = new Polygon()
            {
                Points =
                {
                    new Point(x, y),
                    new Point(x - increment, y + increment),
                    new Point(x + increment, y + increment)
                },
                Fill = Brushes.White
            };

            airQualityScale.Children.Add(polygon);
        }

        private Brush CreateRaindowGradient()
        {
            var gradientBrush = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0)
            };

            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(0, 228, 0), 0));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 0), 0.2));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 126, 0), 0.4));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 0, 0), 0.6));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(143, 63, 151), 0.8));
            gradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(126, 0, 35), 1));

            return gradientBrush;
        }

        private void airQualityScale_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawScale();
        }
    }
}
