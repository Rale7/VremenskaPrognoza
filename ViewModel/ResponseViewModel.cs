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

        public AdditionalChangeAction AdditionalAction { get; set; }

        private Response response;

        public Response Response { 
            get 
            { 
                return response; 
            } 
            set 
            { 
                response = value;                
                OnPropertyChanged();
                Application.Current.Dispatcher.Invoke(() => AdditionalAction());
            } 
        }
    }
}
