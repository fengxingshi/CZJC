using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CZJC.Ctrl.API;
using CZJC.DB;

namespace CZJC.WebAPI.Controllers
{
	[RoutePrefix("api/form")] //设置默认前缀
	public class FormController : SNApiController
	{
		private CZJC.Ctrl.API.IFormCtrl _ctrl;
		public FormController()
		{
			_ctrl = GetCtrl<IFormCtrl>("FormCtrl");
		}
        /// <summary>
        /// 获取指定id的表单
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route("{formid}")]
		public T_FORM GetForm(decimal formId)
		{
			return _ctrl.GetForm(formId);
		}
        /// <summary>
        /// 添加一个表单
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
		[HttpPost]
		[Route("add")]
		public decimal AddForm(T_FORM form)
		{
			return _ctrl.AddForm(form);
		}
        /// <summary>
        /// 更新一个表单
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
		[HttpPut]
		[Route("edit")]
		public decimal UpdateForm(T_FORM form)
		{
			return _ctrl.UpdateForm(form);
		}
        /// <summary>
        /// 删除一个表单（虚删）
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
		[HttpDelete]
		[Route("del/{formid}")]
		public decimal DelForm(decimal formID)
		{
			return _ctrl.DelForm(formID);
		}

	}
}
