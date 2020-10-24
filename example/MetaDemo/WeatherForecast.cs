using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MetaDemo
{
    [MetaModel]
    public class WeatherForecast
    {
        [Display(Name = "日期")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 表示摄氏温度
        /// </summary>
        [DisplayName("摄氏温度")]
        [Description("表示摄氏温度（℃）")]
        [DataUnit("℃")]
        [Range(-20, 50)]
        public int TemperatureC { get; set; }

        [DisplayName("华氏温度")]
        [Description("表示华氏温度（℉）")]
        [DataUnit("℉")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);


        [Display(Description = "摘要信息")]
        public string Summary { get; set; }
    }
}
