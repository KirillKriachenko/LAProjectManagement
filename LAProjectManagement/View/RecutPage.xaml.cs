﻿using LAProjectManagement.ViewModel;
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

namespace LAProjectManagement.View
{
    /// <summary>
    /// Interaction logic for RecutPage.xaml
    /// </summary>
    public partial class RecutPage : Page
    {
        public RecutVM recutVM; 
        public RecutPage()
        {
            InitializeComponent();

            recutVM = new RecutVM(this);
            DataContext = recutVM;
        }
    }
}
