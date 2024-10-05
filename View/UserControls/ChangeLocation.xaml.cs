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
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    /// <summary>
    /// Interaction logic for ChangeLocation.xaml
    /// </summary>
    public partial class ChangeLocation : UserControl
    {


        public ChangeLocation()
        {
            InitializeComponent();
            DataContext = new SearchViewModel();
        }       

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkText();
        }

        private void searchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            placeholder.Visibility = Visibility.Hidden;
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            checkText();
        }

        private void checkText()
        {
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                placeholder.Visibility = Visibility.Visible;
            }
            else
            {
                placeholder.Visibility = Visibility.Hidden;
            }
        }
    }
}
