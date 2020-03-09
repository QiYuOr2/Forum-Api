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
    public class ArticleController : ApiController
    {
        private readonly ForumEntities db = new ForumEntities();

        /// <summary>
        /// 查询所有文章 GET api/article
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/article")]
        public ResponseData<object> ShowArticles()
        {
            ResponseData<object> responseData;

            try
            {
                // 联合查询 { 标题, 内容, 发布时间, 点赞数, 访问量, 作者昵称 }
                var articleList = from a in db.ArticleTb
                                  where a.isDel == false
                                  from u in db.RoleTb
                                  where u.roleId == a.authorId
                                  select new { a.articleId, a.title, a.content, a.publishTime, a.likeCount, a.viewCount, u.nickName };

                if (articleList != null)
                {
                    responseData = ResponseHelper<object>.SendSuccessResponse(articleList.AsEnumerable<object>());
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("暂无文章数据");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 按条件分页查询 GET api/article?pageSize=10&pageIndex=1&isUseTime=true
        /// </summary>
        /// <param name="pageSize">页面容量</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="isUseTime">是否按时间排序</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/article")]
        public ResponseData<object> ShowArticlesOrderByPopularOrPublishTime(int pageSize, int pageIndex, bool isUseTime)
        {
            ResponseData<object> responseData;

            try
            {
                var articleList = from a in db.ArticleTb
                                  where a.isDel == false
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
                                      u.nickName
                                  };

                int totalCount = articleList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (articleList != null)
                {
                    if (isUseTime)
                    {
                        // 按时间排序
                        articleList =
                            articleList
                            .OrderByDescending(a => a.publishTime)
                            .ThenByDescending(a => a.viewCount + a.likeCount)
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    else
                    {
                        // 按热度排序
                        articleList =
                            articleList
                            .OrderByDescending(a => a.viewCount + a.likeCount)
                            .ThenByDescending(a => a.publishTime)
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }

                    List<object> res = new List<object>
                    {
                        new { articles= articleList, totalCount, totalPages }
                    };

                    responseData = ResponseHelper<object>.SendSuccessResponse(res);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("暂无文章数据");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 查找某用户文章 GET api/article?pageSize=10&pageIndex=1&userId=1&isUseTime=true
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <param name="isUseTime"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/article")]
        public ResponseData<object> FindArticleByUserId(int pageSize, int pageIndex, int userId, bool isUseTime)
        {
            ResponseData<object> responseData;

            try
            {
                var articleList = from a in db.ArticleTb
                                  where a.isDel == false
                                  where a.authorId == userId
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
                                      u.nickName
                                  };

                int totalCount = articleList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (articleList != null)
                {
                    if (isUseTime)
                    {
                        // 按时间排序
                        articleList =
                            articleList
                            .OrderByDescending(a => a.publishTime)
                            .ThenByDescending(a => a.viewCount + a.likeCount)
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    else
                    {
                        // 按热度排序
                        articleList =
                            articleList
                            .OrderByDescending(a => a.viewCount + a.likeCount)
                            .ThenByDescending(a => a.publishTime)
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }

                    List<object> res = new List<object>
                    {
                        new { articles= articleList, totalCount, totalPages }
                    };

                    responseData = ResponseHelper<object>.SendSuccessResponse(res);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("暂无文章数据");
                }

            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 展示某篇文章 GET api/article?articleId=1
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/article")]
        public ResponseData<object> ShowArticleById(int articleId)
        {
            ResponseData<object> responseData;

            try
            {
                var article = (from a in db.ArticleTb
                              where a.isDel == false
                              where a.articleId == articleId
                              from u in db.RoleTb
                              where u.roleId == a.authorId
                              select new
                              {
                                  a.articleId,
                                  a.title,
                                  a.content,
                                  a.likeCount,
                                  a.viewCount,
                                  a.publishTime,
                                  u.nickName
                              }).First();
                if (article != null)
                {
                    List<object> res = new List<object>()
                    {
                        article
                    };
                    responseData = ResponseHelper<object>.SendSuccessResponse(res);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("暂无文章数据");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 发布文章 POST api/article/publish
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/article/publish")]
        public ResponseData<ArticleTb> PublishNewArticle([FromBody] ArticlePostData postData)
        {
            ResponseData<ArticleTb> responseData;

            if (SessionHelper.IsExist(postData.Guid))
            {

                ArticleTb article = new ArticleTb()
                {
                    title = postData.Title,
                    content = postData.Content,
                    publishTime = DateTime.Now,
                    authorId = postData.AuthorId
                };

                try
                {
                    db.ArticleTb.Add(article);
                    if (db.SaveChanges() > 0)
                    {
                        responseData = ResponseHelper<ArticleTb>.SendSuccessResponse();
                    }
                    else
                    {
                        responseData = ResponseHelper<ArticleTb>.SendErrorResponse("发布失败");
                    }
                }
                catch (Exception ex)
                {
                    responseData = ResponseHelper<ArticleTb>.SendErrorResponse(ex.Message);
                }
            }
            else
            {
                responseData = ResponseHelper<ArticleTb>.SendErrorResponse("未登录", Models.StatusCode.OPERATION_ERROR);
            }

            return responseData;
        }

        /// <summary>
        /// 删除文章 POST api/article/delete 未测试*
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/article/delete")]
        public ResponseData<ArticleTb> DeleteArticle([FromBody] ArticlePostData postData)
        {
            ResponseData<ArticleTb> responseData;

            if (SessionHelper.IsExist(postData.Guid))
            {
                var article = db.ArticleTb.Where(a => a.articleId == postData.ArticleId).FirstOrDefault();

                article.isDel = true;

                try
                {
                    db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        responseData = ResponseHelper<ArticleTb>.SendSuccessResponse();
                    }
                    else
                    {
                        responseData = ResponseHelper<ArticleTb>.SendErrorResponse("修改失败");
                    }
                }
                catch (Exception ex)
                {
                    responseData = ResponseHelper<ArticleTb>.SendErrorResponse(ex.Message);
                }
            }
            else
            {
                responseData = ResponseHelper<ArticleTb>.SendErrorResponse("未登录", Models.StatusCode.OPERATION_ERROR);
            }

            return responseData;
        }

        /// <summary>
        /// 点赞 POST api/article/like 未测试*
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/article/like")]
        public ResponseData<ArticleTb> AddLikeCount([FromBody] ArticlePostData postData)
        {
            ResponseData<ArticleTb> responseData;

            if (SessionHelper.IsExist(postData.Guid))
            {
                ArticleTb article = db.ArticleTb.Where(a => a.articleId == postData.ArticleId).FirstOrDefault();

                article.likeCount++;

                // 添加到点赞列表
                LikeTb likeItem = new LikeTb()
                {
                    userId = postData.UserId,
                    articleId = postData.ArticleId
                };
                db.LikeTb.Add(likeItem);

                try
                {
                    db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        responseData = ResponseHelper<ArticleTb>.SendSuccessResponse();
                    }
                    else
                    {
                        responseData = ResponseHelper<ArticleTb>.SendErrorResponse("修改失败");
                    }
                }
                catch (Exception ex)
                {
                    responseData = ResponseHelper<ArticleTb>.SendErrorResponse(ex.Message);
                }
            }
            else
            {
                responseData = ResponseHelper<ArticleTb>.SendErrorResponse("未登录", Models.StatusCode.OPERATION_ERROR);
            }

            return responseData;
        }
    }

    /// <summary>
    /// 文章操作需要接收的数据
    /// </summary>
    public class ArticlePostData
    {
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string Guid { get; set; }
    }
}
