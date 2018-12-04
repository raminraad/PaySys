using System.Collections.Generic;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary>
    /// #38 عناوین مؤلفه های گروهی
    /// </summary>
    public class ParameterTitle : EntityBase
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public virtual List<ParameterTitle> ParameterTitles { get; set; }
    }
}