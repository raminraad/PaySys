using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySys.ModelAndBindLib.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = true)]
    public class IncludeInLookupAttribute:Attribute
    {

    }
}
