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

namespace VremenskaPrognoza.View.UserControls
{
    
    public partial class WeatherIcon : UserControl
    {
        private CanvasIconPainter canvasIconPainter;
        
        private IconPainter iconPainter;

        private Thread repainter;

        public WeatherIcon()
        {
            InitializeComponent();
            canvasIconPainter = new CanvasIconPainter(icon);
            iconPainter = new NightClearIcon(canvasIconPainter);

            repainter = new Thread(run);
            repainter.IsBackground = true;
            repainter.Start();

        }

        protected void run()
        {
            while (true) {
                Dispatcher.BeginInvoke(() => {
                    iconPainter.Paint();
                });
                Thread.Sleep(50);
            }
        }

    }
}
