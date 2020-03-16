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
        /// 关键词查找文章 GET api/search?pagesize=2&pageindex=1&keyword=
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
                                  where a.isDel == false
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

        /// <summary>
        /// 关键词查找用户 GET api/search/user?pagesize=2&pageindex=1&keyword=
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public ResponseData<object> SearchUserByKeyword(string keyword, int pageSize, int pageIndex)
        {
            ResponseData<object> responseData;

            try
            {
                var userList = from u in db.RoleTb
                               where u.isDel == false
                               where u.nickName.Contains(keyword)
                               select new
                               {
                                   u.roleId,
                                   u.nickName,
                                   u.avatarUrl,
                                   u.powerNum,
                                   u.account,
                                   u.pwd
                               };

                int totalCount = userList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (userList != null)
                {
                    // 按热度排序
                    userList =
                        userList
                        .OrderBy(u => u.roleId)
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    List<object> res = new List<object>()
                {
                    new { users = userList, totalCount, totalPages }
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

        /// <summary>
        /// 权限查找用户 GET api/search/user?pagesize=2&pageindex=1powerNum=
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public ResponseData<object> SearchUserByPowerNum(int powerNum, int pageSize, int pageIndex)
        {
            ResponseData<object> responseData;

            try
            {
                var userList = from u in db.RoleTb
                               where u.isDel == false
                               where u.powerNum == powerNum
                               select new
                               {
                                   u.roleId,
                                   u.nickName,
                                   u.avatarUrl,
                                   u.powerNum,
                                   u.account,
                                   u.pwd
                               };

                int totalCount = userList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (userList != null)
                {
                    // 按热度排序
                    userList =
                        userList
                        .OrderBy(u => u.roleId)
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    List<object> res = new List<object>()
                {
                    new { users = userList, totalCount, totalPages }
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

        /// <summary>
        /// 账户查找用户 GET api/search/user?pagesize=2&pageindex=1account=
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public ResponseData<object> SearchUserByAccount(string account, int pageSize, int pageIndex)
        {
            ResponseData<object> responseData;

            try
            {
                var userList = from u in db.RoleTb
                               where u.isDel == false
                               where u.nickName.Contains(account)
                               select new
                               {
                                   u.roleId,
                                   u.nickName,
                                   u.avatarUrl,
                                   u.powerNum,
                                   u.account,
                                   u.pwd
                               };

                int totalCount = userList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (userList != null)
                {
                    userList =
                        userList
                        .OrderBy(u => u.roleId)
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    List<object> res = new List<object>()
                {
                    new { users = userList, totalCount, totalPages }
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

        /// <summary>
        /// 查找甲骨文
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("oracle")]
        public ResponseData<object> SearchOracle(string keyword)
        {
            ResponseData<object> responseData;

            try
            {
                var charData = db.CharTb.Where(c => c.CharMsg == keyword).Select(c => new { c.CharId, c.CharUrl, c.CharMsg });

                if (charData != null)
                {
                    responseData = ResponseHelper<object>.SendSuccessResponse(charData);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("抱歉，数据库中暂未收录此文字");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 分页展示甲骨文 GET api/search/oracle?pagesize=2&pageindex=1
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("oracle")]
        public ResponseData<object> SearchOracle(int pageSize, int pageIndex)
        {
            ResponseData<object> responseData;

            try
            {
                var charData = from c in db.CharTb
                               select new
                               {
                                   c.CharId,
                                   c.CharUrl,
                                   c.CharMsg
                               };

                int totalCount = charData.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (charData != null)
                {
                    charData =
                        charData
                        .OrderBy(c => c.CharId)
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize);

                    List<object> res = new List<object>()
                {
                    new { charData, totalCount, totalPages }
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
