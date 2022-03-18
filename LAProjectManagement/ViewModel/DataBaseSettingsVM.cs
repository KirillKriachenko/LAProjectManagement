using LAProjectManagement.Model;
using LAProjectManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace LAProjectManagement.ViewModel
{
    class DataBaseSettingsVM : BaseVM
    {
        public static DataBaseSettingsVM Instance;
        private DataBaseSettingsPage dataBaseSettingsPageOwner;
        public DataBaseSettingsVM(DataBaseSettingsPage _dataBaseSettingsPageOwner)
        {
            Instance = this;
            dataBaseSettingsPageOwner = _dataBaseSettingsPageOwner;
            departureDate = DateTime.Now;

            DataBaseManager.getAllStatuses();
            OnPropertyChanged("StatsCollection");
        }

        #region Properties

        private string projectID;
        public string ProjectID
        {
            get { return projectID; }
            set { projectID = value; OnPropertyChanged("ProjectID"); }
        }
        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; OnPropertyChanged("ProjectName"); }
        }

        private string projectAdress;
        public string ProjectAddress
        {
            get { return projectAdress; }
            set { projectAdress = value; OnPropertyChanged("ProjectAddress"); }
        }

        private string projectDescription;
        public string ProjectDescroption
        {
            get { return projectDescription; }
            set { projectDescription = value; OnPropertyChanged("ProjectDescroption"); }
        }

        private ObservableCollection<Project> projectsCollection;
        public ObservableCollection<Project> ProjectsCollection
        {
            get { projectsCollection = DataBaseManager.getAllProjects(); return projectsCollection; }
            set { projectsCollection = value; OnPropertyChanged("ProjectsCollection"); }
        }

        private static Project selectProject;
        public Project SelectProject
        {
            get { return selectProject; }
            set {
                selectProject = value;
                if (selectProject == null)
                {
                    return;
                }

                ProjectID = SelectProject.ProjectID.ToString();
                ProjectName = SelectProject.ProjectName;
                OnPropertyChanged("SelectProject");

            }
        }

        private ObservableCollection<Status> statusCollectoin;
        public ObservableCollection<Status> StatsCollection
        {
            get { statusCollectoin = DataBaseManager.getAllStatuses(); return statusCollectoin; }
            set { statusCollectoin = value; OnPropertyChanged("StatsCollection"); }
        }

        private static Status selectStatus;
        public Status SelectStatus
        {
            get { return selectStatus; }
            set {
                selectStatus = value;
                if (selectStatus == null)
                {
                    return;
                }

                OnPropertyChanged("SelectStatus");
            }
        }

        private string unitNumber;
        public string UnitNumber
        {
            get { return unitNumber; }
            set { unitNumber = value; OnPropertyChanged("UnitNumber"); }
        }

        private string unitBarcode;
        public string UnitBarcode
        {
            get { return unitBarcode; }
            set { unitBarcode = value; OnPropertyChanged("UnitBarcode"); }
        }

        private DateTime departureDate;
        public DateTime DepartureDate
        {
            get { return departureDate; }
            set { departureDate = value; OnPropertyChanged("DepartureDate"); }
        }


        private ObservableCollection<string> choosenFiles;
        public ObservableCollection<string> ChoosenFiles
        {
            get { return choosenFiles; }
            set { choosenFiles = value; OnPropertyChanged("ChoosenFiles"); }
        }

        #endregion

        #region Commands

        private DelegateCommand addProjectCommand;
        public DelegateCommand AddProjectCommand
        {
            get { return addProjectCommand ?? (addProjectCommand = new DelegateCommand(AddProjectCommandClick)); }
        }

        private void AddProjectCommandClick(object obj)
        {
            if (ProjectID != string.Empty && ProjectName != string.Empty && SelectStatus != null)
            {
                using (var db = new LivingArtPMContext())
                {
                    //var Status = db.Status.FirstOrDefault(s => s.StatusID == SelectStatus.StatusID);
                    var project = new Project
                    {
                        ProjectID = Convert.ToInt32(ProjectID),
                        ProjectName = ProjectName,
                        Description = ProjectDescroption,
                        Address = ProjectAddress,
                        StatusID = selectStatus.StatusID


                    };
                    db.Projects.Add(project);
                    db.SaveChanges();

                    OnPropertyChanged("ProjectsCollection");
                }
                System.Windows.MessageBox.Show("New project has been added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                System.Windows.MessageBox.Show("Project ID and Project Name fields can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private DelegateCommand chooseFilesCommand;
        public DelegateCommand ChooseFilesCommand
        {
            get { return chooseFilesCommand ?? (chooseFilesCommand = new DelegateCommand(ChooseFilesClick)); }
        }
        private void ChooseFilesClick(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All Files | *.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    foreach (var item in openFileDialog.FileNames)
                    {
                        //ChoosenFiles.Add(item);
                        dataBaseSettingsPageOwner.choosenFilelb.Items.Add(item);
                        OnPropertyChanged("ChoosenFiles");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private DelegateCommand addUnitAndPartCommand;
        public DelegateCommand AddUnitPartCommand
        {
            get { return addUnitAndPartCommand ?? (addUnitAndPartCommand = new DelegateCommand(AddUnitAndPartsClick)); }
        }
        private void AddUnitAndPartsClick(object obj)
        {
            if (dataBaseSettingsPageOwner.choosenFilelb.Items != null && SelectStatus != null && UnitNumber != string.Empty && UnitBarcode != string.Empty && DepartureDate != null)
            {
                int partsCounter = 0;
                int partsIdCounter = 1;
                foreach (var file in dataBaseSettingsPageOwner.choosenFilelb.Items)
                {
                    using (var reader = new StreamReader(file.ToString()))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            if (values[0] == "PIECE")
                            {
                                using (var db = new LivingArtPMContext())
                                {
                                    var parts = new Parts
                                    {
                                        PartFile = values[1],
                                        X = values[2],
                                        Y = values[3],
                                        Quantity = Convert.ToInt32(values[4]),
                                        Grain = values[5],
                                        TagVariables = values[6],
                                        JobName = values[7],
                                        ItemName = values[8],
                                        ItemPart = values[9],
                                        CabinetNum = Convert.ToInt32(values[10]),
                                        PartNum = Convert.ToInt32(values[11]),
                                        MaterialName = values[12],
                                        EdgeInfo = values[13],
                                        Barcode = values[14],
                                        PartOffset = Convert.ToInt32(values[15]),
                                        PartPriority = Convert.ToInt32(values[16]),
                                        PartRotation = Convert.ToInt32(values[17]),
                                        UnitUnitID = Convert.ToInt32(UnitNumber),
                                        ScannedQuantity = 0,
                                        StatusID = SelectStatus.StatusID
                                    };

                                    db.Parts.Add(parts);
                                    db.SaveChanges();
                                    partsIdCounter += 1;
                                    partsCounter += 1;
                                }
                            }
                        }
                    }
                }

                //System.Windows.MessageBox.Show(partsCounter.ToString());

                using (var db = new LivingArtPMContext())
                {
                    var unit = new Unit
                    {
                        Name = UnitNumber,
                        Barcode = UnitBarcode,
                        DepartureDate = DepartureDate.ToString("yyyy-mm-dd"),
                        ProjectProjectID = SelectProject.ProjectID,
                        PartsAmount = partsCounter.ToString(),
                        PartsScanned = "0",
                        StatusID = SelectStatus.StatusID

                    };

                    db.Units.Add(unit);
                    db.SaveChanges();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("All unit data should be filled in and unit csv file should be choosen", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

    }
}
