using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
	public interface IFlowCtrl
	{
		/// <summary>
		/// 获取授权用户的可用流程
		/// select *  from t_flow where flow_id in (select flow_id from t_flow_auth t where t.auth_role in (select domain_at roleId from t_user_domain where user_id = ?))
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		IList<DB.T_FLOW> GetFlowByAuthUser(decimal userId);
        /// <summary>
        /// 获取流程授权（List dynamic)
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
	    IList<dynamic> GetFlowAuths(decimal flowId);
		/// <summary>
		/// 获取所有流程定义
		/// </summary>
		/// <returns></returns>
		dynamic GetAllFlow();

		/// <summary>
		/// 获取单个流程定义
		/// </summary>
		/// <param name="flowID"></param>
		/// <returns></returns>
		DB.T_FLOW GetFlow(decimal flowID);

		/// <summary>
		/// 获取单个流程权限定义
		/// </summary>
		/// <param name="flowID"></param>
		/// <returns></returns>
		IList<DB.T_FLOW_AUTH> GetFlowAuth(decimal flowID);

		/// <summary>
		/// 添加流程（包括权限）
		/// </summary>
		/// <param name="flow"></param>
		/// <param name="auth"></param>
		/// <returns></returns>
		decimal AddFlow(DB.T_FLOW flow, IList<DB.T_FLOW_AUTH> auth);

		/// <summary>
		/// 编辑流程
		/// </summary>
		/// <param name="flow"></param>
		/// <param name="auth">为空只更新流程信息</param>
		/// <returns></returns>
		decimal UpdateFlow(DB.T_FLOW flow, IList<DB.T_FLOW_AUTH> auth = null);

		/// <summary>
		/// 删除流程及相关授权
		/// </summary>
		/// <param name="flowID"></param>
		/// <returns></returns>
		decimal DeleteFlow(decimal flowID);

        /// <summary>
        /// 获取流程的分类集合（标签项集合）
        /// </summary>
        /// <returns></returns>
	    IList<DB.T_TAG_ITEM> GetFlowCategoryTI();

	    /// <summary>
	    ///     获取流程可用模型 T_MODEL
	    /// </summary>
	    /// <param name="flowId"></param>
	    /// <returns></returns>
	    IList<T_MODEL> GetFlowModels(decimal flowId);

	    /// <summary>
	    ///     获取流程可用输入项 T_MODEL_ITEM
	    /// </summary>
	    /// <param name="flowId"></param>
	    /// <returns></returns>
	    IList<T_MODEL_ITEM> GetFlowInputs(decimal flowId);

	    /// <summary>
	    /// 获取流程所有步骤的授权信息
	    /// </summary>
	    /// <param name="flowId"></param>
	    /// <returns></returns>
	    IList<dynamic> GetFlowStepAuths(decimal flowId);

	    IList<T_FLOW> GetFlows();
	    IList<T_FLOW> GetFlowByDepartmentAndOrgId(decimal departmentId, decimal orgId,List<decimal> roleList);
	}
}
