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
        public ResponseData<object> ShowAllArticles()
        {
            ResponseData<object> responseData;

            try
            {
                // 联合查询 { 标题, 内容, 发布时间, 点赞数, 访问量, 作者昵称 }
                var articles = from a in db.ArticleTb
                               where a.isDel == false
                               from u in db.RoleTb
                               where u.roleId == a.authorId
                               select new { a.articleId, a.title, a.content, a.publishTime, a.likeCount, a.viewCount, u.nickName };

                if (articles != null)
                {
                    responseData = ResponseHelper<object>.SendSuccessResponse(articles.AsEnumerable<object>());
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
