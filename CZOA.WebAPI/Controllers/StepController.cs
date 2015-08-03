using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;
using CZOA.Tag;
using CZOA._路由表;

namespace CZOA.WebAPI.Controllers
{
    [RoutePrefix("api/step")]
    public class StepController : SNApiController
    {
        private readonly IStepCtrl _ctrl;

        public StepController()
        {
            _ctrl = GetCtrl<IStepCtrl>("StepCtrl");
        }

        /// <summary>
        ///     库中是否存在指定id的步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("has/{stepid}")]
        public bool HasStep(decimal stepId)
        {
            var re = _ctrl.HasStep(stepId);
            return re;
        }

        /// <summary>
        ///     获取指定步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{stepid}")]
        public T_STEP GetStep(decimal stepId)
        {
            var re = _ctrl.GetStep(stepId);
            return re;
        }

        /// <summary>
        ///     获取流程的相关步骤列表以及相关步骤的下步骤信息
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/flow/{flowid}/steps")]
        public dynamic GetStepWithNextByFlow(decimal flowId)
        {
            var re = _ctrl.GetStepWithNextByFlow(flowId);
            return re;
        }

        /// <summary>
        ///     获取步骤的下步骤备选信息（包括已选，标示出来）
        ///     step_id,step_name,step_no,selected
        /// </summary>
        [HttpGet]
        [Route("nextsteps/withselected/{flowid}-{stepid}")]
        public IList<dynamic> GetStepCanUseNextStepInfos(decimal flowId, decimal? stepId = null)
        {
            var re = _ctrl.GetStepCanUseNextStepInfos(flowId, stepId);
            return re;
        }

        /// <summary>
        ///     获取步骤的备选文笺模型
        ///     model_id,model_name,model_type_ti,model_idx,selected
        /// </summary>
        [HttpGet]
        [Route("models/withselected/{orgid}-{stepid}")]
        public IList<dynamic> GetStepCanUseModelInfos(decimal orgId, decimal? stepId = null)
        {
            var re = _ctrl.GetStepCanUseModelInfos(orgId, stepId);
            return re;
        }

        /// <summary>
        ///     获取步骤的备选输入项
        /// </summary>
        [HttpGet]
        [Route("modelitems/withselected/{orgid}-{stepid}")]
        public IList<dynamic> GetStepCanUseModelItemInfos(decimal orgId, decimal? stepId = null)
        {
            var re = _ctrl.GetStepCanUseModelItemInfos(orgId, stepId);
            return re;
        }

        /// <summary>
        ///     获取步骤评价类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("commenttype")]
        public IList<dynamic> GetStepCommentTypes()
        {
            var tagCtrl = GetCtrl<ITagCtrl>("TagCtrl");

            var re = tagCtrl.GetTagItemByTag(StepTag.CommentType._TagID).Select(p => new
            {
                ID = p.TAG_ITEM_ID,
                NAME = p.ITEM_NAME
            }).ToList<dynamic>();

            return re;
        }

        /// <summary>
        ///     获取步骤已选文笺id列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{stepid}/models/id")]
        public IList<decimal> GetStepModelsId(decimal stepId)
        {
            var re = _ctrl.GetStepModels(stepId).Select(p => p.MODEL_ID);
            return re.ToList();
        }

        /// <summary>
        ///     获取步骤已选输入项id列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{stepid}/inputs/id")]
        public IList<decimal> GetStepModelItemsId(decimal stepId)
        {
            var re = _ctrl.GetStepModelItems(stepId).Select(p => p.MODEL_ITEM_ID);
            return re.ToList();
        }

        /// <summary>
        /// 获取步骤已有授权id对列表{auth_scope_ti,auth_to}
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{stepId}/auths/id")]
        public IList<dynamic> GetStepAuthsId(decimal stepId)
        {
            var re = _ctrl.GetStepAuths(stepId).Select(p => new 
            {
                p.AUTH_SCOPE_TI,
                p.AUTH_TO
            });

            return re.ToList<dynamic>();
        }

        /// <summary>
        /// 获取步骤已有授权列表，带授权项名称
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{stepId}/auths/withname")]
        public IList<dynamic> GetStepAuthsWithName(decimal stepId)
        {
            var re = _ctrl.GetStepAuthsWithName(stepId).Select(p => new
            {
                p.AUTH_SCOPE_TI,
                p.AUTH_TO,
                p.AUTH_TO_NAME
            });

            return re.ToList<dynamic>();
        }

        /// <summary>
        /// 保存流程及步骤设置
        /// </summary>
        /// <param name="saveData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveflow")]
        public decimal SaveFlowSteps(dynamic saveData)
        {
            var steps = DynamicTo<IList<T_STEP>>(saveData.Steps);
            var stepsStatus = DynamicTo<IDictionary<decimal, decimal>>(saveData.StepsStatus);
            var stepsData = (saveData.StepsData);
            var mapXml = (byte[])saveData.MapXml;// System.Text.Encoding.UTF8.GetBytes(saveData.MapXml.Value.ToString());
            var re = _ctrl.SaveFlowSteps(steps, stepsStatus, stepsData, mapXml);
            return re;
        }

        /// <summary>
        ///     添加一个步骤及其附属信息
        /// </summary>
        /// <param name="savedata"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Step.SaveNewStep)]
        public decimal SaveNewStep(dynamic savedata)
        {
            var step = DynamicTo<T_STEP>(savedata.Step);
            var nextSteps = DynamicTo<IList<T_STEP_TO_NEXT>>(savedata.NextSteps);
            var models = DynamicTo<IList<T_STEP_MODEL>>(savedata.Models);
            var inputs = DynamicTo<IList<T_STEP_MODEL_ITEM>>(savedata.Inputs);

            var re = _ctrl.SaveNewStep(step, nextSteps, models, inputs);
            return re;
        }

        /// <summary>
        ///     更改一个步骤及其附属信息
        /// </summary>
        /// <param name="savedata"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(Step.SaveEditStep)]
        public decimal SaveEditStep(dynamic savedata)
        {
            var step = DynamicTo<T_STEP>(savedata.Step);
            var nextSteps = DynamicTo<IList<T_STEP_TO_NEXT>>(savedata.NextSteps);
            var models = DynamicTo<IList<T_STEP_MODEL>>(savedata.Models);
            var inputs = DynamicTo<IList<T_STEP_MODEL_ITEM>>(savedata.Inputs);

            var re = _ctrl.SaveEditStep(step, nextSteps, models, inputs);
            return re;
        }
    }
}
