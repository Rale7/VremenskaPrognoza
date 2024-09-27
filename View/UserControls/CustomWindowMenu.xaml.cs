
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using VremenskaPrognoza.ViewModel;


namespace VremenskaPrognoza.UI.UserControls
{
    /// <summary>
    /// Interaction logic for CustomWindowMenu.xaml
    /// </summary>
    public partial class CustomWindowMenu : UserControl
    {       

        public CustomWindowMenu()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();            
        }

    }
}
