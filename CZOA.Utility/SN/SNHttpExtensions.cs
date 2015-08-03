using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CZOA
{
    public static class SNHttpExtensions
    {
        private const string SESSION_NAME = "SESSION";
        public static string HostUrl;// 从调用处初始化时赋值

        /// <summary>
        ///     获取Json数据用的格式化器
        ///     主要用作将Float转为Decimal
        /// </summary>
        private static readonly JsonMediaTypeFormatter snJsonMediaTypeFormatter;

        /// <summary>
        ///     获取数据用的格式化器集合
        ///     Read服务器数据室当参数传入
        /// </summary>
        private static readonly MediaTypeFormatter[] snMediaTypeFormatters;

        /// <summary>
        ///     扩展方法类初始化
        /// </summary>
        static SNHttpExtensions()
        {
            // 初始化媒体格式化器
            snJsonMediaTypeFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings =
                {
                    FloatParseHandling = FloatParseHandling.Decimal
                }
            };
            // 初始化媒体格式化集合
            snMediaTypeFormatters = new MediaTypeFormatter[]
            {
                snJsonMediaTypeFormatter
            };
        }

        /// <summary>
        ///     为HttpClient添加自定义Session
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static void SetSession(this HttpClient httpClient, SNSession session)
        {
            //如果没有SESSION，则添加，有则什么也不做
            if (!httpClient.DefaultRequestHeaders.Contains(SESSION_NAME)) // 避免重复添加SESSION
            {
                var seJson = session.ToJson().EncodeZip();
                // 添加至HttpHeader
                httpClient.DefaultRequestHeaders.Add(SESSION_NAME, seJson);
            }
        }

        public static void ResetSession(this HttpClient httpClient, SNSession session)
        {
            //如果已包含SESSION，删除先
            if (httpClient.DefaultRequestHeaders.Contains(SESSION_NAME)) // 避免重复添加SESSION
            {
                httpClient.DefaultRequestHeaders.Remove(SESSION_NAME);
            }
            var seJson = session.ToJson().EncodeZip();
            // 添加至HttpHeader
            httpClient.DefaultRequestHeaders.Add(SESSION_NAME, seJson);
        }

        /// <summary>
        ///     初始化HttpClient对象
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static HttpClient Init(this HttpClient httpClient, SNSession session = null)
        {
            httpClient = httpClient ?? new HttpClient();
            if (session != null)
            {
                httpClient.SetSession(session);
            }
            return httpClient;
        }

        private static string BuildUri<T>(string path = "", bool needArg = true)
        {
            path = needArg ? path : path.Replace("{0}", "");

            var apiPath = HostUrl;

            var isRootPath = path.StartsWith("/");

            var uri = string.Empty;

            if (isRootPath)
            {
                /* path是根路径，以path为路径 */
                uri = string.Format("{0}/{1}", apiPath, path);
            }
            else
            {
                /* path不是根路径则从泛型类型中提取路径 */
                var type = typeof(T);
                // 如果是泛型集合，提取泛型类型参数集合中第一个类型的类型名
                var typeName = type.IsGenericType ? type.GetGenericArguments()[0].Name : type.Name;
                // 泛型+添加和更新(needArg=false) = 路径后加list
                // path = needArg ? path : "list";

                uri = string.Format("{0}/{1}/{2}", apiPath, typeName, path);
            }

            return uri;
        }

        private static string RegexUri(string path = "")
        {
            var r = new Regex(@"\{\w+\}");
            var maths = r.Matches(path);

            var list = new List<string>();
            for (var i = 0; i < maths.Count; i++)
            {
                var t = maths[i].Value;
                list.Add(t);
            }

            var dlist = list.Distinct().ToList();
            for (var i = 0; i < dlist.Count(); i++)
            {
                path = path.Replace(dlist[i], string.Format("{{{0}}}", i));
            }

            return path;
        }

        private static string InitUri<T>(string path = "")
        {
            if (!path.StartsWith("/") && typeof(T).Name.Equals("Object"))
            {
                //throw new Exception(@"dynamic type route must start with '/table/'");
            }

            var apiPath = HostUrl;

            path = path.StartsWith("~/api/") ? path.Replace("~/api/", "/") : path;// 如果是~/api/开头的则替换为/开头

            path = RegexUri(path);

            var isRootPath = path.StartsWith("/");
            if (isRootPath)
            {
                apiPath = string.Format("{0}/{1}", apiPath, path);
            }
            else
            {
                var tableName = "";
                var table = typeof(T);
                if (table.IsGenericType)
                {
                    //如果是泛型
                    tableName = table.GetGenericArguments()[0].Name;
                }
                else
                {
                    tableName = table.Name;
                }
                apiPath = string.Format("{0}/{1}/{2}", apiPath, tableName, path);
            }
            return apiPath;
        }

        /// <summary>
        ///     HttpClient对象从服务器获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="http"></param>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Get<T>(this HttpClient http, string path = "", params object[] args)
        {
            var uri = InitUri<T>(path);

            var argLs = args.Select(a => a ?? "null").ToList();
            args = argLs.ToArray<object>();

            if (uri.Contains("{0}"))
            {
                uri = string.Format(uri, args);
            }
            else
            {
                uri = string.Format("{0}/{1}", uri, args.Any() ? args[0] : ""); //路径没有{0}参数，args只有一个值
            }

            var task = http.GetAsync(uri);

            var re = task.ContinueWith((resp) =>
            {
                var obj = resp.Result.Content.ReadAsAsync<T>(snMediaTypeFormatters).Result;
                return obj;
            }).Result;

            return re;
        }

        public static decimal Post<T>(this HttpClient http, string path, T t)
        {
            var uri = InitUri<T>(path);

            var task = http.PostAsync<T>(uri, t, snJsonMediaTypeFormatter);

            var re = task.ContinueWith((resp) =>
            {
                var rst = resp.Result.Content.ReadAsAsync<decimal>(snMediaTypeFormatters).Result;
                return rst;
            }).Result;

            return re;
        }

        public static decimal Put<T>(this HttpClient http, string path, T t)
        {
            var uri = InitUri<T>(path);

            var task = http.PutAsync<T>(uri, t, snJsonMediaTypeFormatter);

            var re = task.ContinueWith((resp) =>
            {
                var rst = resp.Result.Content.ReadAsAsync<decimal>(snMediaTypeFormatters).Result;
                return rst;
            }).Result;

            return re;
        }

        public static decimal Delete<T>(this HttpClient http, string path, string delId = "")
        {
            var uri = InitUri<T>(path);
            if (uri.Contains("{0}"))
            {
                uri = string.Format(uri, delId);
            }
            else
            {
                uri = string.Format("{0}/{1}", uri, delId);
            }

            var task = http.DeleteAsync(uri);

            var re = task.ContinueWith((resp) =>
            {
                var rst = resp.Result.Content.ReadAsAsync<decimal>(snMediaTypeFormatters).Result;
                return rst;
            }).Result;

            return re;
        }
    }
}
