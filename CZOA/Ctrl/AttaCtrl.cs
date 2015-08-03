using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.Ctrl
{
    internal class AttaCtrl
    {
        public T_ATTA CreateAtta(decimal entityId, string fileFullName)
        {
            var atta = new T_ATTA
            {
                ATTA_ID = this.BuildID(),
                ATTA_FILE_NAME = fileFullName,
                ATTA_FILE_TYPE_TI = -1,
                ATTA_RECORD_ID = -1,
                ENTITY_ID = entityId,
                ATTA_IDX = 100
            };
            return atta;
        }
    }
}
