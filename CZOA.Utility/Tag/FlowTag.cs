using System.Collections.Generic;

namespace CZOA.Tag
{
    public partial struct FlowTag
    {
        public struct FlowAuth
        {
            /// <summary>
            /// 全部单位
            /// </summary>
            public const decimal AllOrg = 1020;
            /// <summary>
            /// 指定单位
            /// </summary>
            public const decimal AuthOrg = 1021;
            /// <summary>
            /// 全部处室
            /// </summary>
            public const decimal AllDept = 1022;
            /// <summary>
            /// 指定处室
            /// </summary>
            public const decimal AuthDept = 1023;
            /// <summary>
            /// 全部角色
            /// </summary>
            public const decimal AllRole = 1024;
            /// <summary>
            /// 指定角色
            /// </summary>
            public const decimal AuthRole = 1025;
        }
        /// <summary>
        /// 外部流转流程（市县发文之类）
        /// </summary>
        public struct OuterOrgFlow
        {
            public const decimal True = 1;
            public const decimal False = 0;
        }
        /// <summary>
        /// 流程启用状态
        /// </summary>
        public struct Enabled
        {
            public const decimal True = 1;
            public const decimal False = 0;
        }
        /// <summary>
        /// 流程分类
        /// ==动态==
        /// </summary>
        public struct FlowCategory
        {
            public const decimal _TagID = 101;
        }
    }
}
