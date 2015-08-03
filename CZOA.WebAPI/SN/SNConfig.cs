using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN
{
    /// <summary>
    /// 全部静态非并行
    /// </summary>
    public class SNConfig
    {
        private static bool IsInit = false; 

        public static string[] HostUrl = null;
        //public static string FilePath = string.Empty;
        //public static string SystemTempPath = string.Empty;
        //public static readonly int PathLevel = 1;//生成一级子目录( 000/000 000 000 000 000 0.fs）
        //public static long MaxUploadFileSize = 10 * 1024 * 1024;//byte 10M 默认

        public static void Init(bool reInit = false)
        {
            if (!IsInit || reInit)//如果 没有初始化 或 需要重新初始化
            {
                var config = System.Configuration.ConfigurationManager.AppSettings;

                var hosts = config["HostUrl"].Trim(',').Equals("") ?
                        new string[] { "127.0.0.1:20511" } :
                        config["HostUrl"].Trim(',').Split(',');
                for (int i = 0; i < hosts.Length; i++)
                {
                    hosts[i] = @"http://" + hosts[i];
                }
                HostUrl = hosts;

                //ConnString = string.Format(@"Data Source={0};Initial Catalog=HebBgzdhDB;User Id={1};Password={2};Application Name=Hebcz_OA", config["DataBaseAddr"], config["DataBaseUser"], config["DataBasePwd"]);

                ////文件路径如果为空字符串则为 系统临时文件夹，否则使用config中的设置
                //SystemTempPath = System.IO.Path.GetTempPath();
                //FilePath = string.Format(@"{0}", config["FilePath"]).Equals("") ? SystemTempPath : config["FilePath"];

                //MaxUploadFileSize = config["MaxUploadFileSize"].Equals("") ?
                //                    10 * 1024 * 1024 :
                //                    Convert.ToInt32(config["MaxUploadFileSize"]) * 1024 * 1024;

                IsInit = true;
            }
        }
    }
}
