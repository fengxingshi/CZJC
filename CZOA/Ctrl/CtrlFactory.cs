using CZJC.Ctrl.API;

namespace CZJC.Ctrl
{
    /// <summary>
    ///     控制器工厂
    /// </summary>
    public class CtrlFactory : API.CtrlFactory
    {
        public override ITestCtrl TestCtrl()
        {
            return new TestCtrl();
        }

        public override IUserCtrl UserCtrl()
        {
            return new UserCtrl();
        }

        /// <summary>
        ///     节点控制器
        /// </summary>
        /// <returns></returns>
        public override INodeCtrl NodeCtrl()
        {
            return new NodeCtrl();
        }

        /// <summary>
        ///     事项控制器
        /// </summary>
        /// <returns></returns>
        public override IWorkCtrl WorkCtrl()
        {
            return new WorkCtrl();
        }

        /// <summary>
        ///     阶段控制器
        /// </summary>
        /// <returns></returns>
        public override IStageCtrl StageCtrl()
        {
            return new StageCtrl();
        }

        /// <summary>
        ///     实体（数据文笺）控制器
        /// </summary>
        /// <returns></returns>
        public override IEntityCtrl EntityCtrl()
        {
            return new EntityCtrl();
        }

        /// <summary>
        ///     流程定义控制器
        /// </summary>
        /// <returns></returns>
        public override IFlowCtrl FlowCtrl()
        {
            return new FlowCtrl();
        }

        /// <summary>
        ///     步骤定义控制器
        /// </summary>
        /// <returns></returns>
        public override IStepCtrl StepCtrl()
        {
            return new StepCtrl();
        }

        /// <summary>
        ///     标签控制器
        /// </summary>
        /// <returns></returns>
        public override ITagCtrl TagCtrl()
        {
            return new TagCtrl();
        }

        /// <summary>
        ///     处室控制器
        /// </summary>
        /// <returns></returns>
        public override IDeptCtrl DeptCtrl()
        {
            return new DeptCtrl();
        }

        /// <summary>
        ///     单位控制器
        /// </summary>
        /// <returns></returns>
        public override IOrgCtrl OrgCtrl()
        {
            return new OrgCtrl();
        }

        /// <summary>
        ///     角色控制器
        /// </summary>
        /// <returns></returns>
        public override IRoleCtrl RoleCtrl()
        {
            return new RoleCtrl();
        }

        /// <summary>
        ///     模型控制器
        /// </summary>
        /// <returns></returns>
        public override IModelCtrl ModelCtrl()
        {
            return new ModelCtrl();
        }

        /// <summary>
        ///     表单控制器
        /// </summary>
        /// <returns></returns>
        public override IFormCtrl FormCtrl()
        {
            return new FormCtrl();
        }

        /// <summary>
        ///     文件类型控制器
        /// </summary>
        /// <returns></returns>
        public override IFileType FileTypeCtrl()
        {
            return new FileType();
        }

        /// <summary>
        ///     制度库文件控制器
        /// </summary>
        /// <returns></returns>
        public override IRulesFileCtrl RulesFileCtrl()
        {
            return new RulesFileCtrl();
        }
    }
}
