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
    //[Table("T_USER", Schema = Config.DbSchema)]
    public partial class T_USER
    {
        [Key]
        public decimal USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_CODE { get; set; }
        public string USER_PASSWORD { get; set; }
        public decimal USER_STATUS_TI { get; set; }
        public Nullable<decimal> DELEGATE_USER_ID { get; set; }
        public decimal USER_IDX { get; set; }
        public decimal USER_TYPE_TI { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> TS { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<decimal> RS { get; set; }
    }
}