using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #05 پرداختهای متفرقه دخیل در محاسبات مؤلفه ها </summary>
    public class ParameterInvolvedMisc : EntityBase
    {
        public virtual Misc Misc { get; set; }

        public virtual Parameter Parameter { get; set; }
    }
}