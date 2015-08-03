using System.Collections.Generic;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IOrgCtrl
    {
        /// <summary>
        ///     获取所有单位
        /// </summary>
        /// <returns></returns>
        IList<T_ORG> GetAll();

        /// <summary>
        ///     查
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        T_ORG GetOrg(decimal orgId);

        /// <summary>
        ///     增
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        decimal AddOrg(T_ORG org);

        /// <summary>
        ///     改
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        decimal UpdateOrg(T_ORG org);

        /// <summary>
        ///     删
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        decimal DeleteOrg(decimal orgId);

        /// <summary>
        ///     同步
        /// </summary>
        /// <returns></returns>
        decimal SynchronousOrg();
    }
}
