﻿using System;
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
using VremenskaPrognoza.ViewModel;

namespace VremenskaPrognoza.View.UserControls
{
    /// <summary>
    /// Interaction logic for GeneralInfo.xaml
    /// </summary>
    public partial class GeneralInfo : UserControl
    {
        public GeneralInfo()
        {
            InitializeComponent();
            ResponseViewModel rvm = VMFactory.Instance.Rvm;
            DataContext = rvm;
        }
    }
}
