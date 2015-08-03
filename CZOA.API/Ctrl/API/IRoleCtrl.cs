using System.Collections.Generic;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IRoleCtrl
    {
        /// <summary>
        ///     获取指定单位的所有角色
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<T_ROLE> GetAllRoleByOrgId(decimal orgId);

        /// <summary>
        ///     根据roleid获得一行数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        T_ROLE GetRole(decimal roleId);

        /// <summary>
        ///     新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        decimal AddRole(T_ROLE role);

        /// <summary>
        ///     更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        decimal UpdateRole(T_ROLE role);

        /// <summary>
        ///     删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        decimal DeleteRole(decimal roleId);
    }
}
