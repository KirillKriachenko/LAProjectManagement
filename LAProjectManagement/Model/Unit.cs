//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LAProjectManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Unit
    {
        public int UnitID { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string DepartureDate { get; set; }
        public int ProjectProjectID { get; set; }
        public string PartsAmount { get; set; }
        public string PartsScanned { get; set; }
        public int StatusID { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
