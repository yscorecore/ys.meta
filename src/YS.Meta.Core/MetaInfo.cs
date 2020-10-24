using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YS.Meta
{
    public class MetaInfo
    {
        public string Name { get; set; }

        public List<PropInfo> Properties { get; set; }
        
        public string DisplayName { get; set; }

        public string Description { get; set; }
    }

    public class PropInfo
    {
        public string Name { get; set; }
        /// <summary>
        /// 表示存储字段的类型 string,int,double,float,bool,enum,object,datetime
        /// </summary>
        public string FieldTypeCode { get; set; }

        public string DisplayName { get; set; }

        public string ShortDisplayName { get; set; }

        public string Description { get; set; }

        public string DataUnit { get; set; }

        public bool IsKey { get; set; }

        public bool IsName { get; set; }

        public bool ReadOnly { get; set; }

        //public List<PropInfo> Properties { get; set; }

    }
}
