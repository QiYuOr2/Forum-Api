using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApi.Models
{
    /// <summary>
    /// 响应数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseData<T>
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int Length { get; set; }
    }
}