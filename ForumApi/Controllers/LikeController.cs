using ForumApi.Common;
using ForumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ForumApi.Controllers
{
    public class LikeController : ApiController
    {
        private readonly ForumApiEntities db = new ForumApiEntities();

        /// <summary>
        /// 点赞 POST api/like/add 未测试*
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/like/add")]
        public ResponseData<object> AddLikeCount([FromBody] LikePostData postData)
        {
            ResponseData<object> responseData;

            if (SessionHelper.IsExist(postData.Guid))
            {
                // 文章点赞数增加
                ArticleTb article = db.ArticleTb.Where(a => a.articleId == postData.ArticleId).FirstOrDefault();

                article.likeCount++;

                // 查询记录是否存在
                var oldLikeItem = db.LikeTb.Where(l => l.articleId == postData.ArticleId && l.userId == postData.UserId);
                if (oldLikeItem == null)
                {
                    // 添加到点赞列表
                    LikeTb likeItem = new LikeTb()
                    {
                        userId = postData.UserId,
                        articleId = postData.ArticleId
                    };

                    try
                    {
                        db.LikeTb.Add(likeItem);
                        db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                        if (db.SaveChanges() > 0)
                        {
                            responseData = ResponseHelper<object>.SendSuccessResponse(new List<object> { article.likeCount });
                        }
                        else
                        {
                            responseData = ResponseHelper<object>.SendErrorResponse("点赞失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
                    }
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("已经点过一次赞了");
                }
            }
            else
            {
                responseData = ResponseHelper<object>.SendErrorResponse("未登录", Models.StatusCode.OPERATION_ERROR);
            }

            return responseData;
        }

        /// <summary>
        /// 取消点赞 POST api/like/delete 未测试*
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/like/delete")]
        public ResponseData<object> DeleteLikeCount([FromBody] LikePostData postData)
        {
            ResponseData<object> responseData;

            if (SessionHelper.IsExist(postData.Guid))
            {
                // 文章点赞数减少
                ArticleTb article = db.ArticleTb.Where(a => a.articleId == postData.ArticleId).FirstOrDefault();

                article.likeCount--;

                // 获取点赞列表
                LikeTb likeItem = db.LikeTb.Where(l => l.articleId == postData.ArticleId && l.userId == postData.UserId).First();

                try
                {
                    db.LikeTb.Remove(likeItem);
                    db.Entry(article).State = System.Data.Entity.EntityState.Modified;

                    if (db.SaveChanges() > 0)
                    {
                        responseData = ResponseHelper<object>.SendSuccessResponse(new List<object> { article.likeCount });
                    }
                    else
                    {
                        responseData = ResponseHelper<object>.SendErrorResponse("取消点赞失败");
                    }
                }
                catch (Exception ex)
                {
                    responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
                }
            }
            else
            {
                responseData = ResponseHelper<object>.SendErrorResponse("未登录", Models.StatusCode.OPERATION_ERROR);
            }

            return responseData;
        }
    }

    public class LikePostData
    {
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public string Guid { get; set; }
    }
}
