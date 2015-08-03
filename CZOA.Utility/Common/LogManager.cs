using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CZJC.Utility.Common
{
	public static class LogManager
	{

		/// <summary> 
		/// 写文件 
		/// </summary> 
		/// <param name="str"></param> 
		public static void WriteLog(string str)
		{
			if (!Directory.Exists("ErrLog"))
			{
				Directory.CreateDirectory("ErrLog");
			}
			string file = string.Format(@"ErrLog\ErrLog{0}.txt", DateTime.Now.ToString("-yyyy-MM"));
			using (StreamWriter sw = new StreamWriter(file, true))
			{
				sw.WriteLine(str);
				sw.WriteLine("---------------------------------------------------------");
				sw.Close();
			}
		}
	}
}
