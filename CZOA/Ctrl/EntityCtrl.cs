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
    internal class EntityCtrl : IEntityCtrl
    {
        public IList<T_ENTITY> CreateEntityForNode(T_NODE node, T_NODE_DOMAIN nodeDomain)
        {
            return CreateEntityForNode(node.STEP_ID, node.WORK_ID, node.NODE_ID, nodeDomain.DEPT_ID, nodeDomain.USER_ID);
        }

        public IList<T_ENTITY> CreateEntityForNode(decimal stepId, decimal workId, decimal nodeId, decimal deptId,
            decimal userId)
        {
            using (var ctx = new OAContext())
            {
                /* ModelCtrl有获取步骤模型集合的方法，但此处为了性能，直接复制代码于此 */
                // 获取Node关联的Step可用Model，根据Step_Model关系
                var models =
                    from m in ctx.T_MODEL.Where(p => p.RS == 1)
                    join sm in ctx.T_STEP_MODEL.Where(p => p.RS == 1) on m.MODEL_ID equals sm.MODEL_ID
                    where sm.STEP_ID == stepId
                    select m;
                // 循环可用Model，根据Model生成Entity
                return models.ToList() //用ToList预加载
                    .Select(m => new T_ENTITY
                    {
                        WORK_ID = workId,
                        NODE_ID = nodeId,
                        MODEL_ID = m.MODEL_ID,
                        FORM_ID = m.FORM_ID,
                        ENTITY_ID = this.BuildID(), //生成newId
                        ENTITY_NAME = m.MODEL_NAME, //EntityName默认为ModelName
                        DEPT_ID = deptId,
                        USER_ID = userId, //不需考虑代理情况
                        ENTITY_IDX = m.MODEL_IDX,
                        //RANGE_RANK_TI=Tag.ModelTag.RangeRank.NodeRank, //TODO:暂时不考虑应用范围
                        ACTIVE_TI = EntityTag.Active.DoneSave, //默认为正式（完成时保存）
                    }).ToList();
            }
        }

        public IList<T_ENTITY> GetEntityListByWork(decimal workId)
        {
            using (var ctx = new OAContext())
            {
                var re = ctx.T_ENTITY.Where(p => p.RS == 1 && p.WORK_ID == workId);
                return re.ToList();
            }
        }

        public decimal SaveEntities(IList<T_ENTITY> entityList, decimal active=Tag.EntityTag.Active.DoneSave)
        {
            //TODO:尚未测试，逻辑也为检验
            using (var ctx = new OAContext())
            {
                foreach (var e in entityList)
                {
                    if (e.RS == null)
                    {
                        e.ACTIVE_TI = active; //设置新加实体的激活状态
                        ctx.T_ENTITY.Add(e);
                    }
                    else
                    {
                        ctx.T_ENTITY.Attach(e);
                        ctx.Entry<T_ENTITY>(e).State = EntityState.Modified;
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public IList<T_ENTITY_ITEM> GetEntityItems(decimal entityId, decimal? userId = null)
        {
            using (var ctx = new OAContext())
            {
                #region

                //var re =
                //                    ctx.T_ENTITY_ITEM.SqlQuery(@"
                //select * from t_entity_item
                // where rs = 1 and entity_id = :p0
                //   and ts = (select max(t.ts) from t_entity_item t
                //              where t.rs = 1 and t.entity_id = :p0)", entityId);//DONE:仅按时间戳筛选最新，也许不严谨
                //                return re.ToList();
                //DONE:修复换用以下逻辑。上面的是每次输入都存一遍所有输入项，应该是下面的只存有改变的输入项
                //select r.* from t_entity_item r inner join 
                //(
                //  select t.model_item_id,max(t.ts) ts from t_entity_item t
                //  where t.model_id=:mid
                //  group by t.model_item_id --按ModelItem分组，取得组中最新的Ts
                //) e on r.model_item_id=e.model_item_id and r.ts=e.ts and r.rs=1 --按modelItemId和ts链接
                //where r.entity_id=:eid
                //--有可能有ts相等的同ModelItemId的，需要筛选

                //select *
                //  from t_entity_item
                // where create_seq in
                //       (select max(t.create_seq) create_seq
                //          from t_entity_item t
                //         where (t.active_ti = :正式 or (t.user_id = :当前用户 and t.active_ti = :暂存))
                //           and t.model_item_id in
                //               (select model_item_id
                //                  from t_model_item
                //                 where model_id =
                //                       (select model_id from t_entity where entity_id = :实体))
                //         group by t.model_item_id)

                #endregion

                //DONE:新查询逻辑，一切都搞定
                // 1.根据实体的模型的模型项获取相关实体项
                // 2.对实体项按模型项分组，取创建序列最大（最新）的项，即获取每个模型项最新的实体项
                // 3.默认返回全正式的实体项，如果传入用户则返回用户能看到的最新数据（有暂存返回暂存，没暂存返回最新正式）
                var sqlstr = string.Format(@"
select *
  from t_entity_item
 where create_seq in
       (select max(t.create_seq) create_seq
          from t_entity_item t
         where (t.active_ti = {0} {1})
           and t.model_item_id in
               (select model_item_id
                  from t_model_item
                 where model_id =
                       (select model_id from t_entity where entity_id = {2}))
         group by t.model_item_id)",
                    EntityItemTag.Active.DoneSave,
                    userId == null
                        ? ""
                        : string.Format(@"or (t.user_id = {0} and t.active_ti = {1})", userId,
                            EntityItemTag.Active.QuickSave),
                    entityId);

                var re = ctx.T_ENTITY_ITEM.SqlQuery(sqlstr);
                return re.ToList();
            }
        }

        public decimal SaveEntityItems(IList<T_ENTITY_ITEM> entityList, decimal active = Tag.EntityItemTag.Active.DoneSave)
        {
            // 1.从库中提取要更新的数据，根据要更新数据的ItemId
            // 2.与库中数据行做对比，判断要更新的数据执行新增还是更新操作
            // 3.新增和更新状态暂存于要更新的数据的create_seq字段，-100新增 -200更新
            // 4.要新增的数据add到表中
            // 5.要更新的数据赋值到从数据库中提取的旧数据上，savechange时或被当作更新的
            // 6.提交入库
            //TODO:尚未测试
            using (var ctx = new OAContext())
            {
                //using (var scope = new TransactionScope()) //事务
                {
                    var update = -200;
                    var insert = -100;
                    // 获取相对应的旧实体项
                    var old =
                        from t in ctx.T_ENTITY_ITEM
                        where entityList.Select(p => p.ENTITY_ITEM_ID).Contains(t.ENTITY_ITEM_ID)
                        select t; //读库!

                    // 循环要更新的list，进行增、改筛选
                    foreach (var n in entityList)
                    {
                        var o = old.SingleOrDefault(p => p.ENTITY_ITEM_ID == n.ENTITY_ITEM_ID);
                        if (o != null) //如果库中存在旧数据
                        {
                            if (o.ITEM_VALUE_TYPE_TI == n.ITEM_VALUE_TYPE_TI &&
                                o.ITEM_VALUE.Equals(n.ITEM_VALUE) &&
                                o.ITEM_VALUE_BINARY.Equals(n.ITEM_VALUE_BINARY)) // 值类型相同 + 普通值相同 + 二进制对象值相同 = 值相同
                            {
                                // 暂存状态 = 更新
                                n.CREATE_SEQ = update;
                            }
                            else // 值不同
                            {
                                if (o.ACTIVE_TI == EntityItemTag.Active.DoneSave) // 状态正式 = 新增
                                {
                                    n.CREATE_SEQ = insert;
                                }
                                else // 内容不同 + 状态暂存 = 更新
                                {
                                    n.CREATE_SEQ = update;
                                }
                            }
                        }
                        else //库中没有的数据，直接新增
                        {
                            n.CREATE_SEQ = insert; //新增
                        }

                        n.ACTIVE_TI = active; //存储状态
                        n.TS = DateTime.Now; //更新时间戳

                        if (n.CREATE_SEQ == insert)
                        {
                            n.ENTITY_ITEM_ID = this.BuildID(); //产生新ItemId
                            ctx.T_ENTITY_ITEM.Add(n); //新增入库
                        }
                        else if (n.CREATE_SEQ == update)
                        {
                            o = n; //库中旧行赋于新值
                        }
                    }

                    var re = ctx.SaveChanges(); //写库!
                    //scope.Complete();
                    return re;
                }
            }
        }
    }
}
