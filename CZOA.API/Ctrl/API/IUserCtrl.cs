using System.Collections.Generic;
using System.Data;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IUserCtrl
    {
        /// <summary>
        /// 获取用户Session
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SNSession GetUserSession(decimal userId);
        /// <summary>
        ///     获取用户所属处室（包括上溯上级处室）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<T_DEPT> GetUserAllDepts(decimal userId);

        /// <summary>
        ///     获取用户所属处室
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        T_DEPT GetUserDept(decimal userId);

        /// <summary>
        ///     获取用户的角色列表（包括每个角色的上溯上级角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<T_ROLE> GetUserAllRoles(decimal userId);

        /// <summary>
        ///     获取用户的角色（只取直接赋予的角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<T_ROLE> GetUserRoles(decimal userId);
        /// <summary>
        /// 新增用户以及用户的域
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userDomains"></param>
        /// <returns></returns>
        decimal AddUserWithDomain(T_USER user, IList<T_USER_DOMAIN> userDomains);
        /// <summary>
        /// 修改用户以及用户的域
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userDomains"></param>
        /// <returns></returns>
        decimal UpdateUserWithUserDomain(T_USER user, IList<T_USER_DOMAIN> userDomains = null);
        /// <summary>
        /// 获得所有的用户以及域（根据domainat）
        /// </summary>
        /// <param name="domainAt"></param>
        /// <returns></returns>
        dynamic GetUserAndUserDomain(decimal domainAt);
        /// <summary>
        /// 获得所有的用户以及域（根据domainat）name
        /// </summary>
        /// <param name="domainAt"></param>
        /// <returns></returns>
        dynamic GetUserAndUserName(decimal domainAt);
        /// <summary>
        /// 根据Userid返回角色域
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        DataTable GetUserDomainsByUserId(decimal userId);
        /// <summary>
        ///同步用户表
        /// </summary>
        /// <returns></returns>
        decimal SynchronousUser();
        /// <summary>
        /// 同步用户domain
        /// </summary>
        /// <returns></returns>
        decimal SynchronousUserDomain();
    }
}
