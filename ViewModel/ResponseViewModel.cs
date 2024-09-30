using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VremenskaPrognoza.Model;
using VremenskaPrognoza.MVVM;

namespace VremenskaPrognoza.ViewModel
{
    public class ResponseViewModel : ViewModelBase
    {        
        public delegate void AdditionalChangeAction();

        public AdditionalChangeAction AdditionalRealtimeAction { get; set; }

        public AdditionalChangeAction AdditionalAstronomyAction { get; set; }

        private RealtimeResponse realtimeResponse;

        public RealtimeResponse RealtimeResponse { 
            get 
            { 
                return realtimeResponse; 
            } 
            set 
            { 
                realtimeResponse = value;                
                OnPropertyChanged();
                Application.Current.Dispatcher.Invoke(() => AdditionalRealtimeAction());
            } 
        }

        private AstronomyResponse astronomyResponse;

        public AstronomyResponse AstronomyResponse
        {
            get
            {
                return astronomyResponse;
            }
            set
            {
                astronomyResponse = value;
                OnPropertyChanged();
                Application.Current.Dispatcher.Invoke(() => AdditionalAstronomyAction());
            }
        }
    }
}
