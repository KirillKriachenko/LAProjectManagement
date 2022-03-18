using LAProjectManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAProjectManagement.Model
{
    public class ScannedModel : BaseVM
    {
        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; OnPropertyChanged("ProjectName"); }
        }

        private int projectID;
        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; OnPropertyChanged("ProjectID"); }
        }


        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged("Unit"); }
        }

        private string existQuantity;
        public string ExistQuantity
        {
            get { return existQuantity; }
            set { existQuantity = value; OnPropertyChanged("ExistQuantity"); }
        }


        private string scannedQuantity;
        public string ScannedQuantity
        {
            get { return scannedQuantity; }
            set { scannedQuantity = value; OnPropertyChanged("ScannedQuantity"); }
        }

        private string partQuantity;
        public string PartQuantity
        {
            get { return partQuantity; }
            set { partQuantity = value; OnPropertyChanged("PartQuantity"); }
        }

    }
}
