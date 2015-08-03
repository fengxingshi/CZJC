using System;
using System.Collections.Generic;
using System.Web.Http;
using CZJC.Ctrl.API;
using CZJC.DB;
using CtrlFactory = CZJC.Ctrl.CtrlFactory;

namespace CZJC.WebAPI.Controllers
{
    [RoutePrefix(_路由表.Test._Prefix)] //设置默认前缀
    public class TestController : SNApiController
    {
        private readonly ITestCtrl _ctrl;

        public TestController()
        {
            _ctrl = GetCtrl<ITestCtrl>("TestCtrl");
        }

        [HttpGet]
        [Route(_路由表.Test.GetNewid)]
        public decimal GetNewid()
        {
            return this.BuildID();
        }

        [HttpGet]
        [Route(_路由表.Test.GuidToDecimal)]
        public decimal GuidToDecimal(string guid,string guid1)
        {
            Guid g = Guid.Parse(guid);
            Guid g1 = Guid.Parse(guid1);
            var d = SN.Utility.SNHelper.GuidToLongID(g);
            var d1 = SN.Utility.SNHelper.GuidToLongID(g1);
            var re = d - d1;
            return re;
        }
        [HttpGet]
        [Route(_路由表.Test.GetDynamic)]
        public dynamic GetDynamic()
        {
            var re = _ctrl.GetTestDynamic();
            return re;
        }

        [HttpGet]
        [Route(_路由表.Test.GetTable)]
        public dynamic GetTable()
        {
            var re = _ctrl.GetTestTable();
            return re;
        }

        [HttpGet]
        [Route(_路由表.Test.Get)]
        public IEnumerable<TEST> Get()
        {
            var re = _ctrl.GetTest();

            return re;
        }

        [HttpGet]
        [Route(_路由表.Test.GetById)]
        public DB.TEST Get(decimal id)
        {
            var re = _ctrl.GetTest(id);
            //var set = new Newtonsoft.Json.JsonSerializerSettings();
            //set.FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Double;
            //set.FloatFormatHandling = Newtonsoft.Json.FloatFormatHandling.String;
            //var cvt = new JsonDecimalConverter();
            var js = Newtonsoft.Json.JsonConvert.SerializeObject(re);//, cvt);

            return re;
        }

        [HttpPost]
        [Route(_路由表.Test.Post)]
        public decimal Post(TEST test)
        {
            var re = _ctrl.AddTest(test);
            return re;
        }

        /// <summary>
        ///     与上面的POST同名，所以需要重新设置路由，否则调用出错
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(_路由表.Test.PostList)]
        public decimal Post(IList<TEST> list)
        {
            var re = _ctrl.AddTests(list);
            return re;
        }

        [HttpPost]
        [Route(_路由表.Test.PostDynamic)]
        public decimal Post(dynamic t)
        {
            var test = DynamicTo<DB.TEST>(t["Test"]);
            var tests = DynamicTo<IList<DB.TEST>>(t.Tests);
            var re = t;
            return 1;
        }

        [HttpPut]
        [Route(_路由表.Test.Put)]
        public decimal Put(TEST test)
        {
            var re = _ctrl.UpdateTest(test);
            return re;
        }

        [HttpDelete]
        [Route(_路由表.Test.Delete)]
        public decimal Delete(decimal id)
        {
            var re = _ctrl.DeleteTest(id);
            return re;
        }
    }
}
