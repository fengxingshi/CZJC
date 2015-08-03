using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.WebAPI.Controllers
{
	[RoutePrefix("api/flow")] //设置默认前缀
	public class FlowController : SNApiController
	{
        private readonly IFlowCtrl _ctrl;

		public FlowController()
		{
			_ctrl = GetCtrl<IFlowCtrl>("FlowCtrl");
		}

	    [HttpGet]
	    [Route("{flowId}")]
	    public T_FLOW GetFlowById(decimal flowId)
	    {
	        var re = _ctrl.GetFlow(flowId);
            return re;
	    }

	    [HttpGet]
        [Route("org/{orgId}/dept/{departmentId}")]
	    public IList<T_FLOW> GetFlowByDepartmentAndOrgId(decimal departmentId, decimal orgId)
	    {
	        var roleList = this.GetSession().RoleIds.ToList();
	        var re = _ctrl.GetFlowByDepartmentAndOrgId(departmentId, orgId,roleList);
	        return re;
	    }

	    [HttpGet]
	    [Route("list")]
	    public IList<T_FLOW> GetFlows()
	    {
	        var re = _ctrl.GetFlows();
	        return re;
	    }

        /// <summary>
        ///     获取流程授权
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route("auth/dynamic/{flowid}")]
		public IList<dynamic> GetFlowAuthDynamic(decimal flowId)
		{
			var re = _ctrl.GetFlowAuths(flowId);
			return re;
		}

		/// <summary>
        ///     获取流程的授权信息列表
		/// </summary>
		/// <param name="flowId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("auth")]
        public IList<T_FLOW_AUTH> GetFlowAuths(decimal flowId)
		{
			var re = _ctrl.GetFlowAuth(flowId);
			return re;
		}

		/// <summary>
        ///     获取流程的分类集合（标签项集合）
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("category/tags")]
        public IList<T_TAG_ITEM> GetFlowCategoryTI()
		{
			var re = _ctrl.GetFlowCategoryTI();
			return re;
		}

		/// <summary>
        ///     获取所有流程定义
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all/withinfo")]
		public dynamic GetAll()
		{
			var re = _ctrl.GetAllFlow();
			return re;
		}

		/// <summary>
        ///     获取授权用户的可用流程
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("user/{userid}")]
        public IList<T_FLOW> GetFlowByAuthUser(decimal userId)
		{
			return _ctrl.GetFlowByAuthUser(userId);
		}

		/// <summary>
        ///     获取指定流程及其授权信息
		/// </summary>
		/// <param name="flowID"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{flowid}/withauth")]
		public dynamic GetFlowWithAuths(decimal flowID)
		{
			return new
			{
				Flow = _ctrl.GetFlow(flowID),
				Auth = _ctrl.GetFlowAuth(flowID),
			};
		}

        /// <summary>
        ///     获取流程可用模型 T_MODEL
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{flowid}/models")]
        public IList<T_MODEL> GetFlowModels(decimal flowId)
        {
            var re = _ctrl.GetFlowModels(flowId);
            return re;
        }

        /// <summary>
        ///     获取流程可用输入项 T_MODEL_ITEM
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{flowid}/modelitems")]
        public IList<T_MODEL_ITEM> GetFlowInputs(decimal flowId)
        {
            var re = _ctrl.GetFlowInputs(flowId);
            return re;
        }

        /// <summary>
        /// 获取流程所有步骤的授权信息
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
	    [HttpGet]
	    [Route("{flowid}/stepauths")]
	    public IList<dynamic> GetFlowStepAuths(decimal flowId)
	    {
            var re = _ctrl.GetFlowStepAuths(flowId);
            return re;
	    }

        /// <summary>
        ///     添加一个流程及其授权信息
        /// </summary>
        /// <param name="flowInfo"></param>
        /// <returns></returns>
		[HttpPost]
		[Route("add")]
		public decimal PostFlow(dynamic flowInfo)
		{
            var flow = DynamicTo<T_FLOW>(flowInfo.Flow);
            var auth = flowInfo.Auth;
            IList<T_FLOW_AUTH> a = null;
			if (auth != null)
			{
                a = DynamicTo<IList<T_FLOW_AUTH>>(auth);
			}
			var re = _ctrl.AddFlow(flow, a);
			return re;
		}

        /// <summary>
        ///     更新一个流程及其授权信息
        /// </summary>
        /// <param name="flowInfo"></param>
        /// <returns></returns>
		[HttpPut]
		[Route("edit")]
		public decimal PutFlow(dynamic flowInfo)
		{
            var flow = DynamicTo<T_FLOW>(flowInfo.Flow);
			dynamic auth = null;
			if (flowInfo.Auth != null)
			{
                auth = DynamicTo<IList<T_FLOW_AUTH>>(flowInfo.Auth);
			}
			return _ctrl.UpdateFlow(flow, auth);
		}

        /// <summary>
        ///     删除一个流程及其授权信息（虚删）
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
		[HttpDelete]
        [Route("del/{flowid}")]
		public decimal DeleteFlow(decimal flowID)
		{
			return _ctrl.DeleteFlow(flowID);
		}
	}
}
