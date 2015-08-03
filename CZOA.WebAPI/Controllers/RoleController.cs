using System.Collections.Generic;
using System.Web.Http;
using CZJC.Ctrl.API;
using CZJC.DB;

namespace CZJC.WebAPI.Controllers
{
    [RoutePrefix("api/role")]
    public class RoleController : SNApiController
    {
        private readonly IRoleCtrl _ctrl;

        public RoleController()
        {
            _ctrl = GetCtrl<IRoleCtrl>("RoleCtrl");
        }
        /// <summary>
        /// 获取session中所存单位的所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IList<T_ROLE> GetAllByOrg()
        {
            var sn = this.GetSession();
            var re = _ctrl.GetAllRoleByOrgId(sn.OrgId);
            return re;
        }
        /// <summary>
        /// 获取指定单位的所有角色
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/orgId/{orgId}")]
        public IList<T_ROLE> GetAllByOrgId(decimal orgId)
        {
            var re = _ctrl.GetAllRoleByOrgId(orgId);
            return re;
        }
        /// <summary>
        /// 获取指定id的角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{roleId}")]
        public T_ROLE GetRole(decimal roleId)
        {
            var re = _ctrl.GetRole(roleId);
            return re;
        }
        /// <summary>
        /// 添加一个角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public decimal AddRole(T_ROLE role)
        {
            var re = _ctrl.AddRole(role);
            return re;
        }
        /// <summary>
        /// 更新一个角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        public decimal UpdateRole(T_ROLE role)
        {
            var re = _ctrl.UpdateRole(role);
            return re;
        }
        /// <summary>
        /// 删除一个角色（虚删）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("del/{roleId}")]
        public decimal DeleteRole(decimal roleId)
        {
            var re = _ctrl.DeleteRole(roleId);
            return re;
        }
    }
}
