using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySys.ModelAndBindLib.Model
{
    public class Validator : ValidationErrorContainer
    {
        public virtual void ValidateProperty(string propertyName)
        {
        }
    }
}