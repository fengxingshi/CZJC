using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
	public interface IModelCtrl
	{
		/// <summary>
		/// 获取步骤所关联的模型集合
		/// </summary>
		/// <param name="stepId"></param>
		/// <returns></returns>
		IList<T_MODEL> GetStepModels(decimal stepId);
		/// <summary>
		/// 获取模型的模型项集合
		/// </summary>
		/// <param name="modelId"></param>
		/// <returns></returns>
		IList<T_MODEL_ITEM> GetModelItems(decimal modelId);

		decimal AddModelItme(IList<T_MODEL_ITEM> items);

		dynamic GetAllModel(decimal orgID);

		decimal AddModel(T_MODEL model);

		T_MODEL GetModelByID(decimal modelID);

		decimal DelModelByID(decimal modelID);

		decimal UpdateModel(T_MODEL model);		
	}
}
