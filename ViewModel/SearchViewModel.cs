using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VremenskaPrognoza.APICalling;
using VremenskaPrognoza.Model;
using VremenskaPrognoza.MVVM;

namespace VremenskaPrognoza.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; }

        private String location;

        public String Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        public SearchViewModel()
        {
            SearchCommand = new RelayCommand(Search);
        }

        private void Search(object obj)
        {            
            VMFactory.Instance.Location = Location;
            Location = "";
        }
    }
}
