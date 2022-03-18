using LAProjectManagement.Model;
using LAProjectManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LAProjectManagement.ViewModel
{
    class ProjectVM : BaseVM
    {
        public static ProjectVM Instance;
        private ProjectPage _projectPageOwner;

        public ProjectVM(ProjectPage projectPageOwner)
        {
            _projectPageOwner = projectPageOwner;
            Instance = this;
        }

        #region Properties

        private int projectID;
        public int ProjectID
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

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        private int statusID;
        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; OnPropertyChanged("StatusID"); }
        }

        private ObservableCollection<Project> projectsCollections;
        public ObservableCollection<Project> ProjectsCollections
        {
            get
            {
                projectsCollections = DataBaseManager.getAllProjects();
                return projectsCollections;
            }
            set { projectsCollections = value; OnPropertyChanged("ProjectsCollections"); }
        }

        private static Project projectSelect;
        public Project ProjectSelect
        {
            get { return projectSelect; }
            set {

                projectSelect = value;
                if (projectSelect == null)
                {
                    return;
                }

                ProjectID = ProjectSelect.ProjectID;
                OnPropertyChanged("ProjectSelect");

                //Navigate To all Units List
                _projectPageOwner.NavigationService.Navigate(new UnitsPage(ProjectID));
              
            }
        }
        #endregion



        
    }
}

