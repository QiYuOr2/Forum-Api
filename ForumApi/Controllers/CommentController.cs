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
    public class CommentController : ApiController
    {
        private readonly ForumApiEntities db = new ForumApiEntities();

        /// <summary>
        /// 获得某文章评论 GET api/comment?pagesize=2&pageindex=1&articleId=1
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/comment")]
        public ResponseData<object> GetComments(int pageSize, int pageIndex, int articleId)
        {
            ResponseData<object> responseData;

            try
            {
                var commentList = from c in db.CommentTb
                                  where c.articleId == articleId
                                  from a in db.RoleTb
                                  where c.authorId == a.roleId
                                  select new
                                  {
                                      c.commentId,
                                      c.publishTime,
                                      c.content,
                                      c.articleId,
                                      c.likeCount,
                                      a.nickName
                                  };


                int totalCount = commentList.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                if (commentList != null)
                {
                    commentList = commentList
                        .OrderByDescending(c => c.publishTime)
                        .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    List<object> res = new List<object>()
                    {
                        new { comments= commentList, totalCount, totalPages }
                    };

                    responseData = ResponseHelper<object>.SendSuccessResponse(res);
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("暂无评论数据");
                }
            }
            catch (Exception ex)
            {
                responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
            }

            return responseData;
        }

        /// <summary>
        /// 添加评论 POST api/comment/add 未测试*
        /// </summary>
        /// <param name="commentData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/comment/add")]
        public ResponseData<object> AddComment([FromBody] CommentPostData commentData)
        {
            ResponseData<object> responseData;
            if (SessionHelper.IsExist(commentData.Guid))
            {
                CommentTb comment = new CommentTb()
                {
                    articleId = commentData.ArticleId,
                    authorId = commentData.AuthorId,
                    publishTime = DateTime.Now,
                    content = commentData.Content,
                };

                try
                {
                    db.CommentTb.Add(comment);
                    if (db.SaveChanges() > 0)
                    {
                        responseData = ResponseHelper<object>.SendSuccessResponse();
                    }
                    else
                    {
                        responseData = ResponseHelper<object>.SendErrorResponse("评论失败");
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

        /// <summary>
        /// 删除评论 POST api/comment/delete 未测试*
        /// </summary>
        /// <param name="commentData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/comment/delete")]
        public ResponseData<object> DeleteComments([FromBody] CommentPostData commentData)
        {
            ResponseData<object> responseData;

            if (SessionHelper.IsExist(commentData.Guid))
            {
                CommentTb comment = 
                    db.CommentTb
                    .Where(c => c.isDel == false && c.commentId == commentData.CommentId && c.authorId == commentData.AuthorId)
                    .First();

                if (comment != null)
                {
                    comment.isDel = true;
                    try
                    {
                        db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                        if (db.SaveChanges() > 0)
                        {
                            responseData = ResponseHelper<object>.SendSuccessResponse();
                        }
                        else
                        {
                            responseData = ResponseHelper<object>.SendErrorResponse("删除评论失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        responseData = ResponseHelper<object>.SendErrorResponse(ex.Message);
                    }
                }
                else
                {
                    responseData = ResponseHelper<object>.SendErrorResponse("无此评论数据");
                }
            }
            else
            {
                responseData = ResponseHelper<object>.SendErrorResponse("未登录", Models.StatusCode.OPERATION_ERROR);
            }

            return responseData;
        }
    }

    public class CommentPostData
    {
        public string Guid { get; set; }
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
    }
}
