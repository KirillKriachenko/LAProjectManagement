using LAProjectManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAProjectManagement.ViewModel
{
    public class UnitsUserControlVM : BaseVM
    {

        ProjectPage _pageOwner;
        public static UnitsUserControlVM Instance;
        private int _rowNumber;
        public UnitsUserControlVM(int rowNumber, ProjectPage projectPageOwner)
        {
            Instance = this;
            _rowNumber = rowNumber;
            _pageOwner = projectPageOwner;
        }

        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; OnPropertyChanged("ProjectName"); }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged("Unit"); }
        }

        private string barCode;
        public string BarCode
        {
            get { return barCode; }
            set { barCode = value; OnPropertyChanged("BarCode"); }
        }

        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged("Quantity"); }
        }


        //#region Command

        private DelegateCommand openCommand;
        public DelegateCommand OpenCommand
        {
            get { return openCommand ?? (openCommand = new DelegateCommand(OpenGetAllParts)); }
        }

        public void OpenGetAllParts(object obj)
        {
            _pageOwner.NavigationService.Navigate(new AllPartsPage(ProjectName,BarCode));
        }


        //#endregion
    }
}
