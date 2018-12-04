using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #14 فرمول مأموریت زیرگروه در سال و ماه </summary>
    public class MissionFormula : EntityBase
    {
        private Dictionary<ContractField, bool> _tempMissionFormulaInvolvedContractFieldsLeftJoined = null;

        public double DivideFactor { get; set; }

        public double AddFactor { get; set; }

        public double MaxFactor { get; set; }

        public double SubtractFactor { get; set; }

        public double PerKmFactor { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual SubGroup SubGroup { get; set; }

        public virtual List<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { get; set; }

        [NotMapped]
        public Dictionary<ContractField, bool> TempMissionFormulaInvolvedContractFieldsLeftJoined
        {
            get
            {
                if (_tempMissionFormulaInvolvedContractFieldsLeftJoined != null)
                    return _tempMissionFormulaInvolvedContractFieldsLeftJoined;
                var cfs = MissionFormulaInvolvedContractFields?.Select(p => p.ContractField).ToList();
                if (cfs == null) return new Dictionary<ContractField, bool>();
                _tempMissionFormulaInvolvedContractFieldsLeftJoined = new Dictionary<ContractField, bool>();
                cfs.ForEach(p => _tempMissionFormulaInvolvedContractFieldsLeftJoined.Add(p, true));
                SubGroup.MainGroup.CurrentContractFields.Except(cfs).ToList().ForEach(p =>
                    _tempMissionFormulaInvolvedContractFieldsLeftJoined.Add(p, false));
                return _tempMissionFormulaInvolvedContractFieldsLeftJoined;
            }
            set => _tempMissionFormulaInvolvedContractFieldsLeftJoined = value;
        }
    }
}