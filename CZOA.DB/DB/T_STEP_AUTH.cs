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
    //[Table("T_STEP_AUTH", Schema = Config.DbSchema)]
    public partial class T_STEP_AUTH
    {
        public decimal STEP_ID { get; set; }
        [Key]
        public decimal STEP_AUTH_ID { get; set; }
        public decimal AUTH_SCOPE_TI { get; set; }
        public decimal? AUTH_TO { get; set; }
        //public decimal IS_REVERSE_AUTH { get; set; }
        //public decimal AND_OR { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> TS { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<decimal> RS { get; set; }
    }
}