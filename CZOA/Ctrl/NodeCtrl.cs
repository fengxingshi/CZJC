using System;
using System.Collections.Generic;
using System.Data;
using CZOA.Ctrl.API;
using CZOA.DB;
using CZOA.Tag;
using NodeTags = CZOA.Tag.NodeTag;

namespace CZOA.Ctrl
{
    /// <summary>
    ///     节点控制器 —— 节点间的连接线动作
    /// </summary>
    internal class NodeCtrl : INodeCtrl
    {
        #region 节点相关

        public T_NODE CreateNode(decimal stepId, string stepName, decimal prevNodeId, decimal workId, decimal lineId,
            decimal stageId, decimal newNodeId, decimal waitingBranch, decimal lineHeader, decimal lineEnding,
            decimal arriveAction, decimal doStatus, decimal status)
        {
            var re = new T_NODE
            {
                STEP_ID = stepId,
                NODE_STEP_NAME = stepName,
                PREV_NODE_ID = prevNodeId,
                WORK_ID = workId,
                LINE_ID = lineId,
                STAGE_ID = stageId,
                NODE_ID = newNodeId,
                WAITING_BRANCH = waitingBranch,
                LINE_HEADER = lineHeader,
                LINE_ENDING = lineEnding,
                ARRIVE_ACTION = arriveAction,
                DO_STATUS = doStatus,
                STATUS = status
            };
            return re;
        }

        public T_NODE BuildTo(decimal stepId, string stepName, decimal newNodeId,
            decimal prevWorkAnyNodeId = 0)
        {
            var re = CreateNode(stepId, stepName, prevWorkAnyNodeId, newNodeId, newNodeId, newNodeId,
                newNodeId, NodeTags.WaitingBranch.NotWaiting, NodeTags.LineHeader.WorkHeader,
                NodeTags.LineEnding.NotEnding,
                NodeTags.ArriveAction.BuildHere, NodeTags.DoStatus.Doing, NodeTags.Status.Normal);
            return re;
        }

        public T_NODE MoveTo(decimal stepId, string stepName, decimal prevNodeId, decimal prevNodeWorkId,
            decimal prevNodeLineId,
            decimal prevNodeStageId, decimal newNodeId)
        {
            var re = CreateNode(stepId, stepName, prevNodeId, prevNodeWorkId, prevNodeLineId, prevNodeStageId, newNodeId,
                NodeTags.WaitingBranch.NotWaiting, NodeTags.LineHeader.NotHeader, NodeTags.LineEnding.NotEnding,
                NodeTags.ArriveAction.MoveHere, NodeTags.DoStatus.Waiting, NodeTags.Status.Normal);
            return re;
        }

        #region 取消：分段至此

        //public T_NODE SectionTo(decimal stepId, string stepName, decimal prevNodeId, decimal prevNodeWorkId,
        //    decimal prevNodeLineId,
        //    decimal newNodeId)
        //{
        //    var re = CreateNode(stepId, stepName, prevNodeId, prevNodeWorkId, prevNodeLineId, newNodeId, newNodeId,
        //        NodeTags.WaitingBranch.NotWaiting, NodeTags.LineHeader.NotHeader, NodeTags.LineEnding.False,
        //        NodeTags.ArriveAction.SectionHere, NodeTags.DoStatus.Doing, NodeTags.Status.Normal);
        //    return re;
        //}

        #endregion

        public T_NODE BranchTo(decimal stepId, string stepName, decimal prevNodeId, decimal prevNodeWorkId,
            decimal prevNodeStageId, decimal newNodeId, decimal lineHeader)
        {
            var re = CreateNode(stepId, stepName, prevNodeId, prevNodeWorkId, newNodeId, prevNodeStageId, newNodeId,
                NodeTags.WaitingBranch.NotWaiting, lineHeader, NodeTags.LineEnding.NotEnding,
                NodeTags.ArriveAction.BranchHere,
                NodeTags.DoStatus.Waiting, NodeTags.Status.Normal);
            return re;
        }

        public T_NODE ChangeTo(decimal changeTargetNodeStepId, string changeTargetNodeStepName, decimal prevNodeId,
            decimal prevNodeWorkId, decimal prevNodeLineId, decimal prevNodeStageId, decimal newNodeId)
        {
            var re = CreateNode(changeTargetNodeStepId, changeTargetNodeStepName, prevNodeId, prevNodeWorkId,
                prevNodeLineId, prevNodeStageId, newNodeId,
                NodeTags.WaitingBranch.NotWaiting, NodeTags.LineHeader.NotHeader, NodeTags.LineEnding.NotEnding,
                NodeTags.ArriveAction.ChangeHere, NodeTags.DoStatus.Waiting, NodeTags.Status.Normal);
            return re;
        }

