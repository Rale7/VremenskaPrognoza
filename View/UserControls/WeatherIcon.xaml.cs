using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using VremenskaPrognoza.View.IconDrawers;
using VremenskaPrognoza.View.IconDrawers.Common;
using VremenskaPrognoza.View.IconDrawers.Day;
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    
    public partial class WeatherIcon : UserControl
    {
        private ResponseViewModel rvm;

        private IconDrawerFactory iconDrawerFactory;
        private Thread repainter;

        public WeatherIcon()
        {
            InitializeComponent();

            iconDrawerFactory = new IconDrawerFactory(icon);

            repainter = new Thread(run);
            repainter.IsBackground = true;
            repainter.Start();

            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;

        }

        protected void run()
        {
            while (true) {
                try
                {
                    int code = rvm.RealtimeResponse.CurrentWeather.CurrentCondition.Code;
                    bool isDay = rvm.RealtimeResponse.CurrentWeather.IsDay;

                    Dispatcher.BeginInvoke(() =>
                    {
                        iconDrawerFactory.GetIconPainter(code, isDay).RecalculateAndPaint();
                    });
                    Thread.Sleep(50);
                } catch (NullReferenceException)
                {

                }
            }
        }

    }
}
