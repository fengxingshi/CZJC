using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface INodeCtrl
    {
        #region 节点相关

        //-----节点间的连接线动作-----//
        /// <summary>
        ///     生成节点
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="stepName"></param>
        /// <param name="prevNodeId"></param>
        /// <param name="workId"></param>
        /// <param name="lineId"></param>
        /// <param name="stageId"></param>
        /// <param name="newNodeId"></param>
        /// <param name="waitingBranch"></param>
        /// <param name="lineHeader"></param>
        /// <param name="lineEnding"></param>
        /// <param name="arriveAction"></param>
        /// <param name="doStatus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        T_NODE CreateNode(decimal stepId, string stepName, decimal prevNodeId, decimal workId, decimal lineId,
            decimal stageId,
            decimal newNodeId, decimal waitingBranch, decimal lineHeader, decimal lineEnding, decimal arriveAction,
            decimal doStatus,
            decimal status);

        /// <summary>
        ///     创建于此：事项的开启节点
        ///     ---------
        ///     WorkId=LineId=StageId={nodeId}
        ///     WaitingBranch=0
        ///     LineHeader=9
        ///     LineEnding=0
        ///     ArriveAction=0
        ///     Status=1
        ///     DoStatus=1：完成后置-1
        /// </summary>
        /// <param name="newNodeId">新节点Id</param>
        /// <param name="stepId">步骤Id</param>
        /// <param name="stepName">步骤名</param>
        /// <param name="prevNodeIdOfContinueWork">前节点：{默认：0，续接事项：上事项任意节点}</param>
        /// <returns></returns>
        T_NODE BuildTo(decimal stepId, string stepName, decimal newNodeId,
            decimal prevNodeIdOfContinueWork = 0);

        /// <summary>
        ///     流转至此
        ///     ---------
        ///     WaitingBranch=0
        ///     LineHeader=0
        ///     LineEnding=0
        ///     ArriveAction=1
        ///     DoStatus=0
        ///     Status=1
        /// </summary>
        /// <param name="prevNodeId">前节点Id</param>
        /// <param name="prevNodeWorkId">前节点事项Id</param>
        /// <param name="prevNodeLineId">前节点线路Id</param>
        /// <param name="prevNodeStageId">前节点阶段Id</param>
        /// <param name="newNodeId">新节点Id</param>
        /// <param name="stepId">步骤Id</param>
        /// <param name="stepName">步骤名</param>
        /// <returns></returns>
        T_NODE MoveTo(decimal stepId, string stepName, decimal prevNodeId, decimal prevNodeWorkId,
            decimal prevNodeLineId,
            decimal prevNodeStageId, decimal newNodeId);

        #region 取消：分段至此

        ///// <summary>
        /////     分段至此：开启了新流程
        /////     ---------
        /////     StageId={nodeId}
        /////     WaitingBranch=0
        /////     LineHeader=0
        /////     LineEnding=0
        /////     ArriveAction=2
        /////     Status=1
        /////     DoStatus=1：完成后置-1
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <param name="stepName"></param>
        ///// <param name="prevNodeId"></param>
        ///// <param name="prevNodeWorkId"></param>
        ///// <param name="prevNodeLineId"></param>
        ///// <param name="newNodeId"></param>
        ///// <returns></returns>
        //T_NODE SectionTo(decimal stepId, string stepName, decimal prevNodeId, decimal prevNodeWorkId,
        //    decimal prevNodeLineId,
        //    decimal newNodeId);

        #endregion

        /// <summary>
        ///     分支至此：创建分支开启节点（一个个建)
        ///     是否操作开启分支的节点状态? -> 开启分支由其它方法负责，此方法在开启分支中被调用创建分支节点
        ///     ---------
        ///     LineId={nodeId}
        ///     WaitingBranch=0
        ///     LineEnding=0
        ///     ArriveAction=3
        ///     DoStatus=0
        ///     Status=1
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="stepName"></param>
        /// <param name="prevNodeId"></param>
        /// <param name="prevNodeWorkId"></param>
        /// <param name="prevNodeStageId"></param>
        /// <param name="newNodeId"></param>
        /// <param name="lineHeader">线路开启方式：{0：不需合并，1：需合并}</param>
        /// <returns></returns>
        T_NODE BranchTo(decimal stepId, string stepName, decimal prevNodeId, decimal prevNodeWorkId,
            decimal prevNodeStageId,
            decimal newNodeId, decimal lineHeader);

        /// <summary>
        ///     改签至此：修订、补签
        ///     ---------
        ///     WaitingBranch=0
        ///     LineHeader=0
        ///     LineEnding=0
        ///     ArriveAction=6：通过此字段判断是否为修订、补签节点
        ///     DoStatus=0
        ///     Status=1
        /// </summary>
        /// <param name="changeTargetNodeStepId">改签目标节点的步骤ID</param>
        /// <param name="changeTargetNodeStepName">改签目标节点的步骤名称</param>
        /// <param name="prevNodeId"></param>
        /// <param name="prevNodeWorkId"></param>
        /// <param name="prevNodeLineId"></param>
        /// <param name="prevNodeStageId"></param>
        /// <param name="newNodeId"></param>
        /// <returns></returns>
        T_NODE ChangeTo(decimal changeTargetNodeStepId, string changeTargetNodeStepName, decimal prevNodeId,
            decimal prevNodeWorkId, decimal prevNodeLineId,
            decimal prevNodeStageId, decimal newNodeId);

        /// <summary>
        ///     撤销至此：输入内容需撤销
        ///     ---------
        ///     WaitingBranch=0
        ///     LineHeader=0
        ///     LineEnding=0
        ///     ArriveAction=8
        ///     DoStatus=0
        ///     Status=1
        /// </summary>
        /// <param name="rvkNodeStepId">发起撤销节点的步骤ID</param>
        /// <param name="rvkNodeStepName">发起撤销节点的步骤名</param>
        /// <param name="rvkNodeId">发起撤销节点</param>
        /// <param name="rvkNodeWorkId"></param>
        /// <param name="rvkNodeLineId"></param>
        /// <param name="rvkNodeStageId"></param>
        /// <param name="newNodeId"></param>
        /// <returns></returns>
        T_NODE RevokeTo(decimal rvkNodeStepId, string rvkNodeStepName, decimal rvkNodeId, decimal rvkNodeWorkId,
            decimal rvkNodeLineId, decimal rvkNodeStageId, decimal newNodeId);

        /// <summary>
        ///     回退支持：只能同线路内回退，输入内容不需撤销
        ///     ---------
        ///     WaitingBranch=0
        ///     LineHeader=0
        ///     LineEnding=0
        ///     ArriveAction=9
        ///     DoStatus=0
        ///     Status=1
        /// </summary>
        /// <param name="targetNodeStepId"></param>
        /// <param name="targetNodeStepName"></param>
        /// <param name="gobkNodeId"></param>
        /// <param name="gobkNodeWorkId"></param>
        /// <param name="gobkNodeLineId"></param>
        /// <param name="targetNodeStageId"></param>
        /// <param name="newNodeId"></param>
        /// <returns></returns>
        T_NODE GoBackTo(decimal targetNodeStepId, string targetNodeStepName, decimal gobkNodeId, decimal gobkNodeWorkId,
            decimal gobkNodeLineId,
            decimal targetNodeStageId, decimal newNodeId);

        /// <summary>
        ///     改签完成： 发起改签节点{lineEnding->0} -> 改签节点 -> 改签完返回的节点（新节点）
        ///     ---------
        ///     WaitingBranch=0
        ///     LineHeader=0
        ///     LineEnding=0
        ///     ArriveAction=7
        ///     Status=1
        ///     DoStatus=0
        /// </summary>
        /// <param name="changeFromNodeStepId">发起改签的节点的步骤ID</param>
        /// <param name="changeFromNodeStepName">发起改签的节点的步骤名称</param>
        /// <param name="prevNodeId">前节点ID（改签(修订、补签)节点ID）</param>
        /// <param name="changeFromNodeWorkId">发起改签的节点的事项ID</param>
        /// <param name="changeFromNodeLineId">发起改签的节点的线路ID</param>
        /// <param name="changeFromNodeStageId">发起改签的节点的阶段ID</param>
        /// <param name="newNodeId">新节点ID</param>
        /// <returns>返回该前完成返回的节点（根据发起节点新生成）</returns>
        T_NODE ChangeDoneTo(decimal changeFromNodeStepId, string changeFromNodeStepName, decimal prevNodeId,
            decimal changeFromNodeWorkId, decimal changeFromNodeLineId, decimal changeFromNodeStageId, decimal newNodeId);

        /// <summary>
        ///     改签完成
        /// </summary>
        /// <param name="changeFromNode">发起改签的节点</param>
        /// <param name="prevNodeId">前节点ID（改签(修订、补签)节点ID）</param>
        /// <param name="newNodeId">新节点ID</param>
        /// <returns>返回改签完成节点</returns>
        T_NODE ChangeDoneTo(T_NODE changeFromNode, decimal prevNodeId, decimal newNodeId);

        //T_NODE AutoFinishTo();

        /// <summary>
        ///     分支于此
        ///     ---------
        ///     从此节点根据分支方式开启分支
        /// </summary>
        /// <param name="node">开启分支的宿主节点</param>
        /// <param name="waitingBranch">1：需合并，2：无需合并</param>
        void BranchFrom(T_NODE node, decimal waitingBranch);

        #endregion

        #region 节点入库

        /// <summary>
        ///     保存节点（新则添加，改则更新）
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        decimal SaveNode(T_NODE node);

        #endregion

        #region 节点域相关

        /// <summary>
        ///     创建节点的节点域
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="deptId"></param>
        /// <param name="userId"></param>
        /// <param name="delegateUserId"></param>
        /// <returns></returns>
        T_NODE_DOMAIN CreateNodeDomain(decimal nodeId, decimal deptId, decimal userId,
            decimal? delegateUserId = null);

        #endregion

        #region 节点域入库

        /// <summary>
        ///     保存节点域入库（新则添加，改则更新）
        /// </summary>
        /// <param name="nodeDomain"></param>
        /// <returns></returns>
        decimal SaveNodeDomain(T_NODE_DOMAIN nodeDomain);

        #endregion
    }
}
