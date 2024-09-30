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

        public SunControl()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalAstronomyAction += DrawSunGraph;
            hcp = new CanvasPainter(sunGraph);
        }

        public void DrawSunGraph()
        {
            hcp.RecalculateDimensions();

            hcp.DrawHalfCircle(1, Brushes.Gray, 100);

            try
            {                
                TimeOnly sunrise = TimeOnly.Parse(rvm.AstronomyResponse.Astronomy.Astro.Sunrise);
                TimeOnly sunset = TimeOnly.Parse(rvm.AstronomyResponse.Astronomy.Astro.Sunset);
                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
                
                if (currentTime > sunrise && currentTime < sunset)
                {                    
                    TimeSpan dayDuration = sunset - sunrise;
                    TimeSpan currentDuration = currentTime - sunrise;

                    double percentage = 100.0 * ((double)
                        (currentDuration.TotalSeconds / dayDuration.TotalSeconds));

                    hcp.DrawHalfCircle(1, Brushes.Yellow, percentage);
                    DrawSun(percentage);
                }
            }
            catch (NullReferenceException) 
            {
                
            }
        }

        private void DrawSun(double percentage)
        {
            double angle = Math.PI * percentage / 100;

            double circleX = hcp.CenterX - Math.Cos(angle) * hcp.Radius;
            double circleY = hcp.CenterY - Math.Sin(angle) * hcp.Radius;

            double circleRadius = hcp.Radius * 0.1;

            hcp.DrawCircle(circleX, circleY, circleRadius, Brushes.Yellow, Brushes.Transparent);
        }

        private void sunGraph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawSunGraph();
        }
    }
}
