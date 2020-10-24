using System;
using System.Collections.Generic;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MetaModelAttribute : Attribute
    {
        public MetaModelAttribute(string metaName = null)
        {
            this.MetaName = metaName;
        }

        public string MetaName { get; }

        public static string GetMetaName(Type type)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));
            var metaModelAttr = Attribute.GetCustomAttribute(type, typeof(MetaModelAttribute)) as MetaModelAttribute;
            return string.IsNullOrEmpty(metaModelAttr?.MetaName) ? type.FullName : metaModelAttr.MetaName;
        }
    }
}
