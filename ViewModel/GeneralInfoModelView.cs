using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VremenskaPrognoza.Model;
using VremenskaPrognoza.MVVM;
using VremenskaPrognoza.UI.UserControls;

namespace VremenskaPrognoza.ViewModel
{
    public class GeneralInfoModelView : ViewModelBase
    {

        private BasicInfo gim;

        public BasicInfo MyGeneralInfo
        {
            get { return gim; }
            set
            {
                gim = value;
                OnPropertyChanged();
            }
        }

        private DateLocation dl;

        public DateLocation MyDateLocation
        {
            get { return dl; }
            set
            {
                dl = value;
                OnPropertyChanged();
            }
        }

        public GeneralInfoModelView()
        {
            gim = new BasicInfo(26.6, "Sunny", "https://cdn.weatherapi.com/weather/64x64/day/113.png");
            dl = new DateLocation(new DateTime(), "Belgrade");
        }   

    }
}
