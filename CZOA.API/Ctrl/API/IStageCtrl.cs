using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IStageCtrl
    {
        T_STAGE CreateStage(decimal theCreateStageNodeId, decimal flowId, string flowName);
        decimal SaveStage(T_STAGE stage);
    }
}
