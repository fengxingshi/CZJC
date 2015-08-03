using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.Ctrl
{
    internal class RoleCtrl : IRoleCtrl
    {
        public IList<T_ROLE> GetAllRoleByOrgId(decimal orgId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_ROLE.Where(p => p.ORG_ID == orgId && p.RS == 1);
                return re.ToList();
            }
        }

        public T_ROLE GetRole(decimal roleId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_ROLE.SingleOrDefault(t => t.ROLE_ID == roleId && t.RS == 1);
                return re;
            }
        }

        public decimal AddRole(T_ROLE role)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_ROLE.Add(role);
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateRole(T_ROLE role)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_ROLE.Attach(role);
                ctx.Entry<T_ROLE>(role).State = EntityState.Modified;
                return ctx.SaveChanges();
            }
        }

        public decimal DeleteRole(decimal roleId)
        {
            using (var ctx = new OAContext())
            {
                return ctx.Database.ExecuteSqlCommand("update T_ROLE set rs=0 where ROLE_ID=:p", roleId);
            }
        }
    }
}
