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
using VremenskaPrognoza.View.MoonDrawers;
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    /// <summary>
    /// Interaction logic for MoonControl.xaml
    /// </summary>
    public partial class MoonControl : UserControl
    {
        private ResponseViewModel rvm;

        private BaseMoonDrawer[] moonDrawers;        
        public MoonControl()
        {
            InitializeComponent();
            rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;

            moonDrawers = new BaseMoonDrawer[]
            {
                new FirstQuaterMoonDrawer(moonCanvas),
                new SecondQuaterMoonDrawer(moonCanvas),
                new ThirdQuaterMoonDrawer(moonCanvas),
                new ForthQuaterMoonDrawer(moonCanvas)
            };

            rvm.AdditionalAstronomyAction += DrawMoon;
        }

        public void DrawMoon()
        {
            moonCanvas.Children.Clear();
            try
            {
                double percentage = rvm.AstronomyResponse.Astronomy.Astro.MoonIllumination;
                String phase = rvm.AstronomyResponse.Astronomy.Astro.Moonphase;

                moonDrawers[(int)BaseMoonDrawer.PhaseIndex(percentage, phase)].DrawMoonType(percentage);
            } catch (NullReferenceException)
            {
                //moonDrawers[0].DrawMoonType(100);
            }
        }

        private void moonCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawMoon();
        }
    }
}
