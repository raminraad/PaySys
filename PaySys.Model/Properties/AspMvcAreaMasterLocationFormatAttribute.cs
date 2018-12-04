using System;

namespace PaySys.Model.Properties
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
    {
        public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }

        [NotNull] public string Format { get; private set; }
    }
}