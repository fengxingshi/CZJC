using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;
using CZOA.Tag;

namespace CZOA.Ctrl
{
    internal class FlowCtrl : IFlowCtrl
    {
        public IList<T_FLOW> GetFlowByAuthUser(decimal userId)
        {
            // 新
            //--确定用户可用流程
            //--1.确定处室：所有处室可用（is null） + 用户处室（用户处室唯一）
            //--2.确定角色：所有角色可用（is null） + 用户角色（用户角色多个）

            #region 子查询方案，高性能

            //--确定用户的可用流程
            //select r.*
            //  from t_flow_auth r
            // where r.rs=1 and r.flow_id in
            //       (select t.flow_id
            //          from t_flow_auth t
            //         where (t.auth_scope_ti = 3011 and
            //               (t.auth_to is null or t.auth_to = (
            //                          select domain_at from t_user_domain where domain_type_ti=3011 and user_id=5344527867880563311
            //               ))) --确定处室，所有处室可用 + 用户处室(用户处室唯一) 8862120765255046916=办公室
            //        )
            //   and (r.auth_scope_ti = 3012 and
            //       (r.auth_to is null or
            //       r.auth_to in (select domain_at
            //                         from t_user_domain
            //                        where user_id = 5344527867880563311 --5344527867880563311=用户
            //                          and domain_type_ti = 3012))) --在确定处室后确定角色，所有角色可用 + 用户角色（用户角色多个）

            #endregion

            #region 全inner join方案，清晰易读 √

            //select distinct f.* from t_flow f
            //    inner join t_flow_auth d on f.flow_id=d.flow_id and d.rs=1
            //    inner join t_flow_auth r on d.flow_id=r.flow_id and d.auth_scope_ti=3011 and r.auth_scope_ti=3012 and r.rs=1 --角色和处室做交集
            //    inner join t_user_domain ud on (d.auth_to=ud.domain_at or d.auth_to is null) and ud.domain_type_ti=3011 and ud.rs=1 --限定一个用户处室
            //    inner join t_user_domain ur on (r.auth_to=ur.domain_at or r.auth_to is null) and ur.domain_type_ti=3012 and ur.rs=1 --限定多个用户角色
            //where f.rs=1 and ur.user_id=5344527867880563311

            #endregion

            using (var ctx = new OAContext())
            {
                //TODO:表设计已改变，需修改
                var re = ctx.T_FLOW.SqlQuery(@"
select distinct f.* from t_flow f
    inner join t_flow_auth d on f.flow_id=d.flow_id and d.rs=1
    inner join t_flow_auth r on d.flow_id=r.flow_id and d.auth_scope_ti=:处室范围 and r.auth_scope_ti=:角色范围 and r.rs=1 --处室和角色做交集
    inner join t_user_domain ud on (d.auth_to=ud.domain_at or d.auth_to and ud.domain_type_ti=:处室范围 and ud.rs=1 --限定一个用户处室
    inner join t_user_domain ur on (r.auth_to=ur.domain_at or r.auth_to is null) and ur.domain_type_ti=:角色范围 and ur.rs=1 --限定多个用户角色
where f.rs=1 and ur.user_id=:授权的用户
order by f.flow_idx",
                    UserTag.Domain.处室域, UserTag.Domain.角色域,
                    UserTag.Domain.处室域, UserTag.Domain.角色域,
                    userId);
                return re.ToList();
            }
        }

        public IList<T_FLOW> GetFlowByDepartmentAndOrgId(decimal departmentId, decimal orgId, List<decimal> roleList)
        {
            using (var ctx = new OAContext())
            {
                var str = string.Empty;
                if (roleList.Count == 0)
                {
                    str += "null";
                }
                else
                {
                    foreach (var role in roleList)
                    {
                        str += role + ",";
                    }
                }
                str = str.Trim(',');
                var re =
                    ctx.T_FLOW.SqlQuery(string.Format(
                        @"select t.* from T_FLOW t,t_Flow_Auth f where t.flow_id=f.flow_id and ((f.auth_scope_ti=" +
                        FlowTag.FlowAuth.AuthDept + " and f.auth_to=" + departmentId + ") or (f.auth_scope_ti=" +
                        FlowTag.FlowAuth.AllDept + ")) and t.org_id=" + orgId + " and t.outer_org_flow=" +
                        FlowTag.OuterOrgFlow.False +
                        "and t.enabled=1 and f.flow_id in (select distinct(flow_id) from t_flow_auth f1 where (f1.auth_scope_ti =" +
                        FlowTag.FlowAuth.AuthRole +
                        " and f1.auth_to in (select distinct(r.role_id) from T_ROLE r start with r.role_id in ({0}) connect by prior r.super_role_id=r.role_id)) " +
                        "or (f1.auth_scope_ti =" + FlowTag.FlowAuth.AllRole + "))", str));
                return re.ToList();
            }
        }

        public IList<dynamic> GetFlowStepAuths(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    ctx.SqlDynamic(string.Format(@"
select sa.*, (r.role_name || d.dept_name || o.org_name) as auth_to_name
  from t_step_auth sa
 inner join t_step s
    on sa.step_id = s.step_id
  left join t_role r
    on sa.auth_to = {1}
   and sa.auth_to = r.role_id
  left join t_dept d
    on sa.auth_to = {2}
   and sa.auth_to = d.dept_id
  left join t_org o
    on sa.auth_to = {3}
   and sa.auth_to = o.org_id
 where s.rs = 1
   and s.flow_id = {0}
", flowId, Tag.StepTag.StepAuth.AuthRole, Tag.StepTag.StepAuth.AuthDept, Tag.StepTag.StepAuth.AuthOrg));
                return re;
            }
        }

        public IList<T_FLOW> GetFlows()
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_FLOW.Where(t => t.RS == 1 && t.ENABLED == FlowTag.Enabled.True);
                return re.ToList();
            }
        }

