using System;
using System.Collections.Generic;
using System.Web.Http;
using CZOA.Ctrl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CZOA.WebAPI.Controllers
{
	public class SNApiController : ApiController
	{
		public static IDictionary<string, object> _CtrlCache;

        /// <summary>
        /// 构造，初始化各个控制器，存入缓存
        /// </summary>
		public SNApiController()
		{
			if (_CtrlCache == null)
			{
				_CtrlCache = new Dictionary<string, object>
                {
                    {
                        "TestCtrl", new CtrlFactory().TestCtrl()
                    },
                    {
                        "RoleCtrl", new CtrlFactory().RoleCtrl()
                    },
                    {
                        "DeptCtrl", new CtrlFactory().DeptCtrl()
                    },
                    {
                        "FlowCtrl", new CtrlFactory().FlowCtrl()
                    },
                    {
                        "OrgCtrl", new CtrlFactory().OrgCtrl()
                    },
                    {
                        "StepCtrl", new CtrlFactory().StepCtrl()
                    },
                    {
                        "TagCtrl", new CtrlFactory().TagCtrl()
                    },
                    {
                        "UserCtrl", new CtrlFactory().UserCtrl()
                    },
					{
                        "ModelCtrl", new CtrlFactory().ModelCtrl()
                    },
					{
                        "FormCtrl", new CtrlFactory().FormCtrl()
                    },
                    {
                        "FileType", new CtrlFactory().FileTypeCtrl()
                    },
                    {
                        "RulesFile", new CtrlFactory().RulesFileCtrl()
                    }
                };
			}
		}
        /// <summary>
        /// 从缓存中获取指定的控制器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctrlName"></param>
        /// <returns></returns>
		public static T GetCtrl<T>(string ctrlName)
		{
			return (T)_CtrlCache[ctrlName];
		}
        /// <summary>
        /// 将json对象或数组转为对应的强类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
		public static T DynamicTo<T>(object t)
		{
			if (t != null)
			{
			    return JsonConvert.DeserializeObject<T>((t as JContainer).ToString());
			}
			else
			{
				return default(T);
			}
		}
        [HttpGet]
        [Route("~/api/now")]
	    public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }
	}
}
