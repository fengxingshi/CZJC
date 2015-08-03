using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CZOA.Ctrl.API;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl
{
	internal class TagCtrl : ITagCtrl
	{
		public IList<T_TAG_CATEGORY> GetAllCategory()
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_TAG_CATEGORY.Where(p => p.RS == 1);
				return re.ToList();
			}
		}

		public T_TAG_CATEGORY GetCategory(decimal categoryId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_TAG_CATEGORY.SingleOrDefault(p => p.RS == 1 && p.TAG_CATEGORY_ID == categoryId);
				return re;
			}
		}

		public IList<T_TAG> GetTagByCategory(decimal categoryId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_TAG.Where(p => p.RS == 1 && p.TAG_CATEGORY_ID == categoryId);
				return re.ToList();
			}
		}

		public T_TAG GetTag(decimal tagId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_TAG.SingleOrDefault(p => p.RS == 1 && p.TAG_ID == tagId);
				return re;
			}
		}

		public IList<T_TAG_ITEM> GetTagItemByTag(decimal tagId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_TAG_ITEM.Where(p => p.RS == 1 && p.TAG_ID == tagId);
				return re.ToList();
			}
		}

		public T_TAG_ITEM GetTagItem(decimal itemId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_TAG_ITEM.SingleOrDefault(p => p.RS == 1 && p.TAG_ITEM_ID == itemId);
				return re;
			}
		}

		public decimal PostCategory(T_TAG_CATEGORY category)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_TAG_CATEGORY.Add(category);
				return ctx.SaveChanges();
			}
		}

		public decimal PostTag(T_TAG tag)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_TAG.Add(tag);
				return ctx.SaveChanges();
			}
		}

		public decimal PostTagItem(T_TAG_ITEM item)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_TAG_ITEM.Add(item);
				return ctx.SaveChanges();
			}
		}

		public decimal PutCategory(T_TAG_CATEGORY category)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_TAG_CATEGORY.Attach(category);
				ctx.Entry<T_TAG_CATEGORY>(category).State = EntityState.Modified;
				return ctx.SaveChanges();
			}
		}

		public decimal PutTag(T_TAG tag)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_TAG.Attach(tag);
				ctx.Entry<T_TAG>(tag).State = EntityState.Modified;
				return ctx.SaveChanges();
			}
		}

		public decimal PutTagItem(T_TAG_ITEM item)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_TAG_ITEM.Attach(item);
				ctx.Entry<T_TAG_ITEM>(item).State = EntityState.Modified;
				return ctx.SaveChanges();
			}
		}

		public decimal DeleteCategory(decimal categoryID)
		{
			using (var ctx = new OAContext())
			{
				return ctx.Database.ExecuteSqlCommand("update T_TAG_CATEGORY set rs=0 where tag_category_id=:p0", categoryID);
			}
		}

		public decimal DeleteTag(decimal tagID)
		{
			using (var ctx = new OAContext())
			{
				return ctx.Database.ExecuteSqlCommand("update T_TAG set rs=0 where tag_id=:p0", tagID);
			}
		}

		public decimal DeleteTagItem(decimal itemID)
		{
			using (var ctx = new OAContext())
			{
				return ctx.Database.ExecuteSqlCommand("update T_TAG_ITEM set rs=0 where tag_item_id=:p0", itemID);
			}
		}
	}
}
