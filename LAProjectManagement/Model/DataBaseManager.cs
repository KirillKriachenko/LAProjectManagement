using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAProjectManagement.Model
{
    public class DataBaseManager
    {
        //public static int findStatusID(string statusName)
        //{
        //    using (var db = new LivingArtPMContext())
        //    {
        //        var query = (from status in db.Status
        //                     where status.Name == statusName
        //                     orderby status.StatusID
        //                     select status.StatusID).FirstOrDefault();
        //        return query;
        //    }
        //}

        public static string FindProjectName(int projectID)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = (from project in db.Projects
                            where project.ProjectID == projectID
                            select project).FirstOrDefault();

                return query.ProjectName;
            }
        }

        public static ObservableCollection<Project> getProjectWithStatus(int statusID)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from project in db.Projects
                            where project.StatusID == statusID
                            select project;
                return new ObservableCollection<Project>(query);
            }
        }

        public static ObservableCollection<Unit> getAllUnitsByStatus(int projectID,int statusID)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from units in db.Units
                            where units.StatusID == statusID
                            select units;
                return new ObservableCollection<Unit>(query);
            }
        }

        public static ObservableCollection<Status> getAllStatuses()
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from status in db.Status
                            orderby status.StatusID
                            select status;
                return new ObservableCollection<Status>(query);
            }
        }

        public static ObservableCollection<Unit> getSelectedUnit(int projectID, int unitID)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from table in db.Units
                            join projectTable in db.Projects on table.ProjectProjectID equals projectTable.ProjectID
                            where table.ProjectProjectID == projectID && table.UnitID == unitID
                            select table;

                return new ObservableCollection<Unit>(query);
            }
        }

        public static ObservableCollection<Project> getAllProjects()
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from table in db.Projects
                             orderby table.ProjectName
                             select table;
                return new ObservableCollection<Project>(query);
            }
        }
        public static int CalculateUnits(int projectID)
        {
            int counter = 0;
            using (var db = new LivingArtPMContext())
            {
                foreach (var item in db.Units)
                {
                    if (item.ProjectProjectID == projectID)
                    {
                        counter += 1;
                    }
                }
            }

            return counter;
        }

        //public static ObservableCollection<Project> getAllProjectsAndUnits()
        //{
        //    using (var db = new LivingArtPMContext())
        //    {
        //        var query = from table in db.Projects
        //                    join unit in db.Units on table
        //                    orderby table.ProjectID
        //                    select table;
        //        return new ObservableCollection<Project>(query);
        //    }
        //}

        //public static Unit projectUnits(int projectID)
        //{
        //    using (var db = new LivingArtPMContext())
        //    {
        //        var query = from unit in db.Units
        //                    where unit.ProjectProjectID == projectID
        //                    select unit;

        //        Unit myUnit = new Unit();
        //        myUnit.UnitID = query.
        //    }
        //}

        public static ObservableCollection<Unit> getLastUnits(int number)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = (from table in db.Units
                             join projectTable in db.Projects on table.ProjectProjectID equals projectTable.ProjectID
                             select table).Take(number);
                return new ObservableCollection<Unit>(query);
            }
        }

        public static ObservableCollection<Unit> getAllUnits(int projectName)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = (from table in db.Units
                            join projectTable in db.Projects on table.ProjectProjectID equals projectTable.ProjectID
                            where table.ProjectProjectID == projectName 
                            select table).OrderByDescending(d => d.UnitID);
                return new ObservableCollection<Unit>(query);
            }
        }

        public static ObservableCollection<Unit> getAllUnitsForRecuts(int projectName)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from table in db.Units
                            join projectTable in db.Projects on table.ProjectProjectID equals projectTable.ProjectID
                            where table.ProjectProjectID == projectName && table.StatusID == 5
                            select table;
                return new ObservableCollection<Unit>(query);
            }
        }

        public static ObservableCollection<Unit> getUnits()
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from p in db.Units.Include("Project")
                            select p;
                return new ObservableCollection<Unit>(query);
            }
        }


        public static ObservableCollection<Unit> getAllUnits(string projectName)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from table in db.Units
                            join projectTable in db.Projects on table.ProjectProjectID equals projectTable.ProjectID
                            where projectTable.ProjectName == projectName
                            select table;
                return new ObservableCollection<Unit>(query);
            }
        }

        public static ObservableCollection<Scanned> getAllScannedUnits()
        {
            using (var db = new LivingArtPMContext())
            {
                var query = from table in db.Scanneds
                           orderby table.DateTime
                           select table;
                return new ObservableCollection<Scanned>(query);
            }
        }

        public static int findUnitID(string unitName)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = (from unit in db.Units
                            where unit.Name == unitName
                            orderby unit.UnitID
                            select unit.UnitID).FirstOrDefault();

                return query;
            }
        }


        public static ObservableCollection<Parts> getAllParts(string barCode)
        {
            using (var db = new LivingArtPMContext())
            {
                //string value = barCode.Substring(0, bar //part.Barcode.Substring(0, part.Barcode.Length - 4);

                var query = from parts in db.Parts
                            where parts.Barcode.Substring(0,parts.Barcode.Length -4) == barCode  //parts.Barcode.Substring(0,7) == barCode
                            select parts;

                return new ObservableCollection<Parts>(query);
            }
        }

        public static void markAsRecuts(string barCode)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = (from parts in db.Parts
                             where parts.Barcode == barCode
                             select parts).FirstOrDefault();
                query.StatusID = 3;
                db.SaveChanges();
            }
        }
        public static void markUnitAsRecuts(string unitBarCode)
        {
            using (var db = new LivingArtPMContext())
            {
                var query = (from unit in db.Units
                             where unit.Barcode == unitBarCode
                             select unit).FirstOrDefault();
                if (query.StatusID != 3 || query.StatusID != 4)
                {
                    query.StatusID = 3;
                }
                db.SaveChanges();
            }
        }
    }
}
