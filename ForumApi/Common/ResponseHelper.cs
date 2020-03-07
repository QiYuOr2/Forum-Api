using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ForumApi.Models;

namespace ForumApi.Common
{
    public class ResponseHelper<T>
    {
        /// <summary>
        /// 操作成功时响应
        /// </summary>
        /// <param name="data">成功数据</param>
        /// <returns></returns>
        public static ResponseData<T> SendSuccessResponse(IEnumerable<T> data = null)
        {
            ResponseData<T> responseData = new ResponseData<T>()
            {
                Status = 0,
                Msg = "SUCCESS",
                Data = data,
                Length = data == null ? 0 : data.Count()
            };

            return responseData;
        }

        /// <summary>
        /// 操作失败时响应
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="data">错误数据</param>
        /// <returns></returns>
        public static ResponseData<T> SendErrorResponse(string errorMsg, int statusCode = 1, IEnumerable<T> data = null)
        {
            ResponseData<T> responseData = new ResponseData<T>()
            {
                Status = statusCode,
                Msg = "ERROR: " + errorMsg,
                Data = data,
                Length = data == null ? 0 : data.Count()
            };

            return responseData;
        }

        /// <summary>
        /// 打包登陆数据
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="guid">guid</param>
        /// <param name="account">账号</param>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public static IEnumerable<object> SetLoginMsg(string status, string guid, string account, string msg = null)
        {
            var res = new List<object>
            {
                new { Status = status, Guid = guid, Account = account, Msg = msg }
            };

            return res;
        }
    }
}