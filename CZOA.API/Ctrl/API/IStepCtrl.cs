using System.Collections.Generic;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    /// <summary>
    ///     步骤
    /// </summary>
    public interface IStepCtrl
    {
        #region old

        ///// <summary>
        ///// 获取流程入口步骤
        ///// select s.* from t_step s inner join t_step_tag t on s.step_id=t.step_id where s.flow_id=?
        ///// </summary>
        ///// <param name="flowId"></param>
        ///// <returns></returns>
        //IList<DB.T_STEP> GetEnterInStepByFlow(decimal flowId);
        ///// <summary>
        /////     获取步骤的步骤类型列表
        ///// </summary>
        ///// <param name="stepId">步骤ID</param>
        ///// <returns></returns>
        //IList<decimal> GetStepType(decimal stepId);

        ///// <summary>
        /////     获取步骤的步骤类型标签列表
        ///// </summary>
        ///// <param name="step">步骤实例(只需stepId)</param>
        ///// <returns></returns>
        //IList<T_TAG_ITEM> GetStepType(T_STEP step);

        ///// <summary>
        /////     是否为入口：是否为流程的入口
        ///// </summary>
        //bool AllowEnterIn(decimal stepId);

        ///// <summary>
        /////     是否能加笺：是否可添加文笺
        ///// </summary>
        //bool AllowAddForm(decimal stepId);

        /////// <summary>
        /////// 是否起流步：是否可使用新流程开启新阶段，或是否是流程的出口
        /////// </summary>
        ////bool IsStartFlowStep(decimal stepId);

        ///// <summary>
        /////     是否能自接流程
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <returns></returns>
        //bool AllowSelfBeginFlow(decimal stepId);

        ///// <summary>
        /////     是否能下步接流
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <returns></returns>
        //bool AllowNextBeginFlow(decimal stepId);

        /////// <summary>
        /////// 是否发支步：是否可发起协办或并行分支
        /////// </summary>
        ////bool AllowStartBranch(decimal stepId);

        ///// <summary>
        /////     是否能发起协办（会签）
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <returns></returns>
        //bool AllowBeginAssistBranch(decimal stepId);

        ///// <summary>
        /////     是否能发起并行
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <returns></returns>
        //bool AllowBeginParallelBranch(decimal stepId);

        ///// <summary>
        /////     是否能做分支步：是否可做分支线路开启（第一）节点
        ///// </summary>
        //bool AllowBranchLineHeader(decimal stepId);

        ///// <summary>
        /////     是否能做合并步：是否可合并并行分支
        ///// </summary>
        //bool AllowParallelMerge(decimal stepId);

        /////// <summary>
        /////// 是否完结步：是否可完结线路
        /////// </summary>
        ////bool AllowEnding(decimal stepId);

        ///// <summary>
        /////     是否为手动完结步
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <returns></returns>
        //bool AllowManualEnding(decimal stepId);

        ///// <summary>
        /////     是否为自动完结步
        ///// </summary>
        ///// <param name="stepId"></param>
        ///// <returns></returns>
        //bool AllowAutoEnding(decimal stepId);

        #endregion

        /// <summary>
        ///     获取流程的相关步骤列表以及相关步骤的下步骤信息列表
        ///     return {Steps,StepNexts}
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        dynamic GetStepWithNextByFlow(decimal flowId);

        /// <summary>
        ///     检查是否存在此id的步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        bool HasStep(decimal stepId);

        /// <summary>
        ///     获取指定步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        T_STEP GetStep(decimal stepId);

        /// <summary>
        ///     获取步骤的下步骤备选信息（包括已选，标示出来）
        ///     step_id,step_name,step_no,selected
        /// </summary>
        IList<dynamic> GetStepCanUseNextStepInfos(decimal flowId, decimal? stepId = null);

        /// <summary>
        ///     获取步骤的备选文笺模型
        ///     model_id,model_name,model_type_ti,model_idx,selected
        /// </summary>
        IList<dynamic> GetStepCanUseModelInfos(decimal orgId, decimal? stepId = null);

        /// <summary>
        ///     获取步骤的备选输入项
        ///     model_id,model_name,model_item_id,item_name,selected
        /// </summary>
        IList<dynamic> GetStepCanUseModelItemInfos(decimal orgId, decimal? stepId = null);

        /// <summary>
        ///     保存新建步骤及其附属信息
        /// </summary>
        decimal SaveNewStep(T_STEP step, IList<T_STEP_TO_NEXT> nextSteps, IList<T_STEP_MODEL> models,
            IList<T_STEP_MODEL_ITEM> inputs);

        /// <summary>
        ///     保存编辑步骤及其附属信息
        /// </summary>
        decimal SaveEditStep(T_STEP step, IList<T_STEP_TO_NEXT> nextSteps, IList<T_STEP_MODEL> models,
            IList<T_STEP_MODEL_ITEM> inputs);

        /// <summary>
        /// 获取步骤已选文笺列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        IList<T_STEP_MODEL> GetStepModels(decimal stepId);

        /// <summary>
        /// 获取步骤已选输入项列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        IList<T_STEP_MODEL_ITEM> GetStepModelItems(decimal stepId);

        /// <summary>
        /// 获取步骤已有授权列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        IList<T_STEP_AUTH> GetStepAuths(decimal stepId);

        /// <summary>
        /// 获取步骤已有授权列表，带授权项名称
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        IList<dynamic> GetStepAuthsWithName(decimal stepId);

        /// <summary>
        /// 保存流程图及生成的步骤列表及步骤信息
        /// </summary>
        /// <returns></returns>
        decimal SaveFlowSteps(IList<T_STEP> steps, IDictionary<decimal, decimal> stepsStatus,
            dynamic stepsData, byte[] mapXml);
    }
}
