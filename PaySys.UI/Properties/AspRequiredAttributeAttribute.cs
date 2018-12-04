using System;

namespace PaySys.UI.Properties
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AspRequiredAttributeAttribute : Attribute
    {
        public AspRequiredAttributeAttribute([NotNull] string attribute)
        {
            Attribute = attribute;
        }

        [NotNull] public string Attribute { get; private set; }
    }
}