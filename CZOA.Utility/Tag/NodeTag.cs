namespace CZOA.Tag
{
    public struct NodeTag
    {
        /// <summary>
        /// 标记节点等待分支线路完结返回或等待分支完结合并于下步。
        /// 0：不等待，1：等待返回合并，-1：等待返回合并过，2：等待汇聚合并，-2：等待汇聚合并过
        /// </summary>
        public struct WaitingBranch
        {
            /// <summary>
            /// 0:不等待任何分支线路
            /// </summary>
            public const decimal NotWaiting = 0;
            /// <summary>
            /// 1:等待分支返回
            /// </summary>
            public const decimal Waiting_Return = 1;
            /// <summary>
            /// -1:等待分支返回->分支已返回
            /// </summary>
            public const decimal Waiting_Return_Completed = -1;
            /// <summary>
            /// 2:等待分支合并
            /// </summary>
            public const decimal Waiting_Merge = 2;
            /// <summary>
            /// -2:等待分支合并->分支已合并
            /// </summary>
            public const decimal Waiting_Merge_Completed = -2;
        }
        /// <summary>
        /// 节点与线路开头相关的标记
        /// 0:不是开启线路的节点
        /// 1:开启线路的节点->且有节点在等待此线路完结
        /// -1:开启线路的节点->有节点在等待此线路完结->此线路已经完结
        /// 2:开启线路的节点->没有节点在等待此线路完结
        /// -2:开启线路的节点->没有节点在等待此线路完结->此线路已经完结
        /// 9:事项开头
        /// -9:事项开头->此事项已经完结
        /// </summary>
        public struct LineHeader
        {
            /// <summary>
            /// 0:不是开启线路的节点
            /// </summary>
            public const decimal NotHeader = 0;
            /// <summary>
            /// 1:开启线路的节点->有节点在等待此线路完结
            /// </summary>
            public const decimal Header_BeWaiting = 1;
            /// <summary>
            /// -1:开启线路的节点->有节点在等待此线路完结->此线路已经完结
            /// </summary>
            public const decimal Header_BeWaiting_LineComplete = -1;
            /// <summary>
            /// 2:开启线路的节点->没有节点在等待此线路完结
            /// </summary>
            public const decimal Header_NotBeWaiting = 2;
            /// <summary>
            /// -2:开启线路的节点->没有节点在等待此线路完结->此线路已经完结
            /// </summary>
            public const decimal Header_NotBeWaiting_LineComplete = -2;
            /// <summary>
            /// 9:事项开头
            /// </summary>
            public const decimal WorkHeader = 9;
            /// <summary>
            /// -9:事项开头->此事项已经完结
            /// </summary>
            public const decimal WorkHeader_WorkComplete = -9;
        }
        /// <summary>
        /// 标记线路结束点
        /// 0：非线路结束点，1：线路结束点_手动，2：线路结束点_自动
        /// </summary>
        public struct LineEnding
        {
            /// <summary>
            /// 0:非线路结束点
            /// </summary>
            public const decimal NotEnding = 0;
            /// <summary>
            /// 1:线路结束点，手动完结
            /// </summary>
            public const decimal Ending_Manual = 1;
            /// <summary>
            /// 2:线路结束点，自动完结
            /// </summary>
            public const decimal Ending_Auto = 2;
        }
        /// <summary>
        /// 标记上步骤抵达当前步骤的方式（节点间连接线的动作类型）
        /// </summary>
        public struct ArriveAction
        {
            /// <summary>
            /// 0:创建于此
            /// </summary>
            public const decimal BuildHere = 0;
            /// <summary>
            /// 1:流转至此 
            /// </summary>
            public const decimal MoveHere = 1;
            /// <summary>
            /// 2:分段（接新流程）至此
            /// </summary>
            public const decimal SectionHere = 2;
            /// <summary>
            /// 3:分支（会签）至此
            /// </summary>
            public const decimal BranchHere = 3;
            /// <summary>
            /// 6:改签至此
            /// </summary>
            public const decimal ChangeHere = 6;
            /// <summary>
            /// 7:改签完成
            /// </summary>
            public const decimal ChangeDoneHere = 7;
            /// <summary>
            /// 8:撤销至此
            /// </summary>
            public const decimal RevokeHere = 8;
            /// <summary>
            /// 9:回退至此
            /// </summary>
            public const decimal GoBackHere = 9;
            /// <summary>
            /// 10:完结于此
            /// </summary>
            public const decimal FinishHere = 10;
        }
        /// <summary>
        /// 标记节点的办理状态。
        /// 0：待办理，1：办理中，-1：已办结
        /// </summary>
        public struct DoStatus
        {
            /// <summary>
            /// 0:待办理
            /// </summary>
            public const decimal Waiting = 0;
            /// <summary>
            /// 1:办理中
            /// </summary>
            public const decimal Doing = 1;
            /// <summary>
            /// -1:已办结
            /// </summary>
            public const decimal Complete = -1;
        }
        /// <summary>
        /// 节点状态
        /// 1:正常，0:撤销
        /// </summary>
        public struct Status
        {
            /// <summary>
            /// 1 正常状态：流转所用
            /// </summary>
            public const decimal Normal = 1;
            /// <summary>
            /// 0 撤销状态：被撤销的节点所用，撤销不等于删除(rs=0)
            /// </summary>
            public const decimal Revoke = 0;
        }
    }

    public struct NodeDomainTag
    {
        /// <summary>
        /// 是否激活的域
        /// </summary>
        public struct Active
        {
            public const decimal True = 1;
            public const decimal False = 0;
        }
    }
}