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
using System.Data;

namespace CZOA.DB
{
    using System;
    using System.Collections.Generic;
    //[Table("T_ATTA", Schema = Config.DbSchema)]
    public partial class C_FILE_TYPE
    {
        [Key]
        public decimal TYPE_ID { get; set; }
        
        public string TYPE_NAME { get; set; }
        public string TYPE_CODE { get; set; }
        public string NOTE { get; set; }
        public string ENABLED { get; set; }
        public decimal IDX { get; set; }
        public Nullable<System.DateTime> TS { get; set; }
    }
}
