//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace CZOA.DB
{
    using System;
    using System.Collections.Generic;
    //[Table("T_WORK_TAG", Schema = Config.DbSchema)]
    public partial class T_WORK_TAG
    {
        public decimal WORK_ID { get; set; }
        [Key]
        public decimal WORK_TAG_ID { get; set; }
        public decimal TAG_ID { get; set; }
        public decimal TAG_ITEM_ID { get; set; }
        public string TAG_ITEM_VALUE { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> TS { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<decimal> RS { get; set; }
    }
}