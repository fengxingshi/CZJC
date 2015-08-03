using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;
using SN.Utility;

namespace CZOA.Ctrl
{
    internal class OrgCtrl : IOrgCtrl
    {
        public IList<T_ORG> GetAll()
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_ORG.Where(p => p.RS == 1);
                return re.ToList();
            }
        }

        public T_ORG GetOrg(decimal orgId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_ORG.SingleOrDefault(t => t.ORG_ID == orgId && t.RS == 1);
                return re;
            }
        }

        public decimal AddOrg(T_ORG org)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_ORG.Add(org);
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateOrg(T_ORG org)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_ORG.Attach(org);
                ctx.Entry<T_ORG>(org).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public decimal DeleteOrg(decimal orgId)
        {
            using (var ctx = new OAContext())
            {
                return ctx.Database.ExecuteSqlCommand("update T_ORG set rs=0 where ORG_ID=:p", orgId);
            }
        }

        public decimal SynchronousOrg()
        {
            DataTable dt;
            var re = 0;

            using (var ctx = new OAContext())
            {
                dt = ctx.SqlTable("select * from V_ORG v where v.ts>nvl((select max(ts) from T_org),to_timestamp('2000-1-1 0:00:00.001','YYYY-MM-DD HH24:MI:SS.FF3')) order by ts");
            }

            using (var ctx = new OAContext())
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow d in dt.Rows)
                    {
                        var org = Math.Abs(SNHelper.GuidToLongID(Guid.Parse(d["org_id"].ToString())));
                        string superorg = "null";
                        if (d["super_org_id"] != DBNull.Value)
                        {
                            superorg =
                                Math.Abs(SNHelper.GuidToLongID(Guid.Parse(d["super_org_id"].ToString()))).ToString();
                        }
                        var sql = string.Format(
                            @"insert into t_org(org_id, org_name, org_type_ti, SUPER_ORG_ID,org_idx, ts, rs, org_code) values ({0},'{1}',{2},{3},{4},to_timestamp('{5}','YYYY-MM-DD HH24:MI:SS.FF3'),{6},{7})",
                            org, d["org_name"], d["org_type_ti"], superorg, d["org_idx"], d["ts"].ToString(), d["rs"],
                            d["org_code"]);

                        re += ctx.Database.ExecuteSqlCommand(sql);
                    }
                }
            }
            return re;
        }
    }
}
