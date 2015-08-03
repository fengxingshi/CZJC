using System.Linq;
using CZOA.DB;
using CZOA.Ctrl.API;

namespace CZOA.Ctrl
{
	internal class FormCtrl : IFormCtrl
	{
		public T_FORM GetForm(decimal formId)
		{
			using (var ctx = new OAContext())
			{
				var re = ctx.T_FORM.SingleOrDefault(p => p.RS == 1 && p.FORM_ID == formId);
				return re;
			}
		}

		public decimal AddForm(T_FORM form)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_FORM.Add(form);
				return ctx.SaveChanges();
			}
		}

		public decimal UpdateForm(T_FORM form)
		{
			using (var ctx = new OAContext())
			{
				ctx.T_FORM.Attach(form);
				ctx.Entry<T_FORM>(form).State = System.Data.EntityState.Modified;
				return ctx.SaveChanges();
			}
		}

		public decimal DelForm(decimal formID)
		{
			using (var ctx = new OAContext())
			{
			    var re = 0;
                re += ctx.Database.ExecuteSqlCommand("update T_FORM set rs=0 where form_id=:p", formID);
                return re;
			}
		}
	}
}
