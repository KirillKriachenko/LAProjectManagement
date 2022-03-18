using LAProjectManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAProjectManagement.Model
{
    class CSVFilesModel : BaseVM
    {
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; OnPropertyChanged("FileName"); }
        }

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; OnPropertyChanged("FilePath"); }
        }
    }
}
