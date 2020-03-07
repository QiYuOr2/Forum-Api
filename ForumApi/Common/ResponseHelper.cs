using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                Length = data.Count()
            };

            return responseData;
        }

        /// <summary>
        /// 操作失败时响应
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="data">错误数据</param>
        /// <returns></returns>
        public static ResponseData<T> SendErrorResponse(string errorMsg, IEnumerable<T> data = null)
        {
            ResponseData<T> responseData = new ResponseData<T>()
            {
                Status = 1,
                Msg = "ERROR: " + errorMsg,
                Data = data,
                Length = data.Count()
            };

            return responseData;
        }
    }
}