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
using VremenskaPrognoza.View.IconDrawers;
using VremenskaPrognoza.View.IconDrawers.Common;
using VremenskaPrognoza.View.IconDrawers.Day;

namespace VremenskaPrognoza.View.UserControls
{
    
    public partial class WeatherIcon : UserControl
    {
        private IconDrawerFactory iconDrawerFactory;
        private Thread repainter;

        public WeatherIcon()
        {
            InitializeComponent();

            iconDrawerFactory = new IconDrawerFactory(icon);

            repainter = new Thread(run);
            repainter.IsBackground = true;
            repainter.Start();

        }

        protected void run()
        {
            while (true) {
                Dispatcher.BeginInvoke(() => {
                    iconDrawerFactory.GetIconPainter(176, false).RecalculateAndPaint();                                        
                });
                Thread.Sleep(50);
            }
        }

    }
}
