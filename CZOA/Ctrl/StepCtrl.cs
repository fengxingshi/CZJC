using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;
using CZOA.Tag;

namespace CZOA.Ctrl
{
    /* EF性能提升:ctx.TABLE.AsNoTracking()不需在EF缓存追踪 */

    internal class StepCtrl : IStepCtrl
    {
        #region old

        //public IList<T_STEP> GetEnterInStepByFlow(decimal flowId)
        //{
        //    using (var ctx = new OAContext())
        //    {
        //        var re =
        //            from step in ctx.T_STEP
        //            where step.RS == 1 && step.ENTER_IN_STEP == 1 && step.FLOW_ID == flowId
        //            select step;
        //        return re.ToList();
        //    }
        //}

        //public virtual IList<decimal> GetStepType(decimal stepId)
        //{
        //    using (var ctx = new OAContext())
        //    {
        //        var re =
        //            ctx.T_STEP_TAG.AsNoTracking() //不在EF缓存
        //                .Where(p => p.RS == 1 && p.STEP_ID == stepId && p.TAG_ID == StepTag.StepType._TagID)
        //                .Select(p => p.STEP_TAG_ID);
        //        return re.ToList();
        //    }
        //}

        //public virtual IList<T_TAG_ITEM> GetStepType(T_STEP step)
        //{
        //    using (var ctx = new OAContext())
        //    {
        //        var re = //use inner join
        //            from it in ctx.T_TAG_ITEM.Where(p => p.RS == 1)
        //            join st in ctx.T_STEP_TAG.Where(p => p.RS == 1) on it.TAG_ITEM_ID equals st.TAG_ITEM_ID
        //            where st.STEP_ID == step.STEP_ID
        //            select it;
        //        //var x = ctx.T_TAG_ITEM.Join(ctx.T_STEP_TAG, it => it.TAG_ITEM_ID, st => st.TAG_ITEM_ID, (it, st) => it);
        //        return re.ToList();
        //    }
        //}

        //public virtual bool AllowEnterIn(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowAddForm(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowSelfBeginFlow(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowNextBeginFlow(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowBeginAssistBranch(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowBeginParallelBranch(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowBranchLineHeader(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowParallelMerge(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowManualEnding(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual bool AllowAutoEnding(decimal stepId)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        #region 流程步骤列表界面

        /// <summary>
        ///     获取流程的所有步骤
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public IList<T_STEP> GetStepByFlow(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_STEP.Where(p => p.FLOW_ID == flowId && p.RS == 1);
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取流程的所有步骤的下步骤信息，包括下步名称和步序
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public IList<dynamic> GetStepNextByFlow(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    ctx.SqlDynamic(
                        string.Format(@"select n.*,s.step_name next_step_name,s.step_no next_step_no from t_step_to_next n inner join t_step s on n.next_step_id=s.step_id
where s.rs=1 and s.flow_id={0}", flowId));
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取流程的相关步骤列表以及相关步骤的下步骤信息列表
        ///     return {Steps,StepNexts}
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public dynamic GetStepWithNextByFlow(decimal flowId)
        {
            using (var ctx = new OAContext())
            {
                var steps = GetStepByFlow(flowId);
                var stepNexts = GetStepNextByFlow(flowId);

                var re = new
                {
                    Steps = steps,
                    StepNexts = stepNexts
                };
                return re;
            }
        }

        #endregion

        #region 步骤设置界面

        /// <summary>
        ///     检查是否存在此id的步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public bool HasStep(decimal stepId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_STEP.Any(p => p.STEP_ID == stepId);
                return re;
            }
        }

        /// <summary>
        ///     获取指定步骤
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public T_STEP GetStep(decimal stepId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_STEP.SingleOrDefault(p => p.STEP_ID == stepId);
                return re;
            }
        }

        /// <summary>
        ///     获取步骤的下步骤备选信息（包括已选，标示出来）
        ///     step_id,step_name,step_no,selected
        /// </summary>
        public IList<dynamic> GetStepCanUseNextStepInfos(decimal flowId, decimal? stepId = null)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    ctx.SqlDynamic(string.Format(@"
select s.step_id,
       s.step_name,
       s.step_no,
       case
         when n.next_step_id is not null then
          1
         else
          0
       end SELECTED
  from t_step s
  left join t_step_to_next n
    on s.step_id = n.next_step_id
   and n.step_id = {0}
 where s.rs=1 and s.flow_id = {1}", stepId ?? 0, flowId));
                return re;
            }
        }

        /// <summary>
        ///     获取步骤的备选文笺模型
        ///     model_id,model_name,model_type_ti,model_idx,selected
        /// </summary>
        public IList<dynamic> GetStepCanUseModelInfos(decimal orgId, decimal? stepId = null)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.SqlDynamic(string.Format(@"
select m.model_id,
       m.model_name,
       m.model_type_ti,
       m.model_idx,
       case
         when sm.model_id is not null then
          1
         else
          0
       end selected
  from t_model m
  left join t_step_model sm
    on m.model_id = sm.model_id
   and sm.step_id = {0}
 where m.org_id = {1}", stepId ?? 0, orgId));
                return re;
            }
        }

        /// <summary>
        ///     获取步骤的备选输入项
        ///     model_id,model_name,model_item_id,item_name,selected
        /// </summary>
        public IList<dynamic> GetStepCanUseModelItemInfos(decimal orgId, decimal? stepId = null)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.SqlDynamic(string.Format(@"
select m.model_id,
       m.model_name,
       mi.model_item_id,
       mi.item_name,
       case
         when smi.model_item_id is not null then
          1
         else
          0
       end selected
  from t_model_item mi
 inner join t_model m
    on mi.model_id = m.model_id
  left join t_step_model_item smi
    on mi.model_item_id = smi.model_item_id
   and smi.step_id = {0}
 where m.org_id = {1}", stepId ?? 0, orgId));
                return re;
            }
        }

        /// <summary>
        ///     获取步骤已选文笺列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<T_STEP_MODEL> GetStepModels(decimal stepId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_STEP_MODEL.Where(p =>p.RS ==1 && p.STEP_ID == stepId);
                return re.ToList();
            }
        }

