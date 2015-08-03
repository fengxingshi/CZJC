namespace CZOA.Ctrl.API
{
    /// <summary>
    ///     控制器工厂
    /// </summary>
    public abstract class CtrlFactory
    {
        public abstract ITestCtrl TestCtrl();

        /// <summary>
        ///     用户控制器
        /// </summary>
        /// <returns></returns>
        public abstract IUserCtrl UserCtrl();

        /// <summary>
        ///     节点控制器
        /// </summary>
        /// <returns></returns>
        public abstract INodeCtrl NodeCtrl();

        /// <summary>
        ///     事项控制器
        /// </summary>
        /// <returns></returns>
        public abstract IWorkCtrl WorkCtrl();

        /// <summary>
        ///     阶段控制器
        /// </summary>
        /// <returns></returns>
        public abstract IStageCtrl StageCtrl();

        /// <summary>
        ///     实体（数据文笺）控制器
        /// </summary>
        /// <returns></returns>
        public abstract IEntityCtrl EntityCtrl();

        /// <summary>
        ///     流程定义控制器
        /// </summary>
        /// <returns></returns>
        public abstract IFlowCtrl FlowCtrl();

        /// <summary>
        ///     步骤定义控制器
        /// </summary>
        /// <returns></returns>
        public abstract IStepCtrl StepCtrl();
        /// <summary>
        ///     标签控制器
        /// </summary>
        /// <returns></returns>
        public abstract ITagCtrl TagCtrl();

		/// <summary>
		/// 处室控制器
		/// </summary>
		/// <returns></returns>
		public abstract IDeptCtrl DeptCtrl();

        /// <summary>
        /// 单位控制器
        /// </summary>
        /// <returns></returns>
        public abstract IOrgCtrl OrgCtrl();

        /// <summary>
        /// 角色控制器
        /// </summary>
        /// <returns></returns>
        public abstract IRoleCtrl RoleCtrl();

        public abstract IModelCtrl ModelCtrl();

        public abstract IFormCtrl FormCtrl();
        public abstract IFileType  FileTypeCtrl();

        /// <summary>
        /// 制度库文件控制器
        /// </summary>
        /// <returns></returns>
        public abstract IRulesFileCtrl RulesFileCtrl();
    }
}
