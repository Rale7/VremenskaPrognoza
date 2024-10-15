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
using VremenskaPrognoza.View.CanvasDraw;
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    /// <summary>
    /// Interaction logic for SunControl.xaml
    /// </summary>
    public partial class SunControl : UserControl
    {
        private ResponseViewModel rvm;
        private CanvasPainter hcp;

        private Brush sunColor = new SolidColorBrush(Color.FromRgb(255, 223, 34));
        private Brush outlineSunColor = new SolidColorBrush(Color.FromArgb(100, 255, 223, 34));

        public SunControl()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalAstronomyAction += DrawSunGraph;
            hcp = new CanvasPainter(sunGraph);
            
        }

        public async Task DrawSunGraph()
        {
            hcp.ClearCanvas();
            hcp.RecalculateDimensions();

            hcp.DrawHalfCircle(1, Brushes.Gray, 100);

            try
            {                
                TimeOnly sunrise = TimeOnly.Parse(rvm.AstronomyResponse.Astronomy.Astro.Sunrise);
                TimeOnly sunset = TimeOnly.Parse(rvm.AstronomyResponse.Astronomy.Astro.Sunset);                
                TimeOnly currentTime = TimeOnly.FromDateTime(
                    DateTime.Parse(rvm.AstronomyResponse.Location.Localtime));
                
                if (currentTime > sunrise && currentTime < sunset)
                {                    
                    TimeSpan dayDuration = sunset - sunrise;
                    TimeSpan currentDuration = currentTime - sunrise;

                    double percentage = 100.0 * ((double)
                        (currentDuration.TotalSeconds / dayDuration.TotalSeconds));

                    hcp.DrawHalfCircle(1, sunColor, percentage);
                    DrawSun(percentage, sunColor);
                    DrawSun(percentage, outlineSunColor, 0.13);
                }
            }
            catch (NullReferenceException) 
            {
                
            }
        }

        private void DrawSun(double percentage, Brush color, double scaleRadius = 0.1)
        {
            double angle = Math.PI * percentage / 100;

            double circleX = hcp.CenterX - Math.Cos(angle) * hcp.Radius;
            double circleY = hcp.CenterY - Math.Sin(angle) * hcp.Radius;

            double circleRadius = hcp.Radius * scaleRadius;

            hcp.DrawCircle(circleX, circleY, circleRadius, color, Brushes.Transparent);
        }

        private void sunGraph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawSunGraph();
        }
    }
}
