using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.WebAPI.Controllers
{
	[RoutePrefix(_路由表.Tag._Prefix)]
	public class TagController : SNApiController
	{
		private readonly ITagCtrl _ctrl;

		public TagController()
		{
		    _ctrl = GetCtrl<ITagCtrl>("TagCtrl");
		}

        /// <summary>
        /// 获取所有标签分类
        /// </summary>
        /// <returns></returns>
		[HttpGet]
		[Route(_路由表.Tag.GetAllCategory)]
		public IList<DB.T_TAG_CATEGORY> GetAllCategory()
		{
			var re = _ctrl.GetAllCategory();
			return re;
		}
        /// <summary>
        /// 获取指定id的标签分类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route(_路由表.Tag.GetCategory)]
		public T_TAG_CATEGORY GetCategory(decimal categoryId)
		{
			return _ctrl.GetCategory(categoryId);
		}
        /// <summary>
        /// 获取指定分类的标签集合
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route(_路由表.Tag.GetTagByCategory)]
		public IList<T_TAG> GetTagByCategory(decimal categoryId)
		{
			return _ctrl.GetTagByCategory(categoryId);
		}
        /// <summary>
        /// 获取指定id的标签
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route(_路由表.Tag.GetTag)]
		public T_TAG GetTag(decimal tagId)
		{
			return _ctrl.GetTag(tagId);
		}
        /// <summary>
        /// 获取指定标签的标签项集合
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route(_路由表.Tag.GetTagItemByTag)]
		public IList<T_TAG_ITEM> GetTagItemByTag(decimal tagId)
		{
			return _ctrl.GetTagItemByTag(tagId);
		}
        /// <summary>
        /// 获取指定id的标签项
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
		[HttpGet]
		[Route(_路由表.Tag.GetTagItem)]
		public T_TAG_ITEM GetTagItem(decimal itemId)
		{
			return _ctrl.GetTagItem(itemId);
		}
        /// <summary>
        /// 添加一个标签分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
		[HttpPost]
		[Route(_路由表.Tag.PostCategory)]
		public decimal PostCategory(T_TAG_CATEGORY category)
		{
			return _ctrl.PostCategory(category);
		}
        /// <summary>
        /// 添加一个标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
		[HttpPost]
		[Route(_路由表.Tag.PostTag)]
		public decimal PostTag(T_TAG tag)
		{
			return _ctrl.PostTag(tag);
		}
        /// <summary>
        /// 添加一个标签项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
		[HttpPost]
		[Route(_路由表.Tag.PostTagItem)]
		public decimal PostTagItem(T_TAG_ITEM item)
		{
			return _ctrl.PostTagItem(item);
		}
        /// <summary>
        /// 更改一个标签分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
		[HttpPut]
		[Route(_路由表.Tag.PutCategory)]
		public decimal PutCategory(T_TAG_CATEGORY category)
		{
			return _ctrl.PutCategory(category);
		}
        /// <summary>
        /// 更改一个标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
		[HttpPut]
		[Route(_路由表.Tag.PutTag)]
		public decimal PutTag(T_TAG tag)
		{
			return _ctrl.PutTag(tag);
		}
        /// <summary>
        /// 更改一个标签项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
		[HttpPut]
		[Route(_路由表.Tag.PutTagItem)]
		public decimal PutTagItem(T_TAG_ITEM item)
		{
			return _ctrl.PutTagItem(item);
		}
        /// <summary>
        /// 删除一个标签分类（虚删）
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
		[HttpDelete]
		[Route(_路由表.Tag.DeleteCategory)]
		public decimal DeleteCategory(decimal categoryID)
		{
			return _ctrl.DeleteCategory(categoryID);
		}
        /// <summary>
        /// 删除一个标签（虚删）
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
		[HttpDelete]
		[Route(_路由表.Tag.DeleteTag)]
		public decimal DeleteTag(decimal tagID)
		{
			return _ctrl.DeleteTag(tagID);
		}
        /// <summary>
        /// 删除一个标签项（虚删）
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
		[HttpDelete]
		[Route(_路由表.Tag.DeleteTagItem)]
		public decimal DeleteTagItem(decimal itemID)
		{
			return _ctrl.DeleteTagItem(itemID);
		}


	}
}
