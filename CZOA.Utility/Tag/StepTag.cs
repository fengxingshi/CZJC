namespace CZOA.Tag
{
    public struct StepTag
    {
        /// <summary>
        /// 步骤授权
        /// </summary>
        public struct StepAuth
        {
            public const decimal _TagID = 113;
            /// <summary>
            /// 全部处室
            /// </summary>
            public const decimal AllDept = 1130;
            /// <summary>
            /// 指定处室
            /// </summary>
            public const decimal AuthDept = 1131;
            /// <summary>
            /// 当前处室
            /// </summary>
            public const decimal CurrDept = 1132;
            /// <summary>
            /// 全部角色
            /// </summary>
            public const decimal AllRole = 1133;
            /// <summary>
            /// 指定角色
            /// </summary>
            public const decimal AuthRole = 1134;
            /// <summary>
            /// 全部单位
            /// </summary>
            public const decimal AllOrg = 1135;
            /// <summary>
            /// 指定单位
            /// </summary>
            public const decimal AuthOrg = 1136;
        }
        /// <summary>
        ///     一个步骤可有多种类型
        /// </summary>
        public struct StepType
        {
            /// <summary>
            /// 类型标签ID
            /// </summary>
            public const decimal _TagID = 111;
            /// <summary>
            ///     入口步
            /// </summary>
            public const decimal EnterInStep = 1110;

            /// <summary>
            ///     加笺步
            /// </summary>
            public const decimal AddFormStep = 1111;

            ///// <summary>
            ///// 起流步(出口步)
            ///// </summary>
            //public const decimal StartFlowStep = 1112;

            /// <summary>
            ///     自接流程
            /// </summary>
            public const decimal SelfBeginFlowStep = 1112;

            /// <summary>
            ///     下步接流
            /// </summary>
            public const decimal NextBeginFlowStep = 1113;

            ///// <summary>
            ///// 发支步：发起分支
            ///// </summary>
            //public const decimal StartBranchStep = 1113;

            /// <summary>
            ///     发起协办（会签）
            /// </summary>
            public const decimal BeginAssistBranchStep = 1114;

            /// <summary>
            ///     发起并行
            /// </summary>
            public const decimal BeginParallelBranchStep = 1115;

            /// <summary>
            ///     分支步(并行步)：分支头
            /// </summary>
            public const decimal BranchLineHeaderStep = 1116;

            /// <summary>
            ///     合并步：并行合并
            /// </summary>
            public const decimal ParallelMergeStep = 1117;

            /// <summary>
            ///     手动完结步
            /// </summary>
            public const decimal ManualEndingStep = 1118;

            /// <summary>
            ///     自动完结步
            /// </summary>
            public const decimal AutoEndingStep = 1119;
        }
        /// <summary>
        /// 评价类型
        /// </summary>
        public struct CommentType
        {
            public const decimal _TagID = 112;
            public const decimal NoComment = 1120;
            public const decimal CommentOnPervNode = 1121;
            public const decimal CommentOnWork = 1121;
        }

        ///// <summary>
        ///// 完结类型
        ///// </summary>
        //public struct EndingType
        //{
        //    /// <summary>
        //    /// 手动完结
        //    /// </summary>
        //    public const decimal ManualEnding = 0;
        //    /// <summary>
        //    /// 自动完结
        //    /// </summary>
        //    public const decimal AutoEnding = 1;
        //}
        ///// <summary>
        ///// 接流程方式
        ///// </summary>
        //public struct StartFlowType
        //{
        //    /// <summary>
        //    /// 自己接流
        //    /// </summary>
        //    public const decimal BySelf = 0;
        //    /// <summary>
        //    /// 下步接流
        //    /// </summary>
        //    public const decimal ByNext = 1;
        //}
    }
}
