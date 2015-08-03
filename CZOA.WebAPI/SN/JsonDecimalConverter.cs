using System;
using Newtonsoft.Json;

namespace CZJC.WebAPI
{
    /// <summary>
    /// json中decimal类型的转换器，主要为去掉json中decimal带的.0后缀
    /// </summary>
    public class JsonDecimalConverter : JsonConverter
    {
        private readonly Type decArrayType = typeof(decimal[]);
        private readonly Type decType = typeof(decimal);
        private readonly Type nvlDecType = typeof(decimal?);
        /// <summary>
        /// 是否支持转换的类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == decArrayType || objectType == decType || objectType == nvlDecType)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否可读
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }
        /// <summary>
        /// 读取json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 去掉.0后缀（按数组）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="array"></param>
        private void dumpNumArray<T>(JsonWriter writer, T[] array)
        {
            foreach (var n in array)
            {
                var s = n.ToString();
                if (s.EndsWith(".0"))
                {
                    writer.WriteRawValue(s.Substring(0, s.Length - 2));
                }
                else if (s.Contains("."))
                {
                    writer.WriteRawValue(s.TrimEnd('0'));
                }
                else
                {
                    writer.WriteRawValue(s);
                }
            }
        }
        /// <summary>
        /// 去掉.0后缀（按T）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="n"></param>
        private void dumpNum<T>(JsonWriter writer, T n)
        {
            var s = n.ToString();
            if (s.EndsWith(".0"))
            {
                writer.WriteRawValue(s.Substring(0, s.Length - 2));
            }
            else if (s.Contains("."))
            {
                writer.WriteRawValue(s.TrimEnd('0'));
            }
            else
            {
                writer.WriteRawValue(s);
            }
        }
        /// <summary>
        /// 写入json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = value.GetType();
            if (t == decArrayType)
            {
                writer.WriteStartArray();
                dumpNumArray<decimal>(writer, (decimal[])value);
                writer.WriteEndArray();
            }
            else if (t == decType || t == nvlDecType)
            {
                dumpNum<decimal>(writer, (decimal)value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
