using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApi.Models;
using ForumApi.Common;

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
                responseData = ResponseHelper<RoleTb>.SendErrorResponse(ex.ToString());
            }

            return responseData;
        }
    }
}
