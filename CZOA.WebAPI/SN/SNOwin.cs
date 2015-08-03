using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using CZOA.WebAPI;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SN
{
    /// <summary>
    /// 自宿主启动器
    /// </summary>
    public class SNOwin
    {
        private static IDisposable _CloseOWin = null;
        public static void Open(params string[] hostUrls)
        {
            try
            {
                if (_CloseOWin == null)
                {
                    // Start OWIN host 
                    StartOptions so = new StartOptions();
                    foreach (string s in hostUrls)
                    {
                        so.Urls.Add(s);
                    }
                    _CloseOWin = WebApp.Start<SNStartup>(so);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is System.Net.HttpListenerException)
                {
                    throw new Exception("监听IP有错误");
                }

                if (_CloseOWin != null)
                {
                    _CloseOWin.Dispose();//必须
                    _CloseOWin = null;//必须
                }

                throw;
            }
        }

        public static void Close()
        {
            if (_CloseOWin != null)
            {
                // Close OWIN host
                _CloseOWin.Dispose();
                _CloseOWin = null;
            }
        }
    }

    /// <summary>
    /// 路由设置，消息监控，格式返回等设置
    /// </summary>
    internal class SNStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            // Web API 配置和服务
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            // VIP:将float转为decimal，而不是默认的double
            config.Formatters.JsonFormatter.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
            config.Formatters.JsonFormatter.SerializerSettings.FloatFormatHandling = FloatFormatHandling.String;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = SN.Utility.SNDateTimeFormat._100;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new JsonDecimalConverter());
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //RouteTable.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{id}",
            //    defaults: new
            //    {
            //        id = RouteParameter.Optional
            //    }
            //    ).RouteHandler = new SessionStateRouteHandler();

            //设定返回格式，只返回JSON
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //注册自己的过滤器
            config.MessageHandlers.Add(new SN.SNMsgHandler());
            //开启跨域访问
            //httpConfig.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));//全局开启所有域所有请求标头所有请求方法
            config.EnableCors(); 

            appBuilder.UseWebApi(config);
        }
    }

    //public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    //{
    //    public SessionableControllerHandler(RouteData routeData)
    //        : base(routeData)
    //    { }
    //}
    //public class SessionStateRouteHandler : IRouteHandler
    //{
    //    IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
    //    {
    //        return new SessionableControllerHandler(requestContext.RouteData);
    //    }
    //}  
}
