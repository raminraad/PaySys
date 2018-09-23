using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySys.ModelAndBindLib.Model
{
    public abstract class Validator : ValidationErrorContainer
    {
        public abstract void ValidateProperty(string propertyName);
    }
}