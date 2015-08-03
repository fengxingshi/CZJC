using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using CZOA.Ctrl.API;
using CZOA.DB;
using Newtonsoft.Json;

namespace CZOA.WebAPI.Controllers
{
    [RoutePrefix("api/user")] //设置默认前缀
    public class UserController : SNApiController
    {
        private readonly IUserCtrl _ctrl;

        public UserController()
        {
            _ctrl = GetCtrl<IUserCtrl>("UserCtrl");
        }
        /// <summary>
        /// 获取用户session
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/session")] //默认前缀+Route
        public SNSession GetUserSesssion(decimal id)
        {
            var re = _ctrl.GetUserSession(id);
            return re;
        }

        /// <summary>
        /// 根据用户id返回相关角色域
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/userid/{userid}")]
        public DataTable GetUserDomainsByUserId(decimal userId)
        {
            var re = _ctrl.GetUserDomainsByUserId(userId);
            return re;
        }
        /// <summary>
        /// 添加一个用户及其用户域
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [Route("add")]
        public decimal Post(dynamic d)
        {
            var user = DynamicTo<T_USER>(d.User);
            var userDomains = d.UserDomains;
            IList<T_USER_DOMAIN> domains = null;
            if (userDomains != null)
            {
                domains = DynamicTo<IList<T_USER_DOMAIN>>(userDomains);
            }
            var re = _ctrl.AddUserWithDomain(user, domains);
            return re;
        }
        /// <summary>
        /// 更新一个用户及其用户域
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        public decimal Put(dynamic d)
        {
            var user = DynamicTo<T_USER>(d.User);
            var userDomains = d.UserDomains;
            IList<T_USER_DOMAIN> domains = null;
            if (userDomains != null)
            {
                domains = DynamicTo<IList<T_USER_DOMAIN>>(userDomains);
            }
            var re = _ctrl.UpdateUserWithUserDomain(user, domains);
            return re;
        }
        /// <summary>
        /// 获取指定域id的用户及域信息
        /// </summary>
        /// <param name="domainAt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{domainAt}")]
        public dynamic Get(decimal domainAt)
        {
            var re = _ctrl.GetUserAndUserDomain(domainAt);
            return re;
        }
        /// <summary>
        /// 获取指定域id的用户（带用户名）及域信息
        /// </summary>
        /// <param name="domainAt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/name/{domainAt}")]
        public dynamic GetUserName(decimal domainAt)
        {
            var re = _ctrl.GetUserAndUserName(domainAt);
            return re;
        }
        /// <summary>
        /// 与绩效系统同步用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SynchronousUser")]
        public decimal SynchronousUser()
        {
            var re = _ctrl.SynchronousUser();
            return re;
        }
        /// <summary>
        /// 与绩效系统同步用户域
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SynchronousUserDomain")]
        public decimal SynchronousUserDomain()
        {
            var re = _ctrl.SynchronousUserDomain();
            return re;
        }
    }
}
