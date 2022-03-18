using LAProjectManagement.Model;
using LAProjectManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAProjectManagement.ViewModel
{
    class MainWindowVM : BaseVM
    {
        private MainWindow _windowOwner;

        public MainWindowVM(MainWindow windowOwner)
        {
            UnSelect();
            IsProjectPageSelected = true;

            _windowOwner = windowOwner;
            _windowOwner.FrameMain.NavigationService.Navigate(new ProjectPage());
        }


        #region Properties
        private bool isProjectPageSelected = false;
        public bool IsProjectPageSelected
        {
            get { return isProjectPageSelected; }
            set { isProjectPageSelected = value; OnPropertyChanged("ButtonProjectTickness"); }
        }
        public int ButtonProjectTickness
        {
            get { return IsProjectPageSelected ? 0 : 1; }
        }


        private bool isDataBaseSettingsPageSelected = false;
        public bool IsDataBaseSettingsPageSelected
        {
            get { return isDataBaseSettingsPageSelected; }
            set { isDataBaseSettingsPageSelected = value; OnPropertyChanged("ButtonDataBaseSettingsTickness"); }
        }

        public int ButtonDataBaseSettingsTickness
        {
            get { return IsDataBaseSettingsPageSelected ? 0 : 1; }
        }


        private bool isRecutPageSelected = false;
        public bool IsRecutPageSelected
        {
            get { return isRecutPageSelected; }
            set { isRecutPageSelected = value; OnPropertyChanged("ButtonRecutPageTickness"); }
        }
        public int ButtonRecutPageTickness
        {
            get { return IsRecutPageSelected ? 0 : 1; }
        }

        public void UnSelect()
        {
            IsProjectPageSelected = false;
            IsDataBaseSettingsPageSelected = false;
            IsRecutPageSelected = false;
        }

        #endregion


        #region Commands

        private DelegateCommand projectOpen;
        public DelegateCommand ProjectOpen { get { return projectOpen ?? (projectOpen = new DelegateCommand(OpenProjectClick)); } }
        private void OpenProjectClick(object obj)
        {
            UnSelect();
            IsProjectPageSelected = true;

            _windowOwner.FrameMain.NavigationService.Navigate(new ProjectPage());
        }

        private DelegateCommand dataSettingsOpen;
        public DelegateCommand DataSettingsOpen
        {
            get { return dataSettingsOpen ?? (dataSettingsOpen = new DelegateCommand(OpenDataSettingsClick)); }
        }

        private void OpenDataSettingsClick(object obj)
        {
            UnSelect();
            IsDataBaseSettingsPageSelected = true;

            _windowOwner.FrameMain.NavigationService.Navigate(new DataBaseSettingsPage());
        }

        private DelegateCommand recutOpen;
        public DelegateCommand RecutOpen { get { return recutOpen ?? (recutOpen = new DelegateCommand(OpenRecutClick)); } }
        private void OpenRecutClick(object obj)
        {
            UnSelect();
            IsRecutPageSelected = true;

            _windowOwner.FrameMain.NavigationService.Navigate(new RecutPage());
        }
        #endregion
    }
}
