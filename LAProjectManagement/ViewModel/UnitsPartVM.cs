using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAProjectManagement.Model;
using LAProjectManagement.View;

namespace LAProjectManagement.ViewModel
{
    public class UnitsPartVM : BaseVM
    {
        public static UnitsPartVM Instance;
        private UnitsPage _projectPageOwner;
        public UnitsPartVM(UnitsPage projectPageOwner, int projectID)
        {
            _projectPageOwner = projectPageOwner;
            Instance = this;
            ProjectProjectID = projectID;
            unitsCollection = DataBaseManager.getAllUnits(ProjectProjectID);
            OnPropertyChanged("UnitsCollection");
        }

        #region Properties

        private int projectProjectID;
        public int ProjectProjectID
        {
            get { return projectProjectID; }
            set { projectProjectID = value; OnPropertyChanged("ProjectProjectID"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; OnPropertyChanged("Barcode"); }
        }

        private string partsAmount;
        public string PartsAmount
        {
            get { return partsAmount; }
            set { partsAmount = value; OnPropertyChanged("PartsAmount"); }
        }

        private string partsScanned;
        public string PartsScanned
        {
            get { return partsScanned; }
            set { partsScanned = value; OnPropertyChanged("PartsScanend"); }
        }

        private int statusID;
        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; OnPropertyChanged("StatusID"); }
        }

        private ObservableCollection<Unit> unitsCollection;
        public ObservableCollection<Unit> UnitsCollection
        {
            get { unitsCollection = DataBaseManager.getAllUnits(ProjectProjectID); return unitsCollection; }
            set { unitsCollection = value; OnPropertyChanged("UnitsCollection"); }
        }

        private static Unit unitSelected;
        public Unit UnitSelected
        {
            get { return unitSelected; }
            set
            {
                unitSelected = value;
                if (unitSelected == null)
                {
                    return;
                }
                Barcode = UnitSelected.Barcode;
                Name = UnitSelected.Name;

                //Navigate to All Parts
                _projectPageOwner.NavigationService.Navigate(new AllPartsPage(Name,Barcode));
            }
        }

        private ObservableCollection<Status> statusesCollection;
        public ObservableCollection<Status> StatusesCollection
        {
            get { statusesCollection = DataBaseManager.getAllStatuses(); return statusesCollection; }
            set
            {
                statusesCollection = value; OnPropertyChanged("StatusesCollection");
            }
        }

        private static Status selectStatus;
        public Status SelectStatus
        {
            get { return selectStatus; }
            set
            {
                selectStatus = value;
                if (selectStatus == null)
                {
                    return;
                }
                unitsCollection = DataBaseManager.getAllUnitsByStatus(ProjectProjectID,SelectStatus.StatusID);
                OnPropertyChanged("SelectStatus");
                OnPropertyChanged("UnitsCollection");

            }
        }

        #endregion

        #region Commands

        private DelegateCommand goBackCommand;
        public DelegateCommand GoBackCommand
        { get { return goBackCommand ?? (goBackCommand = new DelegateCommand(GoBack)); } }
        public void GoBack(object obj)
        {
            _projectPageOwner.NavigationService.GoBack();
        }


        #endregion
    }
}
