using System;
using System.Data;
using CZOA.Ctrl.API;
using CZOA.DB;
using CZOA.Msg;

namespace CZOA.Ctrl
{
    internal class WorkCtrl : IWorkCtrl
    {
        /// <summary>
        ///     校验work的数据完整性
        /// </summary>
        /// <param name="work"></param>
        private void Validate(T_WORK work)
        {
            work.WORK_NAME.IsNullEmptyZero().TrueThrow(WorkMsg.Error_WorkNameNull);
            work.WORK_CATEGORY_TI.IsNullEmptyZero().TrueThrow(WorkMsg.Error_WorkCategoryNull);
            work.WORK_LEVEL_TI.IsNullEmptyZero().TrueThrow(WorkMsg.Error_WorkLevelNull);
            work.WORK_TYPE_TI.IsNullEmptyZero().TrueThrow(WorkMsg.Error_WorkTypeNull);
            work.WORK_SOURCE_TI.IsNullEmptyZero().TrueThrow(WorkMsg.Error_WorkSourceNull);
            work.HOST_DEPT.IsNullEmptyZero().TrueThrow(WorkMsg.Error_WorkHostDeptNull);
        }

        public T_WORK CreateWork(decimal theCreateWorkNodeId, decimal orgId)
        {
            var work = new T_WORK
            {
                WORK_ID = theCreateWorkNodeId,
                ORG_ID = orgId,
                WORK_NAME = null, //TODO：创建时可空，保存时不可空
                WORK_CATEGORY_TI = 0, //TODO：必须设置
                WORK_TYPE_TI = 0, //TODO：UI选择，办件、阅件
                WORK_LEVEL_TI = 0, //TODO：UI选择，紧急等级
                WORK_DO_STATUS_TI = 0, //TODO：此处默认 办理中
                WORK_NO = null, //自动生成，传任意值或不传值不影响自动生成值
                WORK_SOURCE_TI = 0, //TODO：UI选择
                HOST_DEPT = 0, //TODO：UI选择，默认当前处室
                SUPERVISION_TYPE_TI = 0, //TODO：UI选择，默认不督办
                SUPERVISION_START_TIME = DateTime.Now, //TODO：UI操作
                SUPERVISION_FINISH_TIME = null //TODO：UI操作
            };
            return work;
        }

        public decimal SaveWork(T_WORK work)
        {
            Validate(work);

            using (var ctx = new OAContext())
            {
                if (work.RS == null)
                {
                    ctx.T_WORK.Add(work);
                }
                else
                {
                    ctx.T_WORK.Attach(work);
                    ctx.Entry<T_WORK>(work).State = EntityState.Modified;
                }
                return ctx.SaveChanges();
            }
        }
    }
}
