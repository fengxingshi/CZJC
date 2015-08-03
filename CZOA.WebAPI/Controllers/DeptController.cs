using System.Collections.Generic;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.WebAPI.Controllers
{
    [RoutePrefix("api/dept")] //设置默认前缀
    public class DeptController : SNApiController
    {
        private readonly IDeptCtrl _ctrl;

        public DeptController()
        {
            _ctrl = GetCtrl<IDeptCtrl>("DeptCtrl");
        }
        /// <summary>
        /// 获取当前单位所有处室
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IList<T_DEPT> GetAll()
        {
            var session = this.GetSession();
            var re = _ctrl.GetAllDeptByOrgID(session.OrgId);
            return re;
        }
        /// <summary>
        /// 获取当前单位所有处室
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/orgId/{orgId}")]
        public IList<T_DEPT> GetAllByOrgId(decimal orgId)
        {
            var re = _ctrl.GetAllDeptByOrgID(orgId);
            return re;
        }
        /// <summary>
        /// 根据id获取一个处室
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{deptId}")]
        public T_DEPT GetDept(decimal deptId)
        {
            return _ctrl.GetDept(deptId);
        }
        /// <summary>
        /// 添加一个处室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public decimal AddDept(T_DEPT dept)
        {
            var re = _ctrl.AddDept(dept);
            return re;
        }
        /// <summary>
        /// 更新一个处室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        public decimal UpdateDept(T_DEPT dept)
        {
            var re = _ctrl.UpdateDept(dept);
            return re;
        }
        /// <summary>
        /// 删除一个处室（虚删）
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("del/{deptId}")]
        public decimal DeleteDept(decimal deptId)
        {
            var re = _ctrl.DeleteDept(deptId);
            return re;
        }
        /// <summary>
        /// 与绩效系统同步处室
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SynchronousDept")]
        public decimal SynchronousDept()
        {
            var re = _ctrl.SynchronousDept();
            return re;
        }
    }
}
