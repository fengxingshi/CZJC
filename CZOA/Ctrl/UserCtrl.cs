using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using CZOA.Ctrl.API;
using CZOA.DB;
using CZOA.Tag;
using SN.Utility;

namespace CZOA.Ctrl
{
    internal class UserCtrl : IUserCtrl
    {
        /// <summary>
        ///     获取用户Session
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SNSession GetUserSession(decimal userId)
        {
            using (var ctx = new OAContext())
            {
                var session = new SNSession();

                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.Connection.Open();

                //获取单位、处室、代理用户
                cmd.CommandText = string.Format(@"
select u.user_id,u.user_name,u.delegate_user_id,d.dept_id,d.dept_name,o.org_id,o.org_name from t_user_domain ud left join t_user u on ud.user_id=u.user_id left join t_dept d on ud.domain_type_ti={0} and ud.domain_at=d.dept_id left join t_org o on d.org_id=o.org_id
where ud.domain_type_ti={0} and ud.user_id={1}", UserTag.Domain.处室域, userId);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    session.UserId = Convert.ToDecimal(reader[0]); //UserName = Convert.ToString(reader[1]),
                    session.DelegateUserId = reader[2] is DBNull ? (decimal?)null : Convert.ToDecimal(reader[2]);
                    session.DeptId = Convert.ToDecimal(reader[3]); //DeptName = Convert.ToString(reader[4]),
                    session.OrgId = Convert.ToDecimal(reader[5]); //OrgName = Convert.ToString(reader[6])
                }
                reader.Close();

                // 获取角色列表
                cmd.CommandText = string.Format(@"
select ud.user_id,r.role_id,r.role_name from t_user_domain ud left join t_role r on ud.domain_type_ti={0} and ud.domain_at=r.role_id
where ud.domain_type_ti={0} and ud.user_id={1}", UserTag.Domain.角色域, userId);

                reader = cmd.ExecuteReader();
                var ls = new List<decimal>();
                while (reader.Read())
                {
                    ls.Add(Convert.ToDecimal(reader[1]));
                }
                session.RoleIds = ls.ToArray<decimal>();
                reader.Close();

                return session;
            }
        }

        /// <summary>
        ///     获取用户所属处室（包括上溯上级处室）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<T_DEPT> GetUserAllDepts(decimal userId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_DEPT.SqlQuery(@"
select distinct dc.* from t_dept d inner join t_dept dc on d.rs=1 and dc.rs=1 and substr(d.dept_code,1,length(dc.dept_code))=dc.dept_code
where d.dept_id in (select domain_at from t_user_domain where domain_type_ti=:p0 and user_id=:p1)
                    ", UserTag.Domain.处室域, userId);
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取用户所属处室
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public T_DEPT GetUserDept(decimal userId)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    from t in ctx.T_DEPT.Where(p => p.RS == 1)
                    join u in
                        ctx.T_USER_DOMAIN.Where(
                            p => p.RS == 1 && p.USER_ID == userId && p.DOMAIN_TYPE_TI == UserTag.Domain.处室域)
                        on t.DEPT_ID
                        equals u.DOMAIN_AT
                    select t;
                return re.SingleOrDefault();
            }
        }

