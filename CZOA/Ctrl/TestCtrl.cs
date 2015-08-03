using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl
{
    internal class TestCtrl:API.ITestCtrl
    {
        public class REST
        {
            public string NAME { get; set; }
            public decimal AGE { get; set; }
        }
        public System.Data.DataTable GetTestTable()
        {
            using (var ctx = new CZOA.DB.OAContext())
            {
                var re = ctx.SqlTable(@"select NAME,ID from test");
                return re;
            }
        }

        public IList<dynamic> GetTestDynamic()
        {
            using (var ctx = new CZOA.DB.OAContext())
            {
                var re = ctx.SqlDynamic(@"select NAME,ID from test");
                return re;
            }
        }
        public IList<DB.TEST> GetTest(decimal? id = null)
        {
            using (var ctx = new CZOA.DB.OAContext())
            {
                var re = id == null ? ctx.TEST.Where(p => p.ID > 0) : ctx.TEST.Where(p => p.ID == id);
                return re.ToList();
            }
        }

        public DB.TEST GetTest(decimal id)
        {
            using (var ctx = new CZOA.DB.OAContext())
            {
                var ls = ctx.TEST.Where(p => p.ID == id);

                if (ls.Any())
                {
                    return ls.ToList()[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal AddTest(DB.TEST test)
        {
            using (var ctx = new CZOA.DB.OAContext())
            {
                ctx.TEST.Add(test);

                return ctx.SaveChanges();
            }
        }

        public decimal AddTests(IList<TEST> list)
        {
            using (var ctx = new CZOA.DB.OAContext())
            {
                foreach (var t in list)
                {
                    ctx.TEST.Add(t);
                }
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateTest(DB.TEST test)
        {
            if (!test.ID.IsNullEmptyZero())
            {
                using (var ctx = new DB.OAContext())
                {
                    ctx.TEST.Attach(test);
                    ctx.Entry<DB.TEST>(test).State = EntityState.Modified;
                    return ctx.SaveChanges();
                }
            }
            return 0;
        }

        public decimal DeleteTest(decimal id)
        {
            if (!id.IsNullEmptyZero())
            {
                using (var ctx = new DB.OAContext())
                {
                    return ctx.Database.ExecuteSqlCommand("update TEST set RS=0 where ID=:p0", id);
                }
            }
            return 0;
        }
    }
}
