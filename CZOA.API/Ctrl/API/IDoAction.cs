namespace CZOA.Ctrl.API
{
    public interface IDoAction
    {
        //----节点办理行为(非连线操作)----
        /// <summary>
        ///     开启事项
        /// </summary>
        void BeginWork();

        /// <summary>
        ///     续接开启事项
        /// </summary>
        void ContinueWork();

        /// <summary>
        ///     自接新流
        /// </summary>
        void SelfBeginFlow();

        /// <summary>
        ///     下步接流
        /// </summary>
        void NextBeginFlow();

        /// <summary>
        ///     暂存
        /// </summary>
        void QuickSave();

        /// <summary>
        ///     开启协办分支:会签
        /// </summary>
        void AssistBranch();

        /// <summary>
        ///     开启并行分支:并行
        /// </summary>
        void ParallelBranch();

        #region 取消：修订、补签

        ///// <summary>
        /////     开启修订
        ///// </summary>
        //void ReviseChange();

        ///// <summary>
        /////     开启补签
        ///// </summary>
        //void SupplementChange();

        #endregion

        /// <summary>
        ///     发起改签
        /// </summary>
        void BeginChange();

        /// <summary>
        ///     开启
        /// </summary>
        void Revoke();

        /// <summary>
        ///     开启回退
        /// </summary>
        void GoBack();

        /// <summary>
        ///     办理完成：转下步、并发下步、自动完结下步 | 手动完结、改签完成
        /// </summary>
        void Done();

        #region 待议：改签完成、手动完结、自动完结

        ///// <summary>
        /////     改签完成 : 对应改签，修订或补签节点办理完成自动返回（创建新节点）发起改签的步骤(是步骤不是节点)
        ///// </summary>
        //void ChangeDone();
        //void ManualEnding();
        //void AutoEnding();

        #endregion
    }
}
