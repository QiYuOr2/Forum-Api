using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApi.Models
{
    public enum StatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS,
        /// <summary>
        /// 服务器错误
        /// </summary>
        SERVER_ERROR,
        /// <summary>
        /// 用户操作错误
        /// </summary>
        OPERATION_ERROR
    }

    /// <summary>
    /// 响应数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseData<T>
    {
        public StatusCode Status { get; set; }
        public string Msg { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}