using System.Collections.Generic;
using System.Linq;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.Ctrl
{
	internal class ModelCtrl : IModelCtrl
	{
		/// <summary>
		/// 获取步骤所关联的模型集合
		/// </summary>
		/// <param name="stepId"></param>
		/// <returns></returns>
		public IList<T_MODEL> GetStepModels(decimal stepId)
		{
			using (var ctx = new OAContext())
			{
				// 获取Node关联的Step可用Model，根据Step_Model关系
				var re =
					from m in ctx.T_MODEL.Where(p => p.RS == 1)
					join sm in ctx.T_STEP_MODEL.Where(p => p.RS == 1) on m.MODEL_ID equals sm.MODEL_ID
					where sm.STEP_ID == stepId
					select m;
				return re.ToList();
			}
		}
		/// <summary>
		/// 获取模型的模型项集合
		/// </summary>
		/// <param name="modelId"></param>
		/// <returns></returns>
		public IList<T_MODEL_ITEM> GetModelItems(decimal modelId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_MODEL_ITEM.Where(p => p.RS == 1 && p.MODEL_ID == modelId);
				return re.ToList();
			}
		}

		public decimal AddModelItme(IList<T_MODEL_ITEM> items)
		{
			using (var ctx = new OAContext())
			{
				ctx.Database.ExecuteSqlCommand("update T_MODEL_ITEM set rs=0 where model_id=:p0", items[0].MODEL_ID);
				int i = 0;
				foreach (var item in items)
				{
					if (item.RS == null && item.TS == null)
					{
						var re = ctx.T_MODEL_ITEM.Add(item);
					}
					else
					{
						ctx.Database.ExecuteSqlCommand("update T_MODEL_ITEM set rs=1 where model_item_id =:p0", item.MODEL_ITEM_ID);
						ctx.T_MODEL_ITEM.Attach(item);
						ctx.Entry<T_MODEL_ITEM>(item).State = System.Data.EntityState.Modified;
					}
					i++;
				}
				ctx.SaveChanges();
				return i;
			}
		}

		public dynamic GetAllModel(decimal orgID)
		{
			using (var ctx = new OAContext())
			{
				return ctx.SqlDynamic(string.Format(@"select m.model_id,m.model_name 表单名称, i.item_name 表单类型,i2.item_name 表单级别,nvl(o.org_name,'全部') 适用单位 from T_MODEL m
									left join t_tag_item i on i.tag_item_id=m.MODEL_TYPE_TI
									left join t_tag_item i2 on i2.tag_item_id=m.model_level_ti
									left join T_ORG o on o.org_id=m.org_id
									where m.rs=1 and (m.org_id is null or m.org_id={0})
									order by m.org_id,m.model_idx", orgID));
			}
		}

		public decimal AddModel(T_MODEL model)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_MODEL.Add(model);
				return ctx.SaveChanges();
			}
		}

		public T_MODEL GetModelByID(decimal modelID)
		{
			using (var ctx = new OAContext())
			{
				return ctx.T_MODEL.FirstOrDefault(p => p.MODEL_ID == modelID && p.RS == 1);
			}
		}

		public decimal DelModelByID(decimal modelID)
		{
			using (var ctx = new OAContext())
			{
				var re = 0;
				re += ctx.Database.ExecuteSqlCommand("update T_MODEL set rs=0 where model_id=:p", modelID);
				return re;
			}
		}

		public decimal UpdateModel(T_MODEL model)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_MODEL.Attach(model);
				ctx.Entry<T_MODEL>(model).State = System.Data.EntityState.Modified;
				return ctx.SaveChanges();
			}
		}
	}
}
