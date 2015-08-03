using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN
{
    public class SNLog
    {
        private string ErrorLogFileName = string.Empty;
        private Dictionary<DateTime, Exception> ErrorPool = new Dictionary<DateTime, Exception>();
        public static Exception GetError(Exception err)
        {
            //添加到连接池
            return err;//返回传入的异常
        }
        public static bool SaveError(Exception err)
        {
            //将连接池的异常保存到错误日志文件中，并清空连接池
            return true;//保存成功返回true
        }

        public static void AddMsg(string msg)
        {
            SNMsgHandler.RequestURLs.Add(msg);
        }

        public static string GetMsg()
        {
            StringBuilder sb = new StringBuilder();
            lock (SN.SNMsgHandler.RequestURLs)
            {
                for (int i = 0; i < SN.SNMsgHandler.RequestURLs.Count; i++)
                {
                    sb.Append(SN.SNMsgHandler.RequestURLs[i]);
                    sb.Append("\r\n");
                }
                SN.SNMsgHandler.RequestURLs.Clear();
            }
            return sb.ToString();
        }
    }
}
