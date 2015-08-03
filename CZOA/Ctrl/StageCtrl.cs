using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl
{
    internal class StageCtrl : IStageCtrl
    {
        public T_STAGE CreateStage(decimal theCreateStageNodeId, decimal flowId, string flowName)
        {
            var stage = new T_STAGE
            {
                FLOW_ID = flowId,
                STAGE_FLOW_NAME = flowName,
                STAGE_ID = theCreateStageNodeId
            };
            return stage;
        }

        public decimal SaveStage(T_STAGE stage)
        {
            using (var ctx = new OAContext())
            {
                if (stage.RS == null)
                {
                    ctx.T_STAGE.Add(stage);
                }
                else
                {
                    ctx.T_STAGE.Attach(stage);
                    ctx.Entry<T_STAGE>(stage).State = EntityState.Modified;
                }
                return ctx.SaveChanges();
            }
        }
    }
}