        /// <summary>
        ///     获取用户的角色列表（包括每个角色的上溯上级角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<T_ROLE> GetUserAllRoles(decimal userId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_ROLE.SqlQuery(@"
select distinct rc.* from t_role r inner join t_role rc on r.rs=1 and rc.rs=1 and substr(r.role_code,1,length(rc.role_code))=rc.role_code
where r.role_id in (select domain_at from t_user_domain where domain_type_ti=:p0 and user_id=:p1)
                    ", UserTag.Domain.角色域, userId);
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取用户的角色（只取直接赋予的角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<T_ROLE> GetUserRoles(decimal userId)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    from r in ctx.T_ROLE.Where(p => p.RS == 1)
                    join u in
                        ctx.T_USER_DOMAIN.Where(
                            p => p.RS == 1 && p.USER_ID == userId && p.DOMAIN_TYPE_TI == UserTag.Domain.角色域)
                        on r.ROLE_ID
                        equals u.DOMAIN_AT
                    select r;
                return re.ToList();
            }
        }

        public DataTable GetUserDomainsByUserId(decimal userId)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    ctx.SqlTable(
                        string.Format(
                            "select d.*,r.role_name as domain_at_name from t_user_domain d,t_role r where d.rs=1 and d.domain_type_ti={0} and d.user_id={1} and r.role_id=d.domain_at",
                            UserTag.Domain.角色域, userId));
                return re;
            }
        }

        public IList<T_USER_DOMAIN> GetUserDomainsByDomainAt(decimal domainAt)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_USER_DOMAIN.Where(t => t.DOMAIN_AT == domainAt && t.RS == 1);
                return re.ToList();
            }
        }

        public IList<T_USER> GetUserByDomainAt(decimal domainAt)
        {
            using (var ctx = new OAContext())
            {
                var re = from user in ctx.T_USER.Where(t => t.RS == 1)
                    from d in ctx.T_USER_DOMAIN.Where(p => p.RS == 1 && p.DOMAIN_AT == domainAt)
                    where d.USER_ID == user.USER_ID
                    select user;
                return re.ToList();
            }
        }

        public dynamic GetUserAndUserDomain(decimal domainAt)
        {
            var tempUserDomains = GetUserDomainsByDomainAt(domainAt);
            var tempUser = GetUserByDomainAt(domainAt);
            var d = new
            {
                User = tempUser,
                UserDomains = tempUserDomains
            };
            return d;
        }

        public dynamic GetUserAndUserName(decimal domainAt)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.SqlDynamic(
                    string.Format(@"select u.user_id,
       u.user_name,
       u.user_code,
       u.user_password,
       u.user_status_ti,
       u.user_idx,
       case u.user_status_ti when 1 then '启用' else '停用' end  user_status_ti_name,
       u.user_type_ti,
       t2.item_name as user_type_ti_name,
       d.domain_type_ti,
       t.item_name as domain_type_ti_name,
       d.domain_at
       from T_user u, t_User_Domain d, t_tag_item t,t_tag_item t2
       where u.user_id = d.user_id
       and d.domain_at ={0} 
       and t.tag_item_id = d.domain_type_ti
       and t2.tag_item_id=u.user_type_ti
       order by u.user_idx ", domainAt));
                return re;
            }
        }

        public decimal AddUser(T_USER user)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_USER.Add(user);
                return ctx.SaveChanges();
            }
        }

        public decimal AddUserDomain(IList<T_USER_DOMAIN> userDomains)
        {
            using (var ctx = new OAContext())
            {
                foreach (var userDomain in userDomains)
                {
                    ctx.T_USER_DOMAIN.Add(userDomain);
                }
                return ctx.SaveChanges();
            }
        }

        public decimal AddUserWithDomain(T_USER user, IList<T_USER_DOMAIN> userDomains)
        {
            using (var scope = new TransactionScope())
            {
                var re = 0;
                using (var ctx = new OAContext())
                {
                    AddUser(user);
                    AddUserDomain(userDomains);
                    re = ctx.SaveChanges();
                }
                scope.Complete();
                return re;
            }
        }

        public decimal UpdateUser(T_USER user)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_USER.Attach(user);
                ctx.Entry<T_USER>(user).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateUserDomain(IList<T_USER_DOMAIN> userDomains)
        {
            using (var ctx = new OAContext())
            {
                foreach (var userDomain in userDomains)
                {
                    ctx.T_USER_DOMAIN.Attach(userDomain);
                    ctx.Entry<T_USER_DOMAIN>(userDomain).State = EntityState.Modified;
                }
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateUserWithUserDomain(T_USER user, IList<T_USER_DOMAIN> userDomains = null)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_USER.Attach(user);
                ctx.Entry<T_USER>(user).State = EntityState.Modified;
                if (userDomains != null)
                {
                    ctx.Database.ExecuteSqlCommand("delete T_USER_DOMAIN where USER_ID =:p0", user.USER_ID);
                    if (userDomains.Count > 0)
                    {
                        foreach (var item in userDomains)
                        {
                            ctx.T_USER_DOMAIN.Add(item);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public decimal SynchronousUser()
        {
            DataTable dt;
            var re = 0;

            using (var ctx = new OAContext())
            {
                dt =
                    ctx.SqlTable(
                        "select * from V_USER v where v.ts>nvl((select max(ts) from T_USER),to_timestamp('2000-1-1 0:00:00.001','YYYY-MM-DD HH24:MI:SS.FF3')) order by ts");
            }

            using (var ctx = new OAContext())
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow d in dt.Rows)
                    {
                        var userid = Math.Abs(SNHelper.GuidToLongID(Guid.Parse(d["user_id"].ToString())));
                        var sql =
                            string.Format(
                                @"insert into T_USER(user_id,User_Name,User_Code,USER_PASSWORD,User_Status_Ti,User_Idx,ts,rs,USER_TYPE_TI) values({0},'{1}','{2}','{3}',{4},{5},to_timestamp('{6}','YYYY-MM-DD HH24:MI:SS.FF3'),{7},{8})",
                                userid, d["user_name"], d["user_code"], d["user_password"], d["User_Status_Ti"],
                                d["user_idx"], d["ts"], d["rs"], d["USER_TYPE_TI"]);
                        try
                        {
                            re += ctx.Database.ExecuteSqlCommand(sql);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            return re;
        }

        public decimal SynchronousUserDomain()
        {
            DataTable dt;
            var re = 0;

            using (var ctx = new OAContext())
            {
                dt =
                    ctx.SqlTable(
                        "select * from V_USER_DOMAIN v where v.ts>nvl((select max(ts) from T_USER_DOMAIN),to_timestamp('2000-1-1 0:00:00.001','YYYY-MM-DD HH24:MI:SS.FF3')) order by ts");
            }

            using (var ctx = new OAContext())
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow d in dt.Rows)
                    {
                        var userid = Math.Abs(SNHelper.GuidToLongID(Guid.Parse(d["user_id"].ToString())));
                        var userDomainId = this.BuildID();
                        var sql =
                            string.Format(
                                @"insert into T_USER_DOMAIN(user_id,user_domain_id,domain_type_ti,domain_at,ts,rs) values({0},{1},{2},{3},to_timestamp('{4}','YYYY-MM-DD HH24:MI:SS.FF3'),{5})",
                                userid, userDomainId, d["domain_type_ti"], d["domain_at"],
                                d["ts"], d["rs"]);
                        re += ctx.Database.ExecuteSqlCommand(sql);
                    }
                }
            }
            return re;
        }
    }
}
