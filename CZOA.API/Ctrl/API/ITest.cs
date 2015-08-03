using System.Collections.Generic;
using CZJC.DB;

namespace CZJC.Ctrl.API
{
    public interface ITestCtrl
    {
        IList<TEST> GetTest(decimal? id = null);
        TEST GetTest(decimal id);
        decimal AddTest(TEST test);
        decimal AddTests(IList<TEST> list);
        decimal UpdateTest(TEST test);

        decimal DeleteTest(decimal id);
        System.Data.DataTable GetTestTable();
        IList<dynamic> GetTestDynamic();
    }
}
