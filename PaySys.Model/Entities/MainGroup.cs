#region
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.Model.Attributes;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;
using PaySys.Model.Static;
#endregion

namespace PaySys.Model.Entities
{
    /// <summary> #01 گروه اصلی </summary>
    public class MainGroup : EntityBase
    {
        [IncludeInLookup]
        public string Title { get; set; }

        public string Alias { get; set; }

        public ColorPallet ItemColor { get; set; }

        public virtual List<SubGroup> SubGroups { get; set; }

        public virtual List<ContractField> ContractFields { get; set; }

        public virtual List<Variable> Variables { get; set; }

        [NotMapped]
        public IEnumerable<ContractField> CurrentContractFields =>
            ContractFields.Where(c => c.Year == PaySysSetting.CurrentYear);

        [NotMapped]
        public IEnumerable<Variable> CurrentVariables => Variables.Where(v => v.IncludesCurrentDate);

        public override bool Equals(object obj)
        {
            return string.Equals((obj as MainGroup)?.Alias, Alias);
        }
    }
}