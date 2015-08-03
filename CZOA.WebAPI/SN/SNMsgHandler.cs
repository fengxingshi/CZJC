using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SN
{
    /// <summary>
    /// 方法非并行，属性字段共享
    /// </summary>
    public class SNMsgHandler : DelegatingHandler
    {
        public static List<string> RequestURLs = new List<string>();
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            //将每次的访问路径添加到RequestURLs中
            //var ip = (request.Properties["System.ServiceModel.Channels.RemoteEndpointMessageProperty"] as System.ServiceModel.Channels.RemoteEndpointMessageProperty);//SelfHost版
            var ip = request.Properties["MS_OwinContext"] as Microsoft.Owin.OwinContext;//OWin版

            if (request.RequestUri != null)
            {
                //不在服务端信息窗口显示登录信息，不显示病毒式访问，不显示访问小图标
                if (!request.RequestUri.AbsolutePath.Contains("GetUserID") 
                    && !request.RequestUri.AbsolutePath.Equals("/")
                    && !request.RequestUri.AbsolutePath.Equals("/favicon.ico"))
                {
                //RequestURLs.Add(string.Format("{0} - [{1}:{2}] - {3}", DateTime.Now.ToString(), ip.Address, ip.Port, request.RequestUri.AbsolutePath));//SelfHost版

                RequestURLs.Add(string.Format("{0} - [{1}:{2}] - {3}", DateTime.Now.ToString(), ip.Request.RemoteIpAddress, ip.Request.RemotePort, request.RequestUri.AbsolutePath));//OWin版
                }
            }
            //true 继续传递request到其它过滤器
            return base.SendAsync(request, cancellationToken);
            //false
            //403
            //return Task.Factory.StartNew<HttpResponseMessage>(() => { return new HttpResponseMessage(HttpStatusCode.Forbidden); });
        }
    }
}
