using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ForumApi.Common
{
    public class SessionHelper
    {
        /// <summary>
        /// 判断Session是否存在
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsExist(string guid)
        {
            if (HttpContext.Current.Session[guid] != null)
            {
                return true;
            }
            return false;
        }
    }
}