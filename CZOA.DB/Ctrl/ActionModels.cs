using System.Collections.Generic;
using CZOA.DB;


namespace CZOA.Ctrl
{
    #region del
    /// <summary>
    /// 开启事项动作模型
    /// BeginWorkModel
    /// </summary>
    //public class BeginWorkModel
    //{
    //    /// <summary>
    //    ///     事项第一节点
    //    /// FirstNodeOfWork
    //    /// </summary>
    //    public T_NODE FirstNodeOfWork { get; set; }

    //    /// <summary>
    //    ///     事项第一节点域
    //    /// FirstNodeDomain
    //    /// </summary>
    //    public T_NODE_DOMAIN FirstNodeDomain { get; set; }

    //    /// <summary>
    //    ///     事项
    //    /// TheWork
    //    /// </summary>
    //    public T_WORK TheWork { get; set; }

    //    /// <summary>
    //    ///     事项第一阶段
    //    /// FirstStageOfWork
    //    /// </summary>
    //    public T_STAGE FirstStageOfWork { get; set; }

    //    /// <summary>
    //    ///     节点自动生成实体（文笺）
    //    /// NodeAutoAddedEntity
    //    /// </summary>
    //    public IList<T_ENTITY> NodeAutoAddedEntity { get; set; }
    //}
    #endregion
    /// <summary>
    /// /*动作模型：相当于业务逻辑上的ViewModel(LogicModel)*/
    /// </summary>
    public class ActionModel
    {
        /// <summary>
        /// 当前操作节点
        /// </summary>
        public T_NODE CurrentNode { get; set; }
        /// <summary>
        /// 当前节点激活的节点域
        /// </summary>
        public T_NODE_DOMAIN CurrentNodeDomain { get; set; }
        /// <summary>
        /// 当前事项
        /// </summary>
        public T_WORK CurrentWork { get; set; }
        /// <summary>
        /// 当前阶段
        /// </summary>
        public T_STAGE CurrentStage { get; set; }
        /// <summary>
        /// 实体列表
        /// </summary>
        public IList<T_ENTITY> EntityList { get; set; }
        /// <summary>
        /// 实体相关的实体项(输入项)列表。{延迟加载，打开实体时再加载响应实体项}
        /// </summary>
        public IList<T_ENTITY_ITEM> EntityItemList { get; set; }
        /// <summary>
        /// 附件列表。{于实体列表同时加载}
        /// </summary>
        public IList<T_ATTA> AttaList { get; set; }
    }
}
