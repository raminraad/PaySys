﻿using System;

namespace PaySys.Model.Properties
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RazorDirectiveAttribute : Attribute
    {
        public RazorDirectiveAttribute([NotNull] string directive)
        {
            Directive = directive;
        }

        [NotNull] public string Directive { get; private set; }
    }
}