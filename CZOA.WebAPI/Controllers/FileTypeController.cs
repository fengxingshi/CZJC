using System.Collections.Generic;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;

namespace CZOA.WebAPI.Controllers
{
    /// <summary>
    /// 提供文件类型的相关服务
    /// </summary>
    [RoutePrefix("api/file")] //设置默认前缀
    public class FileTypeController : SNApiController
    {
        private readonly IFileType _ctrl;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileTypeController()
        {
            _ctrl = GetCtrl<IFileType>("FileType");
        }
        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IList<C_FILE_TYPE> GetAll()
        {
            var re = _ctrl.GetFileType();
            return re;
        }
        
        /// <summary>
        /// 添加文件类型
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public decimal AddFileType(C_FILE_TYPE fileType)
        {
            var re = _ctrl.AddFileType(fileType);
            return re;
        }
        /// <summary>
        /// 更新文件类型
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        public decimal UpdataFileType(C_FILE_TYPE fileType)
        {
            var re = _ctrl.UpdataFileType(fileType);
            return re;
        }

    }
}
