using System;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    /// <summary>
    ///     事项
    /// </summary>
    public interface IWorkCtrl
    {
        /// <summary>
        ///     创建事项
        /// --------------------
        /// 1.确定事项的入口：确定入口步骤
        /// 2.根据入口步骤生曾第一节点(即产生了主线路)
        /// 3.根据入口步骤所属的流程，生成相关阶段
        /// </summary>
        /// <returns></returns>
        T_WORK CreateWork(decimal theCreateWorkNodeId, decimal orgId);
        /// <summary>
        /// 保存事项
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        decimal SaveWork(T_WORK work);
    }
}
