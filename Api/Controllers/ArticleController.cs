using Api.Infrastructure.Attributes;
using DAS.Service.Services.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("article")]
    public class ArticleController : ApiController
    {
        IArticleService articleService;
        IArticleCommentService articleCommentService;
        IArticleLikeDislikeService articleLikeDislikeService;
        IArticleCommentReplyService articleCommentReplyService;

        public ArticleController(IArticleService articleService, IArticleCommentService articleCommentService, IArticleLikeDislikeService articleLikeDislikeService, IArticleCommentReplyService articleCommentReplyService)
        {
            this.articleService = articleService;
            this.articleCommentService = articleCommentService;
            this.articleLikeDislikeService = articleLikeDislikeService;
            this.articleCommentReplyService = articleCommentReplyService;
        }

        [Route("{id}"), HttpGet, CacheControl, AllowAnonymous]
        public HttpResponseMessage Get([FromUri] Guid id)
        {
            var article = articleService.GetArticleById(id);

            if (article == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "didn't find article");
            //will add image controller
            return Request.CreateResponse(HttpStatusCode.Created, "found");
        }
    }
}
