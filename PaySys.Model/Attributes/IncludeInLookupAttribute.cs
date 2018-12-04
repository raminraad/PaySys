using System;

namespace PaySys.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = true)]
    public class IncludeInLookupAttribute:Attribute
    {

    }
}
