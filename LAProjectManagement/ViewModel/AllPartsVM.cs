using LAProjectManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAProjectManagement.ViewModel
{
    public class AllPartsVM : BaseVM
    {
        public static AllPartsVM Instance;
        static DateTime dateTime;
        public AllPartsVM(string _projectName, string _barcode)
        {
            dateTime = DateTime.Now;

            Instance = this;
            projectName = _projectName;
            barCode = _barcode;
        }

        #region Properites

        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged("Unit"); OnPropertyChanged("UnitID"); }
        }

        private string barCode;
        public string BarCode
        {
            get { return barCode; }
            set { barCode = value; OnPropertyChanged("BarCode"); }
        }
        private string projectName;
        public string ProjectName
        {
            get { projectName = DataBaseManager.FindProjectName(Convert.ToInt32(BarCode.Substring(0, 3))); return projectName; }
            set { projectName = value; OnPropertyChanged("ProjectName"); }
        }

        private string partFile;
        public string PartFile
        {
            get { return partFile; }
            set { partFile = value; OnPropertyChanged("PartFile"); }
        }

        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged("Quantity"); }
        }

        private string jobName;
        public string JobName
        {
            get { return jobName; }
            set { jobName = value; OnPropertyChanged("JobName"); }
        }

        private string itemName;
        public string ItemName
        {
            get { return itemName + " " + itemPart; }
            set { itemName = value; OnPropertyChanged("ItemName"); }
        }

        private string cabinetNum;
        public string CabinetNum
        {
            get { return cabinetNum; }
            set { cabinetNum = value; OnPropertyChanged("CabinetNum"); }
        }

        private string partNum;
        public string PartNum
        {
            get { return partNum; }
            set { partNum = value; OnPropertyChanged("PartNum"); }
        }

        private string itemPart;
        public string ItemPart
        {
            get { return itemPart; }
            set { itemPart = value; OnPropertyChanged("ItemPart"); }
        }

        private string materialName;
        public string MaterialName
        {
            get { return materialName; }
            set { materialName = value; OnPropertyChanged("MaterialName"); }
        }

        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; OnPropertyChanged("Barcode"); }
        }

        private string scannedQuantity;
        public string ScannedQuantity
        {
            get { return scannedQuantity; }
            set { scannedQuantity = value; OnPropertyChanged("ScannedQuantity"); }
        }

        private string statusID;
        public string StatusID
        {
            get { return statusID; }
            set { statusID = value; OnPropertyChanged("StatusID"); }
        }

        private ObservableCollection<Parts> unitPartsCollection;
        public ObservableCollection<Parts> UnitPartsCollection
        {
            get
            {
                unitPartsCollection = DataBaseManager.getAllParts(BarCode);
                return unitPartsCollection;
            }
            set
            {
                unitPartsCollection = value;
                OnPropertyChanged("UnitPartsCollection");
                OnPropertyChanged("Unit");
            }
        }

        private ObservableCollection<Unit> unitCollection;
        public ObservableCollection<Unit> UnitCollection
        {
            get { unitCollection = DataBaseManager.getAllUnits(ProjectName); return unitCollection; }
            set { unitCollection = value; OnPropertyChanged("UnitCollection"); }
        }

        private static Unit selectUnit;
        public Unit SelectUnit
        {
            get { return selectUnit; }
            set
            {
                selectUnit = value;
                if (selectUnit == null)
                {
                    return;
                }
                barCode = SelectUnit.Barcode;
                //UnitPartsCollection = DataBaseManager.getAllParts(SelectUnit.Barcode);
                OnPropertyChanged("SelectUnit");
                OnPropertyChanged("UnitPartsCollection");
                //ProjectName = SelectProject.ProjectName;
                //ProjectID = SelectProject.ProjectID;

                //OnPropertyChanged("SelectProject");
                //OnPropertyChanged("ProjectName");
                //FillUserControl();
            }
        }

        private Parts  selectedPart;
        public Parts SelectedPart
        {
            get { return selectedPart; }
            set {
                selectedPart = value;
                if (selectedPart == null)
                {
                    return;
                }

                barcode = SelectedPart.Barcode;
                OnPropertyChanged("SelectedPart");

            }
        }


        #endregion

        #region Commands

        private DelegateCommand addToRecuts;
        public DelegateCommand AddToRecuts
        {
            get { return addToRecuts ?? (addToRecuts = new DelegateCommand(AddRecutPart)); }
        }
        private void AddRecutPart(object obj)
        {
            if (selectedPart != null && selectedPart.StatusID != 2)
            {
                using (var db = new LivingArtPMContext())
                {
                    var query = (from part in db.Parts
                                 where part.Barcode == selectedPart.Barcode
                                 select part).FirstOrDefault();

                    query.StatusID = 3;

                    DataBaseManager.markUnitAsRecuts(selectedPart.Barcode.Substring(0, selectedPart.Barcode.Length - 4));

                    var recut = new Recut
                    {
                        OrderDate = query.JobName,
                        Issue = query.Barcode
                    };
                    db.Recuts.Add(recut);
                    db.SaveChanges();
                }
            }
        }

        private DelegateCommand prepareMisingCommand;
        public DelegateCommand PrepareMisingCommand
        {
            get { return prepareMisingCommand ?? (prepareMisingCommand = new DelegateCommand(PrepareMisingClick)); }
        }
        private void PrepareMisingClick(object obj)
        {
            //Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var csv = new StringBuilder();
                for (int i = 0; i < UnitPartsCollection.Count; i++)
                {
                    if (UnitPartsCollection[i].StatusID != 2)
                    {
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", "PIECE", UnitPartsCollection[i].PartFile, UnitPartsCollection[i].X, UnitPartsCollection[i].Y, UnitPartsCollection[i].Quantity, UnitPartsCollection[i].Grain, UnitPartsCollection[i].TagVariables, UnitPartsCollection[i].JobName, UnitPartsCollection[i].ItemName, UnitPartsCollection[i].ItemPart, UnitPartsCollection[i].CabinetNum, UnitPartsCollection[i].PartNum, UnitPartsCollection[i].MaterialName, UnitPartsCollection[i].EdgeInfo, UnitPartsCollection[i].Barcode, UnitPartsCollection[i].PartOffset, UnitPartsCollection[i].PartPriority, UnitPartsCollection[i].PartRotation+"\r");
                        csv.Append(newLine);
                    }                    
                }
                File.AppendAllText(saveFileDialog.FileName, csv.ToString());
            }
            MessageBox.Show("File was created successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private DelegateCommand addMissingPartsCommand;
        public DelegateCommand AddMissingPartCommand
        {
            get { return addMissingPartsCommand ?? (addMissingPartsCommand = new DelegateCommand(AddMissingPart)); }
        }

        private void AddMissingPart(object obj)
        {
            if (selectedPart != null && selectedPart.StatusID != 2)
            {
                using (var db =  new LivingArtPMContext())
                {
                    var part = (from parts in db.Parts
                                where parts.Barcode == selectedPart.Barcode
                                select parts).FirstOrDefault();
                    if (part.ScannedQuantity < part.Quantity)
                    {
                        part.ScannedQuantity += 1;
                        if (part.ScannedQuantity == part.Quantity)
                        {
                            part.StatusID = 2;

                        }

                        string value = part.Barcode.Substring(0, part.Barcode.Length - 4);
                        Console.WriteLine("UNIT BARCODE IS " + value);
                        var unit = (from units in db.Units
                                    where units.Barcode == value
                                    select units).FirstOrDefault();

                        try
                        {
                            unit.PartsScanned = (Convert.ToInt32(unit.PartsScanned) + 1).ToString();
                            if (unit.PartsScanned != null)
                            {
                                unit.StatusID = 5;
                            }
                            if (unit.PartsAmount == unit.PartsScanned)
                            {
                                unit.StatusID = 4;
                                Console.WriteLine("Unit :" + unit.Name + "was ready for departure at" + dateTime.ToString("yyyy-mm-dd HH:mm:ss"));
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }

                    db.SaveChanges();
                   
                }
            }
            OnPropertyChanged("UnitPartsCollection");
            OnPropertyChanged("Unit");
        }
        #endregion
    }
}
