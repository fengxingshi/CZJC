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
    //[Table("T_MODEL_ITEM", Schema = Config.DbSchema)]
    public partial class T_MODEL_ITEM
    {
        public decimal MODEL_ID { get; set; }
        [Key]
        public decimal MODEL_ITEM_ID { get; set; }
        public Nullable<decimal> SUPER_MODEL_ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal ITEM_VALUE_TYPE_TI { get; set; }
        public decimal ITEM_IDX { get; set; }
        public decimal VALUE_NULLABLE { get; set; }
        public string DEFAULT_VALUE { get; set; }
        public string OPTION_VALUE { get; set; }
        public decimal FILL_MODE_TI { get; set; }
        public decimal IS_COPY_ITEM { get; set; }
        public decimal DISPLAY_STYLE_TI { get; set; }
        public decimal OUTER_DEPT_EDITABLE { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> TS { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<decimal> RS { get; set; }
    }
}