        public dynamic GetAllFlow()
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_FLOW.Where(p => p.RS == 1).Select(c => new
                {
                    c.FLOW_IDX,
                    c.FLOW_NAME,
                    WORK_CATEGORY_TI_NAME =
                        ctx.T_TAG_ITEM.FirstOrDefault(q => q.TAG_ITEM_ID == c.WORK_CATEGORY_TI).ITEM_NAME,
                    FLOW_CATEGORY_TI_NAME =
                        ctx.T_TAG_ITEM.FirstOrDefault(q => q.TAG_ITEM_ID == c.FLOW_CATEGORY_TI).ITEM_NAME,
                    c.ENABLED,
                    c.FLOW_CATEGORY_TI,
                    c.FLOW_ID,
                    c.FLOW_MAP,
                    c.OUTER_ORG_FLOW,
                    c.ORG_ID,
                    c.WORK_CATEGORY_TI,
                }).OrderBy(t => t.FLOW_IDX);
                //var re = ctx.Database.SqlQuery<T_FLOW>(@"select f.*,i.item_name as FLOW_CATEGORY_TI_NAME from T_flow f inner join t_tag_item i on i.tag_item_id=f.flow_category_ti");
                return re.ToList();
            }
        }

        public T_FLOW GetFlow(decimal flowID)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_FLOW.SingleOrDefault(p => p.FLOW_ID == flowID && p.RS == 1);
                return re;
            }
        }

        public IList<T_FLOW_AUTH> GetFlowAuth(decimal flowID)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_FLOW_AUTH.Where(p => p.FLOW_ID == flowID && p.RS == 1);
                return re.ToList();
            }
        }

        public IList<dynamic> GetFlowAuths(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                var x = new
                {
                    flow_id = 1m,
                    auth_to = 1m,
                    auth_to_name = ""
                };
                var t = x.GetType();
                var re = ctx.SqlDynamic(string.Format(@"
select fa.*,nvl(o.org_name,'') || nvl(d.dept_name,'') || nvl(r.role_name,'') as auth_name from t_flow_auth fa left join t_org o on fa.auth_to = o.org_id 
left join t_dept d on fa.auth_to = d.dept_id
left join t_role r on fa.auth_to = r.role_id
where fa.flow_id={0}", flowId));
                return re;
            }
        }

        public decimal AddFlow(T_FLOW flow, IList<T_FLOW_AUTH> auth)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_FLOW.Add(flow);
                foreach (var item in auth)
                {
                    ctx.T_FLOW_AUTH.Add(item);
                }
                return ctx.SaveChanges();
            }
        }

        public decimal UpdateFlow(T_FLOW flow, IList<T_FLOW_AUTH> auth = null)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_FLOW.Attach(flow);
                ctx.Entry<T_FLOW>(flow).State = EntityState.Modified;
                if (auth != null)
                {
                    ctx.Database.ExecuteSqlCommand("delete T_FLOW_AUTH where flow_id=:p0", flow.FLOW_ID);
                    if (auth.Count > 0)
                    {
                        foreach (var item in auth)
                        {
                            ctx.T_FLOW_AUTH.Add(item);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public decimal DeleteFlow(decimal flowID)
        {
            using (var ctx = new OAContext())
            {
                var re = 0;
                re += ctx.Database.ExecuteSqlCommand("update T_FLOW set rs=0 where flow_id=:p", flowID);
                re += ctx.Database.ExecuteSqlCommand("update T_FLOW_AUTH set rs=0 where flow_id=:p", flowID);
                return re;
            }
        }

        public IList<T_TAG_ITEM> GetFlowCategoryTI()
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_TAG_ITEM.Where(p => p.RS == 1 && p.TAG_ID == FlowTag.FlowCategory._TagID);
                //.ToDictionary(p => p.TAG_ITEM_ID, p => p.ITEM_NAME);
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取流程可用模型 T_MODEL
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public IList<T_MODEL> GetFlowModels(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                // 获取所有流程所在单位可用的文笺（外部流程只能取所有单位可见的文笺），以及所有单位都可见的文笺
                var re = ctx.T_MODEL.SqlQuery(
                    @"select * from t_model where rs=1 and org_id = (select org_id from t_flow where flow_id=:p0) or org_id is null",
                    flowId);
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取流程可用输入项 T_MODEL_ITEM
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public IList<T_MODEL_ITEM> GetFlowInputs(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                // 先获取可用流程，在获取流程的输入项
                var re = ctx.T_MODEL_ITEM.SqlQuery(@"select * from t_model_item
where rs=1 and model_id in (select model_id from t_model where org_id = (select org_id from t_flow where flow_id=:p0) or org_id is null)",
                    flowId);
                return re.ToList();
            }
        }
    }
}