        public T_NODE RevokeTo(decimal rvkNodeStepId, string rvkNodeStepName, decimal rvkNodeId, decimal rvkNodeWorkId,
            decimal rvkNodeLineId,
            decimal rvkNodeStageId, decimal newNodeId)
        {
            var re = CreateNode(rvkNodeStepId, rvkNodeStepName, rvkNodeId, rvkNodeWorkId, rvkNodeLineId, rvkNodeStageId,
                newNodeId,
                NodeTags.WaitingBranch.NotWaiting, NodeTags.LineHeader.NotHeader, NodeTags.LineEnding.NotEnding,
                NodeTags.ArriveAction.RevokeHere, NodeTags.DoStatus.Waiting, NodeTags.Status.Normal);
            return re;
        }

        public T_NODE GoBackTo(decimal targetNodeStepId, string targetNodeStepName, decimal gobkNodeId,
            decimal gobkNodeWorkId,
            decimal gobkNodeLineId, decimal targetNodeStageId, decimal newNodeId)
        {
            var re = CreateNode(targetNodeStepId, targetNodeStepName, gobkNodeId, gobkNodeWorkId, gobkNodeLineId,
                targetNodeStageId, newNodeId,
                NodeTags.WaitingBranch.NotWaiting, NodeTags.LineHeader.NotHeader, NodeTags.LineEnding.NotEnding,
                NodeTags.ArriveAction.GoBackHere, NodeTags.DoStatus.Waiting, NodeTags.Status.Normal);
            return re;
        }

        public T_NODE ChangeDoneTo(decimal changeFromNodeStepId, string changeFromNodeStepName, decimal prevNodeId,
            decimal changeFromNodeWorkId, decimal changeFromNodeLineId, decimal changeFromNodeStageId, decimal newNodeId)
        {
            var re = CreateNode(changeFromNodeStepId, changeFromNodeStepName, prevNodeId, changeFromNodeWorkId,
                changeFromNodeLineId, changeFromNodeStageId, newNodeId, NodeTags.WaitingBranch.NotWaiting,
                NodeTags.LineHeader.NotHeader, NodeTags.LineEnding.NotEnding, NodeTags.ArriveAction.ChangeDoneHere,
                NodeTags.DoStatus.Waiting, NodeTags.Status.Normal);

            return re;
        }

        public T_NODE ChangeDoneTo(T_NODE changeFromNode, decimal prevNodeId, decimal newNodeId)
        {
            var re = ChangeDoneTo(changeFromNode.STEP_ID, changeFromNode.NODE_STEP_NAME, prevNodeId,
                changeFromNode.WORK_ID, changeFromNode.LINE_ID, changeFromNode.STAGE_ID, newNodeId);
            return re;
        }

        public void BranchFrom(T_NODE node, decimal waitingBranch)
        {
            node.WAITING_BRANCH = waitingBranch;
        }

        #endregion

        #region 节点入库

        public decimal SaveNode(T_NODE node)
        {
            using (var ctx = new OAContext())
            {
                if (node.RS == null) //行状态=null 标示为库中没有的新数据
                {
                    ctx.T_NODE.Add(node); //添加
                }
                else
                {
                    ctx.T_NODE.Attach(node); //附加
                    ctx.Entry<T_NODE>(node).State = EntityState.Modified; //改状态为已修改
                }
                return ctx.SaveChanges();
            }
        }
        #endregion

        #region 节点域（节点授权，stepToUser设置）

        public T_NODE_DOMAIN CreateNodeDomain(decimal nodeId, decimal deptId, decimal userId,
            decimal? delegateUserId = null)
        {
            var domain = new T_NODE_DOMAIN
            {
                USER_ID = userId,
                DEPT_ID = deptId,
                NODE_ID = nodeId,
                NODE_DOMAIN_ID = this.BuildID(),
                ACTIVE_TI = NodeDomainTag.Active.True,
                DELEGATE_USER_ID = delegateUserId
            };
            return domain;
        }

        #endregion

        #region 节点域入库

        public decimal SaveNodeDomain(T_NODE_DOMAIN nodeDomain)
        {
            using (var ctx = new OAContext())
            {
                if (nodeDomain.RS == null)
                {
                    ctx.T_NODE_DOMAIN.Add(nodeDomain);
                }
                else
                {
                    ctx.T_NODE_DOMAIN.Attach(nodeDomain);
                    ctx.Entry<T_NODE_DOMAIN>(nodeDomain).State = EntityState.Modified;
                }
                return ctx.SaveChanges();
            }
        }
        #endregion
    }
}
