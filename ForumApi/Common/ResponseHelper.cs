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
                Status = StatusCode.SUCCESS,
                Msg = "SUCCESS",
                Data = data
            };

            return responseData;
        }

        /// <summary>
        /// 操作失败时响应
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="statusCode">错误状态</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static ResponseData<T> SendErrorResponse(string errorMsg, StatusCode statusCode = StatusCode.SERVER_ERROR, IEnumerable<T> data = null)
        {
            ResponseData<T> responseData = new ResponseData<T>()
            {
                Status = statusCode,
                Msg = "ERROR: " + errorMsg,
                Data = data
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
        public static IEnumerable<object> SetLoginMsg(string guid, string account, string msg = null)
        {
            var res = new List<object>
            {
                new { Guid = guid, Account = account, Msg = msg }
            };

            return res;
        }
    }
}