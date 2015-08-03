using System;
using System.Transactions;
using CZOA.DB;
using CZOA.Tag;
using SN.Utility;

namespace CZOA.Ctrl
{
    internal class DoAction //:IDoAction
    {
        public ActionModel BeginWork(SNSession session, T_FLOW beginFlow, T_STEP beginStep, decimal prevWorkAnyNodeId = 0)
        {
            #region 创建事项流程简述

            //TODO:自动生成
            // 1.确定事项的入口：确定入口步骤 done
            // 2.1.根据入口步骤生曾第一节点(即产生了主线路) done
            // 2.2.根据第一节点产生事项 done
            // 2.3.生成节点授权信息(steptouser) done
            // 3.根据入口步骤所属的流程，生成相关阶段 done
            // 4.1.创建表单--根据步骤ID获取要默认创建的文笺列表 done
            // 4.2.默认打开排序号第一的表单 todo
            // 4.3.获取可输入项--根据文笺ID获取当前步骤文笺的输入项列表 done
            //-----------------
            //TODO:输入数据
            // 1.1.输入事项相关属性，生成事项标签
            // 2.1.输入项输入值
            // 3.1.设置表单属性（序号、是否带纸质等）
            //-----------------
            //TODO:完成，保存数据
            // 1.1.保存节点
            // 1.2.保存节点授权
            // 2.1.保存表单
            // 2.2.保存输入项
            // 3.1.保存阶段
            // 4.1.保存事项
            //-----------------
            //TODO:保存下步信息（事项第一步不可接新流程）
            // 1.1.产生下步节点
            // 1.2.产生下步授权(stepToUser)
            //throw new NotImplementedException();

            #endregion
            // 构建控制工厂
            var fact = new CtrlFactory();

            // 构建动作ActionModel
            var vm = new ActionModel();

            // 生成节点新ID
            var nodeId = this.BuildID();

            // 生成事项第一节点
            var nodeCtrl = fact.NodeCtrl();
            vm.CurrentNode = nodeCtrl.BuildTo(beginStep.STEP_ID, beginStep.STEP_NAME, nodeId, prevWorkAnyNodeId);

            // 生成事项第一节点域
            vm.CurrentNodeDomain = nodeCtrl.CreateNodeDomain(vm.CurrentNode.NODE_ID, session.DeptId, session.UserId,
                session.DelegateUserId);

            // 生成事项
            var workCtrl = fact.WorkCtrl();
            vm.CurrentWork = workCtrl.CreateWork(vm.CurrentNode.NODE_ID, session.OrgId);

            // 生成第一阶段
            var stageCtrl = fact.StageCtrl();
            vm.CurrentStage = stageCtrl.CreateStage(vm.CurrentNode.NODE_ID, beginStep.FLOW_ID, beginFlow.FLOW_NAME);

            // 生成需自动创建的实体文笺
            var entityCtrl = fact.EntityCtrl();
            vm.EntityList = entityCtrl.CreateEntityForNode(vm.CurrentNode.STEP_ID, vm.CurrentWork.WORK_ID,
                vm.CurrentNode.NODE_ID, vm.CurrentNodeDomain.DEPT_ID, vm.CurrentNodeDomain.USER_ID);

            //TODO:验证AM下各个对象的完备性
            return vm;
        }

        public ActionModel ContinueWork(SNSession session, T_FLOW beginFlow, T_STEP beginStep, decimal prevWorkAnyNodeId)
        {
            return BeginWork(session, beginFlow, beginStep, prevWorkAnyNodeId);
        }

        public void SelfBeginFlow()
        {
            throw new NotImplementedException();
        }

        public void NextBeginFlow()
        {
            throw new NotImplementedException();
        }

        public decimal QuickSave(ActionModel vm)
        {
            // 保存节点
            // 保存节点域
            // 保存事项
            // 保存阶段
            // 保存实体
            // 保存实体项
            // TODO:保存附件(上传附件)
            // nextnode
            using (var scope = new TransactionScope()) //分布式事务
            {
                var re = 0;
                using (var ctx = new OAContext())
                {
                    #region 保存节点

                    var fact = new CtrlFactory();

                    var nodeCtrl = fact.NodeCtrl();
                    nodeCtrl.SaveNode(vm.CurrentNode);

                    #endregion

                    #region 保存节点域

                    nodeCtrl.SaveNodeDomain(vm.CurrentNodeDomain);

                    #endregion

                    #region 保存事项
                    //TODO:检查work的完备性，必须有事项名、事项类型、{事项编号}、紧急程度、所属单位
                    fact.WorkCtrl().SaveWork(vm.CurrentWork);

                    #endregion

                    #region 保存阶段

                    fact.StageCtrl().SaveStage(vm.CurrentStage);

                    #endregion

                    #region 保存实体列表

                    var entityCtrl = fact.EntityCtrl();
                    entityCtrl.SaveEntities(vm.EntityList, EntityTag.Active.QuickSave);

                    #endregion

                    #region 保存实体项列表

                    entityCtrl.SaveEntityItems(vm.EntityItemList, EntityItemTag.Active.QuickSave);

                    #endregion

                    #region 保存附件列表

                    #endregion

                    re = ctx.SaveChanges();
                }
                scope.Complete(); //提交事务
                return re;
            }
        }

        public void AssistBranch()
        {
            throw new NotImplementedException();
        }

        public void ParallelBranch()
        {
            throw new NotImplementedException();
        }

        public void BeginChange()
        {
            throw new NotImplementedException();
        }

        public void Revoke()
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void Done()
        {
            throw new NotImplementedException();
        }
    }
}
