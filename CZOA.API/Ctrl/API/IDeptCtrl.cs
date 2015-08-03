using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
	public interface IDeptCtrl
	{
		/// <summary>
		/// 获取当前单位所有处室
		/// </summary>
		/// <returns></returns>
		IList<DB.T_DEPT> GetAllDeptByOrgID(decimal orgID);
        /// <summary>
        /// 查
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
	    T_DEPT GetDept(decimal deptId);
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
	    decimal AddDept(T_DEPT dept);
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
	    decimal UpdateDept(T_DEPT dept);
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
	    decimal DeleteDept(decimal deptId);
        /// <summary>
        /// 同步
        /// </summary>
        /// <returns></returns>
	    decimal SynchronousDept();

	}
}
