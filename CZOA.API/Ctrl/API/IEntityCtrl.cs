using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IEntityCtrl
    {
        /// <summary>
        /// 根据节点参数生成需自动创建的实体列表
        /// </summary>
        IList<T_ENTITY> CreateEntityForNode(decimal stepId, decimal workId, decimal nodeId, decimal deptId,
            decimal userId);

        /// <summary>
        /// 获取事项的实体列表
        /// {获取所有事项相关实体，不区分暂存、正式，不区分表单型、动态型，前台使用时再做区分}
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        IList<T_ENTITY> GetEntityListByWork(decimal workId);
        /// <summary>
        /// 保存实体列表（新则添加，改则更新）
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        decimal SaveEntities(IList<T_ENTITY> entityList, decimal active = Tag.EntityTag.Active.DoneSave);

        /// <summary>
        /// 获取实体的实体项列表。默认获取正式数据，传入用户参数则包括用户暂存的数据
        /// </summary>
        /// <param name="entityId">实体（表单）</param>
        /// <param name="userId">暂存的用户</param>
        /// <returns></returns>
        IList<T_ENTITY_ITEM> GetEntityItems(decimal entityId, decimal? userId = null);
        /// <summary>
        /// 保存输入项（与库中数据对比判断为新增还是修改后再提交入库）
        /// TODO:性能(读库! + 写库!)
        /// </summary>
        /// <param name="entityList">待更新的输入项集合</param>
        /// <param name="active">正式or暂存</param>
        /// <returns>库中变动的行数</returns>
        decimal SaveEntityItems(IList<T_ENTITY_ITEM> entityList, decimal active = Tag.EntityItemTag.Active.DoneSave);
    }
}
