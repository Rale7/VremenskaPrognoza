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
    /// Interaction logic for UVChart.xaml
    /// </summary>
    public partial class UVChart : UserControl
    {
        private int[] scalePoints = { 0, 2, 4, 6, 8, 10, 12 };

        private CanvasPainter hcp;

        private ResponseViewModel rvm;

        public UVChart()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
            rvm.AdditionalRealtimeAction += RepaintChart;
            hcp = new CanvasPainter(ChartCanvas);
        }

        private void CharCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RepaintChart();
        }

        public void RepaintChart()
        {
            hcp.RecalculateDimensions();

            double chartPercentage;

            try
            {
                chartPercentage = rvm.RealtimeResponse.CurrentWeather.UvIndex;
            } catch (NullReferenceException)
            {
                chartPercentage = 0;
            }
             
            hcp.DrawHalfCircle(20, createGradientBrush(), 
                (chartPercentage / 12.0) * 100);
            hcp.DrawHalfCircle(1, Brushes.White);
            hcp.DrawHalfCircle(6, new SolidColorBrush(Color.FromArgb(50, 255, 255, 255)));
            hcp.DrawPoints<int>(scalePoints);
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
    }
}
