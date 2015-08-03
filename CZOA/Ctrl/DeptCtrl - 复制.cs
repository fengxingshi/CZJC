using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;
using SN.Utility;

namespace CZOA.Ctrl
{
    internal class DeptCtrl : IDeptCtrl
    {
        public IList<T_DEPT> GetAllDeptByOrgID(decimal orgID)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_DEPT.Where(p => p.ORG_ID == orgID && p.RS == 1);
                return re.ToList();
            }
        }

        public T_DEPT GetDept(decimal deptId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_DEPT.SingleOrDefault(t => t.DEPT_ID == deptId && t.RS == 1);
                return re;
            }
        }

        public decimal AddDept(T_DEPT dept)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_DEPT.Add(dept);
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateDept(T_DEPT dept)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_DEPT.Attach(dept);
                ctx.Entry<T_DEPT>(dept).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public decimal DeleteDept(decimal deptId)
        {
            using (var ctx = new OAContext())
            {
                return ctx.Database.ExecuteSqlCommand("update T_DEPT set rs=0 where DEPT_ID=:p", deptId);
            }
        }

        public decimal SynchronousDept()
        {
            DataTable dt;
            var re = 0;

            using (var ctx = new OAContext())
            {
                dt =
                    ctx.SqlTable(
                        "select * from V_DEPT v where v.ts>nvl((select max(ts) from T_DEPT),to_timestamp('2000-1-1 0:00:00.001','YYYY-MM-DD HH24:MI:SS.FF3')) order by ts");
            }

            using (var ctx = new OAContext())
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow d in dt.Rows)
                    {
                        var org = Math.Abs(SNHelper.GuidToLongID(Guid.Parse(d["org_id"].ToString())));
                        string superDeptId = "null";
                        if (d["super_dept_id"]!=DBNull.Value)
                        {
                            superDeptId = d["super_dept_id"].ToString();
                        }
                        var sql =
                            string.Format(
                                @"insert into t_dept(dept_id,super_dept_id,dept_name,dept_code,org_id,dept_idx,ts,rs) values({0},{1},'{2}',{3},{4},{5},to_timestamp('{6}','YYYY-MM-DD HH24:MI:SS.FF3'),{7})",
                                d["dept_id"], superDeptId, d["dept_name"], d["dept_code"], org,
                                d["dept_idx"], d["ts"], d["rs"]);
                        re += ctx.Database.ExecuteSqlCommand(sql);
                    }
                }
            }
            return re;
        }
    }
}
