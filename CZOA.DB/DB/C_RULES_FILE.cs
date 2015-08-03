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
    public partial class C_RULES_FILE
    {
        [Key]
        public decimal FILE_ID { get; set; }
        public decimal FILE_TYPE_ID { get; set; }
        public string FILE_TITLE { get; set; }
        public string TITLE_PINYIN { get; set; }
        public string FILE_CODE { get; set; }
        public string FILE_PATH { get; set; }
        public decimal ACCESSSCOPE { get; set; }
        public decimal FILE_STATE { get; set; }
        public decimal FILE_LEVEL { get; set; }
        public decimal FILE_IDX { get; set; }
        public decimal SOURCE_DEPT { get; set; }
        public decimal EDITOR { get; set; }
        public DateTime EDIT_DATE { get; set; }
        public decimal AUDITOR { get; set; }
        public DateTime AUDIT_DATE { get; set; }
        public decimal APPROVAL_USER { get; set; }
        public DateTime APPROVAL_DATE { get; set; }
        public decimal RELEASE_USER { get; set; }
        public DateTime RELEASE_DATE { get; set; }
        public decimal RS { get; set; }    
        public Nullable<System.DateTime> TS { get; set; }
        public decimal DATA_STATE { get; set; }

        public decimal SOURCE_ORG { get; set; }
    }
}