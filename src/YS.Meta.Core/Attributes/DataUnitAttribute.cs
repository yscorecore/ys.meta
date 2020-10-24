using System;
using System.Collections.Generic;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DataUnitAttribute : Attribute
    {
        public string Unit { get; }
        public DataUnitAttribute(string unit)
        {
            this.Unit = unit;
        }
    }
}
