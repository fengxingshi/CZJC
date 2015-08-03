using System.Collections.Generic;
using CZOA.DB;

namespace CZOA.Ctrl.API
{
	public interface ITagCtrl
	{
		/// <summary>
		/// 获取所有标签分类列表
		/// </summary>
		/// <returns></returns>
		IList<T_TAG_CATEGORY> GetAllCategory();
		/// <summary>
		/// 获取指定id的标签分类对象
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		T_TAG_CATEGORY GetCategory(decimal categoryId);
		/// <summary>
		/// 获取指定分类的所有标签列表
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		IList<T_TAG> GetTagByCategory(decimal categoryId);
		/// <summary>
		/// 获取指定id的标签对象
		/// </summary>
		/// <param name="tagId"></param>
		/// <returns></returns>
		T_TAG GetTag(decimal tagId);
		/// <summary>
		/// 获取指定标签的所有标签项列表
		/// </summary>
		/// <param name="tagId"></param>
		/// <returns></returns>
		IList<T_TAG_ITEM> GetTagItemByTag(decimal tagId);
		/// <summary>
		/// 获取指定id的标签项对象
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		T_TAG_ITEM GetTagItem(decimal itemId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		decimal PostCategory(T_TAG_CATEGORY category);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		decimal PostTag(T_TAG tag);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		decimal PostTagItem(T_TAG_ITEM item);

		decimal PutCategory(T_TAG_CATEGORY category);

		decimal PutTag(T_TAG tag);

		decimal PutTagItem(T_TAG_ITEM item);

		decimal DeleteCategory(decimal categoryID);

		decimal DeleteTag(decimal tagID);

		decimal DeleteTagItem(decimal itemID);
	}
}