        /// <summary>
        ///     获取步骤已选输入项列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<T_STEP_MODEL_ITEM> GetStepModelItems(decimal stepId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_STEP_MODEL_ITEM.Where(p =>p.RS ==1 && p.STEP_ID == stepId);
                return re.ToList();
            }
        }
        
        /// <summary>
        /// 获取步骤已有授权列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<T_STEP_AUTH> GetStepAuths(decimal stepId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_STEP_AUTH.Where(p => p.RS == 1 && p.STEP_ID == stepId);
                return re.ToList();
            }
        }

        /// <summary>
        /// 获取步骤已有授权列表，带授权项名称
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<dynamic> GetStepAuthsWithName(decimal stepId)
        {
            using (var ctx = new OAContext())
            {
                var re =
                    ctx.SqlDynamic(
                        string.Format(@"select sa.*, (r.role_name || d.dept_name || o.org_name) as auth_to_name
  from t_step_auth sa
  left join t_role r
    on sa.auth_scope_ti = {1}
   and sa.auth_to = r.role_id
  left join t_dept d
    on sa.auth_scope_ti = {2}
   and sa.auth_to = d.dept_id
  left join t_org o
    on sa.auth_scope_ti = {3}
   and sa.auth_to = o.org_id
 where sa.rs = 1
   and sa.step_id = {0}", stepId, Tag.StepTag.StepAuth.AuthRole, Tag.StepTag.StepAuth.AuthDept,
                            Tag.StepTag.StepAuth.AuthOrg));
                return re;
            }
        }
        #endregion

        #region 保存步骤

        /// <summary>
        ///     保存新建步骤及其附属信息
        /// </summary>
        /// <param name="step"></param>
        /// <param name="nextSteps"></param>
        /// <param name="models"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public decimal SaveNewStep(T_STEP step, IList<T_STEP_TO_NEXT> nextSteps, IList<T_STEP_MODEL> models,
            IList<T_STEP_MODEL_ITEM> inputs)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_STEP.Add(step);
                foreach (var s in nextSteps)
                {
                    ctx.T_STEP_TO_NEXT.Add(s);
                }
                foreach (var m in models)
                {
                    ctx.T_STEP_MODEL.Add(m);
                }
                foreach (var i in inputs)
                {
                    ctx.T_STEP_MODEL_ITEM.Add(i);
                }
                return ctx.SaveChanges();
            }
        }

        /// <summary>
        ///     保存编辑步骤及其附属信息
        /// </summary>
        /// <param name="step"></param>
        /// <param name="nextSteps"></param>
        /// <param name="models"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public decimal SaveEditStep(T_STEP step, IList<T_STEP_TO_NEXT> nextSteps, IList<T_STEP_MODEL> models,
            IList<T_STEP_MODEL_ITEM> inputs)
        {
            using (var ctx = new OAContext())
            {
                ctx.T_STEP.Attach(step);
                ctx.Entry<T_STEP>(step).State = EntityState.Modified;
                // 先清除附属信息
                foreach (var m in ctx.T_STEP_TO_NEXT.Where(p => p.STEP_ID == step.STEP_ID))
                {
                    ctx.T_STEP_TO_NEXT.Remove(m);
                }
                foreach (var m in ctx.T_STEP_MODEL.Where(p => p.STEP_ID == step.STEP_ID))
                {
                    ctx.T_STEP_MODEL.Remove(m);
                }
                foreach (var m in ctx.T_STEP_MODEL_ITEM.Where(p => p.STEP_ID == step.STEP_ID))
                {
                    ctx.T_STEP_MODEL_ITEM.Remove(m);
                }
                // 重新添加附属信息
                foreach (var m in nextSteps)
                {
                    ctx.T_STEP_TO_NEXT.Add(m);
                }
                foreach (var m in models)
                {
                    ctx.T_STEP_MODEL.Add(m);
                }
                foreach (var m in inputs)
                {
                    ctx.T_STEP_MODEL_ITEM.Add(m);
                }
                return ctx.SaveChanges();
            }
        }

        /// <summary>
        ///     大保存
        /// </summary>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public decimal SaveFlowSteps(IList<T_STEP> steps, IDictionary<decimal, decimal> stepsStatus,
            dynamic stepsData, byte[] mapXml)
        {
            //var toServer = new
            //{
            //    Steps = _steps,//IList<T_STEP>
            //    StepsStatus = _stepStatus,//IDic<decimal,decimal>
            //    StepsData = lastSaveData,//IDic<decimal,dynamic>  ;  dynamic = {NextSteps,Models,Inputs} = {List<decimal>?,List<decimal>?,List<decimal>?}
            //    MapXml = xml//string
            //};
            using (var ctx = new OAContext())
            {
                var re = 0;

                #region 更新流程图

                var flowid = steps[0].FLOW_ID;
                var flow = ctx.T_FLOW.SingleOrDefault(p => p.FLOW_ID.Equals(flowid));
                if (flow != null)
                {
                    flow.FLOW_MAP = mapXml;
                }

                #endregion

                foreach (var st in stepsStatus)
                {
                    #region 新增

                    if (st.Value == 0)
                    {
                        // 添加步骤
                        var step = steps.SingleOrDefault(p => p.STEP_ID == st.Key);
                        ctx.T_STEP.Add(step);
                        // 添加下步
                        var nextsteps = stepsData[st.Key.ToString()].NextSteps;
                        if (nextsteps != null)
                        {
                            foreach (var n in nextsteps)
                            {
                                ctx.T_STEP_TO_NEXT.Add(new T_STEP_TO_NEXT
                                {
                                    STEP_ID = st.Key,
                                    STEP_TO_NEXT_ID = this.BuildID(),
                                    NEXT_STEP_ID = n
                                });
                            }
                        }
                        // 添加文笺
                        var models = stepsData[st.Key.ToString()].Models;
                        if (models != null)
                        {
                            foreach (var n in models)
                            {
                                ctx.T_STEP_MODEL.Add(new T_STEP_MODEL
                                {
                                    STEP_ID = st.Key,
                                    STEP_MODEL_ID = this.BuildID(),
                                    MODEL_ID = n,
                                    CREATE_MODE_TI = ModelTag.CreateMode.ManualCreate //默认都手动创建
                                });
                            }
                        }
                        // 添加输入项
                        var inputs = stepsData[st.Key.ToString()].Inputs;
                        if (inputs != null) {
                            foreach (var n in inputs)
                            {
                                ctx.T_STEP_MODEL_ITEM.Add(new T_STEP_MODEL_ITEM
                                {
                                    STEP_ID = st.Key,
                                    STEP_MODEL_ITEM_ID = this.BuildID(),
                                    MODEL_ITEM_ID = n
                                });
                            }
                        }
                    }
                    #endregion
                    #region 更新

                    else if (st.Value == 1)
                    {
                        // 更新步骤
                        var step = steps.SingleOrDefault(p => p.STEP_ID == st.Key);
                        ctx.T_STEP.Attach(step);
                        ctx.Entry<T_STEP>(step).State = EntityState.Modified;
                        // 更新下步，先删再加
                        // 删
                        foreach (var n in ctx.T_STEP_TO_NEXT.Where(p => p.STEP_ID == st.Key))
                        {
                            ctx.T_STEP_TO_NEXT.Remove(n);
                        }
                        // 加
                        var nextsteps = stepsData[st.Key.ToString()].NextSteps;
                        if (nextsteps != null)
                        {
                            foreach (var n in nextsteps)
                            {
                                ctx.T_STEP_TO_NEXT.Add(new T_STEP_TO_NEXT
                                {
                                    STEP_ID = st.Key,
                                    STEP_TO_NEXT_ID = this.BuildID(),
                                    NEXT_STEP_ID = n
                                });
                            }
                        }
                        // 更新文笺，先删再加
                        var models = stepsData[st.Key.ToString()].Models;
                        // 如果没有传入文笺，则不需删除库中文笺。{只编辑了步骤数据，没有编辑步骤模型信息（没有打开编辑步骤窗体）}
                        // models = null 等于UI没有对models进行修改，所以不用对其进行更新（不删不加不改)
                        if (models != null)
                        {
                            // 删
                            foreach (var n in ctx.T_STEP_MODEL.Where(p => p.STEP_ID == st.Key))
                            {
                                ctx.T_STEP_MODEL.Remove(n);
                            }
                            // 加
                            foreach (var n in models)
                            {
                                ctx.T_STEP_MODEL.Add(new T_STEP_MODEL
                                {
                                    STEP_ID = st.Key,
                                    STEP_MODEL_ID = this.BuildID(),
                                    MODEL_ID = n,
                                    CREATE_MODE_TI = ModelTag.CreateMode.ManualCreate //默认都手动创建
                                });
                            }
                        }
                        // 更新输入项，先删再加
                        var inputs = stepsData[st.Key.ToString()].Inputs;
                        // 如果没有传入输入项，则不需删除库中输入项。
                        // INPUTS = null 等于不做任何修改
                        if (inputs != null)
                        {
                            // 删
                            foreach (var n in ctx.T_STEP_MODEL_ITEM.Where(p => p.STEP_ID == st.Key))
                            {
                                ctx.T_STEP_MODEL_ITEM.Remove(n);
                            }
                            // 加
                            foreach (var n in inputs)
                            {
                                ctx.T_STEP_MODEL_ITEM.Add(new T_STEP_MODEL_ITEM
                                {
                                    STEP_ID = st.Key,
                                    STEP_MODEL_ITEM_ID = this.BuildID(),
                                    MODEL_ITEM_ID = n
                                });
                            }
                        }
                        // 更新权限，先删后加
                        var auths = stepsData[st.Key.ToString()].Auths;
                        // Auths如为null，不需修改
                        if (auths != null)
                        {
                            // 删
                            foreach (var n in ctx.T_STEP_AUTH.Where(p => p.STEP_ID == st.Key))
                            {
                                ctx.T_STEP_AUTH.Remove(n);
                            }
                            // 加
                            foreach (var n in auths)
                            {
                                ctx.T_STEP_AUTH.Add(new T_STEP_AUTH
                                {
                                    STEP_ID = st.Key,
                                    STEP_AUTH_ID = this.BuildID(),
                                    AUTH_SCOPE_TI = n.AUTH_SCOPE_TI,
                                    AUTH_TO = n.AUTH_TO
                                });
                            }
                        }
                    }
                    #endregion
                    #region 删除，虚删

                    else if (st.Value == 2)
                    {
                        re += ctx.Database.ExecuteSqlCommand(@"update T_STEP set RS=0 where STEP_ID=:p0", st.Key);
                    }

                    #endregion
                }
                re += ctx.SaveChanges();
                return re;
            }
        }

        #endregion
    }
}
