using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZOA.Tag
{
    public struct EntityTag
    {
        /// <summary>
        /// 使用方式（保存方式）
        /// </summary>
        public struct Active
        {
            /// <summary>
            /// 正式
            /// </summary>
            public const decimal DoneSave = 1;
            /// <summary>
            /// 暂存
            /// </summary>
            public const decimal QuickSave = 0;
        }
    }
    public struct EntityItemTag
    {
        /// <summary>
        /// 使用方式（保存方式）
        /// </summary>
        public struct Active
        {
            /// <summary>
            /// 正式
            /// </summary>
            public const decimal DoneSave = 1;
            /// <summary>
            /// 暂存
            /// </summary>
            public const decimal QuickSave = 0;
        }
    }
}
