using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZOA.Utility.Tag
{
    public partial struct FileTag
    {
        /// <summary>
        /// 文件范围
        /// </summary>
        public struct FileScope
        {
            public const decimal _TagID = 2000010;
            public const decimal 系统 = 200001001;
            public const decimal 厅局 = 200001002;
            public const decimal 处室 = 200001003;
        }

        /// <summary>
        /// 文件等级
        /// </summary>
        public struct FileGrade
        {
            public const decimal _TagID = 2000012;
            public const decimal A = 200001201;
            public const decimal B = 200001202;
            public const decimal C = 200001203;
        }

        /// <summary>
        /// 文件状态
        /// </summary>
        public struct FileState
        {
            public const decimal _TagID = 2000011;
            public const decimal 正常 = 200001101;
            public const decimal 过期 = 200001102;
            public const decimal 作废 = 200001103;
        }
    }
}
