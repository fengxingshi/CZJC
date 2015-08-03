using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;

namespace CZJC.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
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
        }
    }
}
