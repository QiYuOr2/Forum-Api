using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApi.Models
{
    public enum StatusCode
    {
        SUCCESS,
        SERVER_ERROR, // 服务器错误
        OPERATION_ERROR // 用户操作错误
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
        public int Length { get; set; }
    }
}