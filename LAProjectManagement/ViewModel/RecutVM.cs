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
    public class RecutVM : BaseVM
    {
        public static RecutVM Instance;
        private RecutPage _projectPageOwner;

        public RecutVM(RecutPage projectPageOwner)
        {
            _projectPageOwner = projectPageOwner;
            Instance = this;
        }

        #region Properties

        private ObservableCollection<Project> projectCollection;
        public ObservableCollection<Project> ProjectsCollection
        {
            get { projectCollection = DataBaseManager.getAllProjects(); return projectCollection; }
            set { projectCollection = value; OnPropertyChanged("ProjectsCollection"); }
        }

        private static Project projectSelected;
        public Project ProjectSelected
        {
            get { return projectSelected; }
            set
            {
                projectSelected = value;
                if (projectSelected == null)
                {
                    return;
                }
                unitCollection = DataBaseManager.getAllUnitsForRecuts(ProjectSelected.ProjectID);
                OnPropertyChanged("ProjectSelected");
                OnPropertyChanged("UnitCollection");
            }
        }

        private ObservableCollection<Unit> unitCollection;
        public ObservableCollection<Unit> UnitCollection
        {
            get {  return unitCollection; }
            set { unitCollection = value; OnPropertyChanged("UnitCollection"); }
        }

        private static Unit selectedUnit;
        public Unit SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                selectedUnit = value;
                if (selectedUnit == null)
                {
                    return;
                }
                _projectPageOwner.SelectedUnitsList.Items.Add(SelectedUnit.Barcode);
                OnPropertyChanged("SelectedUnit");

            }
        }

        private ObservableCollection<Parts> unitPartsCollection;
        public ObservableCollection<Parts> UnitPartsCollection
        {
            get
            {
               // unitPartsCollection = DataBaseManager.getAllParts(BarCode);
                return unitPartsCollection;
            }
            set
            {
                unitPartsCollection = value;
                OnPropertyChanged("UnitPartsCollection");
                OnPropertyChanged("Unit");
            }
        }

        #endregion

        #region Commands

        private DelegateCommand makeRecutCommand;
        public DelegateCommand MakeRecutCommand
        {
            get { return makeRecutCommand ?? (makeRecutCommand = new DelegateCommand(DoRecutFile)); }
        }
        public void DoRecutFile(object obj)
        {
            //Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var csv = new StringBuilder();
                foreach (var item in _projectPageOwner.SelectedUnitsList.Items)
                {
                    unitPartsCollection = DataBaseManager.getAllParts(item.ToString());
                    OnPropertyChanged("UnitPartsCollection");
                    for (int i = 0; i < UnitPartsCollection.Count; i++)
                    {
                        if (UnitPartsCollection[i].StatusID != 2)
                        {
                            DataBaseManager.markAsRecuts(UnitPartsCollection[i].Barcode);
                            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", "PIECE", UnitPartsCollection[i].PartFile, UnitPartsCollection[i].X, UnitPartsCollection[i].Y, UnitPartsCollection[i].Quantity, UnitPartsCollection[i].Grain, UnitPartsCollection[i].TagVariables, UnitPartsCollection[i].JobName, UnitPartsCollection[i].ItemName, UnitPartsCollection[i].ItemPart, UnitPartsCollection[i].CabinetNum, UnitPartsCollection[i].PartNum, UnitPartsCollection[i].MaterialName, UnitPartsCollection[i].EdgeInfo, UnitPartsCollection[i].Barcode, UnitPartsCollection[i].PartOffset, UnitPartsCollection[i].PartPriority, UnitPartsCollection[i].PartRotation + "\r\n");
                            csv.Append(newLine);
                        }
                        DataBaseManager.markUnitAsRecuts(UnitPartsCollection[i].Barcode.Substring(0, UnitPartsCollection[i].Barcode.Length-4));
                    }
                   
                }
                File.AppendAllText(saveFileDialog.FileName, csv.ToString());
            }
            System.Windows.Forms.MessageBox.Show("File was created successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #endregion
    }
}
