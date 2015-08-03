using System.Linq;
using System.Web.Http;

namespace CZJC.WebAPI
{
    /// <summary>
    /// ApiController的扩展，增加了SESSION
    /// </summary>
    public static class SNExtensions
    {
        private const string SESSION_NAME = "SESSION";
        /// <summary>
        /// 获取当前apiController中的session信息
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static SNSession GetSession(this ApiController ctrl)
        {
            if (ctrl.Request.Headers.Contains(SESSION_NAME))
            {
                // 从HttpHeader中读取Session
                var se = ctrl.Request.Headers.GetValues(SESSION_NAME) as string[];
                if (se != null && se.Any())
                {
                    var seStr = se[0].UncodeZip();
                    var session = Newtonsoft.Json.JsonConvert.DeserializeObject<SNSession>(seStr);
                    return session;
                }
            }
            return null;
        }
    }
}
