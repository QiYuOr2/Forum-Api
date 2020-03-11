using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApi.Common;
using ForumApi.Models;

namespace ForumApi.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private readonly ForumApiEntities db = new ForumApiEntities();

        /// <summary>
        /// 关键词查找 GET api/search?pagesize=2&pageindex=1&keyword=
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<object> SearchByKeyword(string keyword, int pageSize, int pageIndex)
        {
            ResponseData<object> responseData;

            try
            {
                var articleList = from a in db.ArticleTb
                                  where a.title.Contains(keyword)
                                  from u in db.RoleTb
                                  where u.roleId == a.authorId
                                  select new
                                  {
                                      a.articleId,
                                      a.title,
                                      a.content,
                                      a.publishTime,
                                      a.likeCount,
                                      a.viewCount,
                                      u.nickName,
                                      u.roleId
                                  };

                int totalCount = articleList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (articleList != null)
                {
                    // 按热度排序
                    articleList =
                        articleList
                        .OrderByDescending(a => a.viewCount + a.likeCount)
                        .ThenByDescending(a => a.publishTime)
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    List<object> res = new List<object>()
                {
                    new { articles = articleList, totalCount, totalPages }
                };

                    responseData = ResponseHelper<object>.SendSuccessResponse(res);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("没找到");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }
    }
}
