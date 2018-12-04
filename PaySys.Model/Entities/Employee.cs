using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.Model.Attributes;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;
using PaySys.Model.ExtensionMethods;
using PaySys.Model.Static;

namespace PaySys.Model.Entities
{
    /// <summary> #22 اشخاص </summary>
    public class Employee : EntityBase
    {
        [IncludeInLookup]
        public string FName { get; set; }

        [IncludeInLookup]
        public string LName { get; set; }

        [IncludeInLookup]
        public string NationalCardNo { set; get; }

        [IncludeInLookup]
        public DateTime BirthDate { get; set; }

        [IncludeInLookup]
        public string BirthPlace { get; set; }

        [IncludeInLookup]
        public string FatherName { get; set; }

        [IncludeInLookup]
        public string HomeTel { get; set; }

        [IncludeInLookup]
        public string Address { get; set; }

        [IncludeInLookup]
        public string DossierNo { get; set; }

        [IncludeInLookup]
        public Sex Sex { set; get; }

        [IncludeInLookup]
        public string CellNo { get; set; }

        [IncludeInLookup]
        public string PostalCode { get; set; }

        [IncludeInLookup]
        public string PersonnelCode { get; set; }

        [IncludeInLookup]
        public string IdCardExportPlace { get; set; }

        [IncludeInLookup]
        public DateTime IdCardExportDate { set; get; }

        [IncludeInLookup]
        public string IdCardNo { get; set; }
        
        public byte[] PhotoStream { set; get; }

        [NotMapped]
        public System.Drawing.Image Photo
        {
            set => PhotoStream = value.ToByteArray();
            get => PhotoStream.ToImage();
        }

        [NotMapped]
        public List<MiscValueForEmployee> CurrentMiscValues => MiscValueForEmployees.Where(valForEmp =>
                valForEmp.Misc.Year == PaySysSetting.CurrentYear && valForEmp.Misc.Month == PaySysSetting.CurrentMonth)
            .ToList();

        public virtual List<ContractMaster> ContractMasters { get; set; }

        public virtual List<MiscRecharge> MiscRecharges { get; set; }

        public virtual List<MiscValueForEmployee> MiscValueForEmployees { get; set; }

        public virtual List<VariableValueForEmployee> VariableValueForEmployees { get; set; }
        public virtual List<PayslipItemValueForEmployee> PayslipItemValueForEmployees { get; set; }
        public virtual List<PayslipValueOfMiscForEmployee> PayslipValueOfMiscForEmployees { get; set; }

        [NotMapped]
        public IEnumerable<Mission> MissionsOfCurrentYear =>
            ContractMasters.SelectMany(c => c.Missions.Where(m =>
                m.StartDate >= PaySysSetting.CurrentYearStartGreg && m.StartDate <= PaySysSetting.CurrentYearEndGreg));

        [IncludeInLookup]
        [NotMapped]
        public string FullName => $"{Id} : {FName} {LName}"; //Todo: Remove EmployeeId from result

        [IncludeInLookup]
        [NotMapped]
        public string LuffName => $"{LName} {FName}";

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != GetType())
                return false;

            return ((Employee) obj).Id == Id;
        }
    }
}