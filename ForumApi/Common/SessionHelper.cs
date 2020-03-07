using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApi.Common
{
    public class SessionHelper
    {
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