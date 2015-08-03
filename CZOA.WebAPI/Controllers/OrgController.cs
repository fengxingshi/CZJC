using System.Collections.Generic;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;
using CtrlFactory = CZOA.Ctrl.CtrlFactory;

namespace CZOA.WebAPI.Controllers
{
    [RoutePrefix("api/org")]
    public class OrgController : SNApiController
    {
        private readonly IOrgCtrl _ctrl;

        public OrgController()
        {
            _ctrl = GetCtrl<IOrgCtrl>("OrgCtrl");
        }
        /// <summary>
        /// 获取全部单位集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IList<T_ORG> GetAll()
        {
            return _ctrl.GetAll();
        }
        /// <summary>
        /// 获取指定id的单位
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{orgId}")]
        public T_ORG GetOrg(decimal orgId)
        {
           return _ctrl.GetOrg(orgId);
        }
        /// <summary>
        /// 添加一个单位
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public decimal AddOrg(T_ORG org)
        {
            var re = _ctrl.AddOrg(org);
            return re;
        }
        /// <summary>
        /// 更新一个单位
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        public decimal UpdateOrg(T_ORG org)
        {
            var re = _ctrl.UpdateOrg(org);
            return re;
        }
        /// <summary>
        /// 删除一个单位（虚删）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("del/{orgId}")]
        public decimal DeleteOrg(decimal orgId)
        {
            var re = _ctrl.DeleteOrg(orgId);
            return re;
        }
        /// <summary>
        /// 与绩效系统同步单位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Synchronous")]
        public decimal SynchronousOrg()
        {
            var re = _ctrl.SynchronousOrg();
            return re;
        }
    }
}
