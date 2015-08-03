using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZOA.Tag
{
	public struct ModelTag
	{
		/// <summary>
		/// 文笺创建方式
		/// </summary>
		public struct CreateMode
		{
			public const decimal AutoCreate = 1220;
			public const decimal ManualCreate = 1221;
		}
		/// <summary>
		/// 模型应用途径
		/// </summary>
		public struct RangeRank
		{
			/// <summary>
			/// 文笺数据
			/// </summary>
			public const decimal 用户文笺 = 1210;
			/// <summary>
			/// 背景数据
			/// </summary>
			public const decimal 后台数据 = 1211;
		}
		/// <summary>
		/// 模型类型
		/// ==动态==
		/// </summary>
		public struct ModelType
		{
			public const decimal 发文 = 1230;
			public const decimal 收文 = 1231;
			public const decimal 督办 = 1232;
			public const decimal 其他 = 1233;
		}
		/// <summary>
		/// 重要级别
		/// </summary>
		public struct ModelLevel
		{
			public const decimal 重要文笺 = 1240;
			public const decimal 普通文笺 = 1241;
		}
		/// <summary>
		/// 是否控制输入项
		/// </summary>
		public struct IsControlItems
		{
			public const decimal True = 1;
			public const decimal False = 0;
		}

		/// <summary>
		/// 文件类型
		/// </summary>
		public struct FormType
		{
			public const decimal HTM = 1250;
			public const decimal DOC = 1251;
			public const decimal XLS = 1252;
			public const decimal PPT = 1253;
			public const decimal TXT = 1254;
		}

		public struct ITEM_VALUE_TYPE
		{
			public const decimal 字符 = 1420;
			public const decimal 数字 = 1421;
			public const decimal 时间 = 1422;
			public const decimal 二进制 = 1423;
			public const decimal 对象 = 1424;
			public const decimal 表格 = 1425;
		}

		public struct FILL_MODE
		{
			public const decimal 累加式 = 1430;
			public const decimal 覆盖式 = 1431;
			public const decimal 插入式 = 1432;
		}

		public struct DISPLAY_STYLE
		{
			public const decimal 普通 = 1440;
			public const decimal 领导签字 = 1441;
			public const decimal 大段文字 = 1442;
			public const decimal 人员签名 = 1443;
		}

	}
}
