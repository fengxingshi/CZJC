using System;
using SN.Utility;

namespace CZJC
{
    public static class SNExtensions
    {
        public static decimal BuildID(this object obj)
        {
            return SN.Utility.SNHelper.BuildID();
        }
        /// <summary>
        ///     对象是否 Null | String.Empty | 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullEmptyZero(this object obj)
        {
            if (obj == null)
            {
                return true;
            }

            var t = obj.GetType();

            switch (t.Name)
            {
                case "String":
                    return String.IsNullOrEmpty((String)obj) ? true : false;
                case "Int32":
                    return (Int32)obj == 0 ? true : false;
                case "Int64":
                    return (Int64)obj == 0 ? true : false;
                case "Decimal":
                    return (Decimal)obj == 0 ? true : false;
                case "Single":
                    return (Single)obj == 0 ? true : false;
                case "Double":
                    return (Double)obj == 0 ? true : false;
                default:
                    return false;
            }
        }

        /// <summary>
        ///     条件True抛出异常
        /// </summary>
        /// <param name="b"></param>
        /// <param name="msg"></param>
        public static void TrueThrow(this bool b, string msg)
        {
            if (b)
            {
                throw new Exception(msg);
            }
        }

        /// <summary>
        ///     条件False抛出异常
        /// </summary>
        /// <param name="b"></param>
        /// <param name="msg"></param>
        public static void FalseThrow(this bool b, string msg)
        {
            if (!b)
            {
                throw new Exception(msg);
            }
        }

        /// <summary>
        ///     加密加压
        /// </summary>
        /// <param name="encodingString"></param>
        /// <returns></returns>
        public static string EncodeZip(this string encodingString)
        {
            var re = SNHelper.GzipCompress(encodingString);
            //var bytes = System.Text.Encoding.Default.GetBytes(encodingString);
            //var re = Convert.ToBase64String(bytes);
            return re;
        }

        /// <summary>
        ///     解压解密
        /// </summary>
        /// <param name="uncodingString"></param>
        /// <returns></returns>
        public static string UncodeZip(this string uncodingString)
        {
            var re = SNHelper.GzipDeCompress(uncodingString);
            //var bytes = Convert.FromBase64String(uncodingString);
            //var re = System.Text.Encoding.Default.GetString(bytes);
            return re;
        }

        ///// <summary>
        /////     为HttpClient添加自定义Session
        ///// </summary>
        ///// <param name="httpClient"></param>
        ///// <param name="session"></param>
        ///// <returns></returns>
        //public static void SetSession(this HttpClient httpClient, SNSession session)
        //{
        //    if (httpClient.DefaultRequestHeaders.Contains("SESSION")) // 避免重复添加SESSION
        //    {
        //        var seJson = session.ToJson().EncodeZip();
        //        // 添加至HttpHeader
        //        httpClient.DefaultRequestHeaders.Add("SESSION", seJson);
        //    }
        //}

        ///// <summary>
        ///// 初始化HttpClient对象
        ///// </summary>
        ///// <param name="httpClient"></param>
        ///// <param name="session"></param>
        ///// <returns></returns>
        //public static HttpClient InitHttp(this HttpClient httpClient, SNSession session = null)
        //{
        //    httpClient = httpClient ?? new HttpClient();
        //    if (session != null)
        //    {
        //        httpClient.SetSession(session);
        //    }
        //    return httpClient;
        //}

        ///// <summary>
        ///// 获取Json数据用的格式化器
        ///// 主要用作将Float转为Decimal
        ///// </summary>
        //private static JsonMediaTypeFormatter snJsonMediaTypeFormatter;
        ///// <summary>
        ///// 获取数据用的格式化器集合
        ///// Read服务器数据室当参数传入
        ///// </summary>
        //private static MediaTypeFormatter[] snMediaTypeFormatter;
        ///// <summary>
        ///// 扩展方法类初始化
        ///// </summary>
        //static SNExtensions()
        //{
        //    // 初始化媒体格式化器
        //    snJsonMediaTypeFormatter = new JsonMediaTypeFormatter
        //    {
        //        SerializerSettings =
        //        {
        //            FloatParseHandling = FloatParseHandling.Decimal
        //        }
        //    };
        //    // 初始化媒体格式化集合
        //    snMediaTypeFormatter = new MediaTypeFormatter[]
        //    {
        //        snJsonMediaTypeFormatter
        //    };
        //}
        ///// <summary>
        ///// HttpClient对象从服务器获取数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="http"></param>
        ///// <param name="uri"></param>
        ///// <param name="args"></param>
        ///// <returns></returns>
        //public static T Get<T>(this HttpClient http, string uri, params object[] args)
        //{
        //    var task = http.GetAsync(string.Format(uri, args));
        //    var re = task.ContinueWith((resp) =>
        //    {
        //        var obj = resp.Result.Content.ReadAsAsync<T>(snMediaTypeFormatter).Result;
        //        return obj;
        //    }).Result;
        //    return re;
        //}
    }
}
