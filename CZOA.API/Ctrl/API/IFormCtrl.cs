using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
	public interface IFormCtrl
	{
		/// <summary>
		/// 获取指定formId的表单
		/// </summary>
		/// <param name="formId"></param>
		/// <returns></returns>
		T_FORM GetForm(decimal formId);

		decimal AddForm(T_FORM form);

		decimal UpdateForm(T_FORM form);

		decimal DelForm(decimal formID);
	}
}
