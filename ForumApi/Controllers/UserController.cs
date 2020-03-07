using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApi.Models;
using ForumApi.Common;
using System.Web;

namespace ForumApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly ForumEntities db = new ForumEntities();

        /// <summary>
        /// 注册 POST api/register
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/register")]
        public ResponseData<RoleTb> UserRegister([FromBody] RoleTb entity)
        {
            ResponseData<RoleTb> responseData;

            RoleTb user = new RoleTb
            {
                account = entity.account,
                nickName = entity.nickName,
                pwd = entity.pwd
            };

            try
            {
                db.RoleTb.Add(user);
                if (db.SaveChanges() > 0)
                {
                    responseData = ResponseHelper<RoleTb>.SendSuccessResponse();
                }
                else
                {
                    responseData = ResponseHelper<RoleTb>.SendErrorResponse("添加失败");
                }
            }
            catch (Exception ex)
            {
                if (db.RoleTb.Where(u => u.isDel == false && u.account == user.account) != null)
                {
                    responseData = ResponseHelper<RoleTb>.SendErrorResponse("账号已存在", Models.StatusCode.OPERATION_ERROR);
                }
                else
                {
                    responseData = ResponseHelper<RoleTb>.SendErrorResponse(ex.Message);
                }
            }

            return responseData;
        }

        /// <summary>
        /// 登陆 api/login
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/Login")]
        public ResponseData<object> UserLogin([FromBody] RoleTb entity)
        {
            ResponseData<object> responseData;

            RoleTb user = new RoleTb
            {
                account = entity.account,
                pwd = entity.pwd
            };

            try
            {
                var loginUser =
                    db.RoleTb
                    .Where(u => u.isDel == false && u.account == user.account && u.pwd == user.pwd)
                    .FirstOrDefault();

                if (loginUser != null)
                {
                    string guid = Guid.NewGuid().ToString();

                    var loginUserMsg = ResponseHelper<object>.SetLoginMsg("SUCCESS", guid, loginUser.account);

                    HttpContext.Current.Session[guid] = loginUser.account;
                    HttpContext.Current.Session.Timeout = 30;

                    responseData = ResponseHelper<object>.SendSuccessResponse(loginUserMsg);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("登陆失败，请检查账号或密码是否正确");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse("登陆失败: " + ex.Message);
            }

            return responseData;
        }
    }
}
