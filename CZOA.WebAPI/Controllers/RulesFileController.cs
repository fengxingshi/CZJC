using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.WebAPI.Controllers
{
    /// <summary>
    /// 提供文件类型的相关服务
    /// </summary>
    [RoutePrefix("api/RulesFile")] //设置默认前缀
    public class RulesFileController : SNApiController
    {
        private readonly IRulesFileCtrl _ctrl;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RulesFileController()
        {
            _ctrl = GetCtrl<IRulesFileCtrl>("RulesFile");
        }
        /// <summary>
        /// 获取指定处室下的制度库文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{departmentId}")]
        public DataTable GetRulesFile(long departmentId)
        {
            var re = _ctrl.GetRulesFile(departmentId);
            return re;
        }

        /// <summary>
        /// 获取某处室下某类型的制度库文件
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="type"></param>
        /// <param name="sccope">访问范围</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{orgId}-{departmentId}-{userId}-{type}-{sccope}")]
        public DataTable GetTypeRulesFile(decimal orgId, decimal departmentId, decimal userId, decimal type, decimal sccope = 0)
        {
            var re = _ctrl.GetTypeRulesFile(orgId,departmentId,userId,type,sccope);
            return re;
        }

        /// <summary>
        /// 添加文件信息
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public decimal AddRulesFile(C_RULES_FILE file)
        {
            var re = _ctrl.AddRulesFile(file);
            return re;
        }

        /// <summary>
        /// 更新文件信息
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        public decimal UpdataRulesFile(C_RULES_FILE file)
        {
            var re = _ctrl.UpdataRulesFile(file);
            return re;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="rulesFileId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{rulesFileId}")]
        public decimal DeleteRulesFile(decimal rulesFileId)
        {
            var re = _ctrl.DeleteRulesFile(rulesFileId);
            return re;
        }

        /// <summary>
        /// 更新是否发布
        /// </summary>
        /// <param name="rulesFileId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("edit/{rulesFileId}-{state}-{userId}")]
        public decimal UpdataFileState(decimal rulesFileId, int state,decimal userId)
        {
            var re = _ctrl.UpdataFileState(rulesFileId, state, userId);
            return re;
        }
    }
}
