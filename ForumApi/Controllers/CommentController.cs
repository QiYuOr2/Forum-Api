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
    }
}
