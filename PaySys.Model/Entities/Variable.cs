using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;
using PaySys.Model.Static;

namespace PaySys.Model.Entities
{
    /// <summary> #36 متغیرهای ماهانه در بازه زمانی </summary>
    public class Variable : EntityBase
    {
        public int Index { get; set; }
        public int FromYear { get; set; }

        public int FromMonth { get; set; }

        public int ToYear { get; set; }

        public int ToMonth { get; set; }

        public virtual MainGroup MainGroup { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public ValueType ValueType { get; set; }

        public virtual List<VariableValueForEmployee> VariableValueForEmployees { get; set; }

        public bool IncludesDate(int year, int month)
        {
            var fromDate = (FromYear * 100) + FromMonth;
            var toDate = (ToYear * 100) + ToMonth;
            var currentDate = (year * 100) + month;
            return currentDate >= fromDate && currentDate <= toDate;
        }

        [NotMapped]
        public bool IncludesCurrentDate => IncludesDate(PaySysSetting.CurrentYear, PaySysSetting.CurrentMonth);
    }
}