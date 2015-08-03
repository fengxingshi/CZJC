using System.Collections;
using System.Collections.Generic;
using System.Data;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
    public interface IRulesFileCtrl
    {
        decimal AddRulesFile(C_RULES_FILE rulesFile);

        decimal UpdataRulesFile(C_RULES_FILE rulesFile);

        DataTable GetRulesFile(long departmentid);

        DataTable GetTypeRulesFile(decimal orgId, decimal departmentId, decimal userId, decimal type, decimal sccope = 0);

        decimal DeleteRulesFile(decimal rulesFileId);

        decimal UpdataFileState(decimal rulesFileId, int state, decimal userId);
    }
}
