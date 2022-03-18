using LAProjectManagement.ViewModel;
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
    /// Interaction logic for UnitsUC.xaml
    /// </summary>
    public partial class UnitsUC : UserControl
    {

        public UnitsUserControlVM unitsControlVM;

        public UnitsUC(int rowNumber, ProjectPage _projectPageOwner)
        {
            InitializeComponent();
            unitsControlVM = new UnitsUserControlVM(rowNumber, _projectPageOwner);
            DataContext = unitsControlVM;
        }



        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //unitsControlVM.OpenGetAllParts(sender);
        }
    }
}
