using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;
using CtrlFactory = CZOA.Ctrl.CtrlFactory;

namespace CZOA.WebAPI.Controllers
{
	[RoutePrefix("api/model")] //设置默认前缀
	public class ModelController : SNApiController
	{
		private CZOA.Ctrl.API.IModelCtrl _ctrl;
		private CZOA.Ctrl.API.IFormCtrl _form;
		public ModelController()
		{
			_ctrl = GetCtrl<IModelCtrl>("ModelCtrl");
			_form = GetCtrl<IFormCtrl>("FormCtrl");
		}
		/// <summary>
		/// 获取步骤所关联的模型集合
		/// </summary>
		/// <param name="stepId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("step/{stepid}")]
		public IList<T_MODEL> GetStepModels(decimal stepId)
		{
			return _ctrl.GetStepModels(stepId);
		}
		/// <summary>
		/// 获取模型的模型项集合
		/// </summary>
		/// <param name="modelId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("items/{modelid}")]
		public IList<T_MODEL_ITEM> GetModelItems(decimal modelId)
		{
			return _ctrl.GetModelItems(modelId);
		}

		/// <summary>
		/// 添加模型的模型项集合
		/// </summary>
		/// <param name="modelId"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("items/add")]
		public decimal AddModelItems(dynamic data)
		{
			var list = DynamicTo<IList<T_MODEL_ITEM>>(data);
			return _ctrl.AddModelItme(list);
		}

		/// <summary>
		/// 获取session所存单位的所有模型
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public dynamic GetAllModel()
		{
			return _ctrl.GetAllModel(this.GetSession().OrgId);
		}
		/// <summary>
		/// 获取指定模型及其表单
		/// </summary>
		/// <param name="modelID"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{modelid}")]
		public dynamic GetModelByID(decimal modelID)
		{
			var model = _ctrl.GetModelByID(modelID);
			if (model.FORM_ID == null)
			{
				model.FORM_ID = 0;
			}
			var form = _form.GetForm(model.FORM_ID.Value);
			var data = new
			{
				Model = model,
				Form = form,
			};
			return data;
		}
		/// <summary>
		/// 添加一个模型及其表单信息
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public decimal AddModel(dynamic data)
		{
			var mode = DynamicTo<T_MODEL>(data.Model);
			var form = DynamicTo<T_FORM>(data.Form);
			_ctrl.AddModel(mode);
			_form.AddForm(form);
			return 1;
		}
		/// <summary>
		/// 更新一个模型及其表单信息
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("edit")]
		public decimal UpdateModel(dynamic data)
		{
			var mode = DynamicTo<T_MODEL>(data.Model);
			var form = DynamicTo<T_FORM>(data.Form);
			_ctrl.UpdateModel(mode);
			if (form != null)
			{
				var f = _form.GetForm(form.FORM_ID);
				if (f == null)
				{
					_form.AddForm(form);
				}
				else
				{
					_form.UpdateForm(form);
				}
			}
			return 1;
		}
		/// <summary>
		/// 删除一个模型及其表单信息
		/// </summary>
		/// <param name="modelID"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("del/{modelid}")]
		public decimal DelModelByID(decimal modelID)
		{
			var model = _ctrl.GetModelByID(modelID);
			if (model.FORM_ID != null)
			{
				_form.DelForm(model.FORM_ID.Value);
			}
			_ctrl.DelModelByID(modelID);

			return 1;
		}

	}
}
