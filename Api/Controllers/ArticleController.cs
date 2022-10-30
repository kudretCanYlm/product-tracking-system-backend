using Api.Infrastructure.Attributes;
using Api.Models.Article;
using AutoMapper;
using DAS.Model.Base.Enums;
using DAS.Model.Model.Article;
using DAS.Model.Model.Enums;
using DAS.Service.Services.Article;
using DAS.Service.Services.Authentication;
using DAS.Service.Services.Media;
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
        ILoginService loginService;
        IArticleCommentService articleCommentService;
        IArticleLikeDislikeService articleLikeDislikeService;
        IArticleCommentReplyService articleCommentReplyService;

        public ArticleController(IArticleService articleService, ILoginService loginservice, IArticleCommentService articleCommentService, IArticleLikeDislikeService articleLikeDislikeService, IArticleCommentReplyService articleCommentReplyService)
        {
            this.articleService = articleService;
            this.loginService = loginservice;
            this.articleCommentService = articleCommentService;
            this.articleLikeDislikeService = articleLikeDislikeService;
            this.articleCommentReplyService = articleCommentReplyService;
        }

        [Route("{id}"), HttpGet, CacheControl, AllowAnonymous]
        public HttpResponseMessage Get([FromUri] Guid id)
        {
            var article = articleService.GetArticleById(id);

            if (article == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "didn't find any article");

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<ArticleViewModel>(article));
        }

        [Route("getUserArticles/{id}"), JwtAuthentication(RoleEnum.Admin), CacheControl]
        public HttpResponseMessage GetUserArticles([FromUri] Guid id)
        {
            var articles = articleService.GetUserArticles(id);

            if (articles == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "didn't find any article");

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<IEnumerable<ArticleViewModel>>(articles));

        }

        [Route("likeOrDislikeArticle"), JwtAuthentication(RoleEnum.Admin),HttpPost]
        public HttpResponseMessage LikeArticle([FromBody] ArticleLikeDislikePostModel articleLikeDislikePostModel)
        {
            if (articleLikeDislikePostModel == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ArticleLikeDislikePostModel is null");

            string userName = ActionContext.RequestContext.Principal.Identity.Name;
            var owner = loginService.GetUser(x => x.Username == userName);
            var article = articleService.GetArticleEntityById(articleLikeDislikePostModel.ArticleId);

            var articleLikeDislikeEntity = Mapper.Map<ArticleLikeDislikeEntity>(articleLikeDislikePostModel);

            if(owner==null || article==null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "owner or/and article is/are null");


            articleLikeDislikeEntity.ArticleLikeDislikeOwner = owner;
            articleLikeDislikeEntity.Article = article;

            var result = articleLikeDislikeService.LikeDislikeArticle(articleLikeDislikeEntity);

            if (result is false)
                return Request.CreateResponse(HttpStatusCode.BadRequest,"There is an Error");

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [Route("IsILikeIt/{id}"),JwtAuthentication(RoleEnum.Admin),HttpGet]
        public HttpResponseMessage IsILikeIt([FromUri] Guid id)
        {
            string userName = ActionContext.RequestContext.Principal.Identity.Name;
            Guid userId = loginService.GetUserId(userName).Value;

            var status = articleLikeDislikeService.GetMyStatus(id, userId);

            if (status == null)
                return Request.CreateResponse(HttpStatusCode.Accepted,"didn't find any like or dislike");
            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<ArticleLikeDislikeSingleViewModel>(status));
        }

        [HttpPost, JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage Post([FromBody] ArticlePostModel articlePostModel)
        {
            if (articlePostModel == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "article is null");

            string userName = ActionContext.RequestContext.Principal.Identity.Name;
            var user = loginService.GetUser(x => x.Username == userName);

            var article = Mapper.Map<ArticlePostModel, ArticleEntity>(articlePostModel);
            article.ArticleOwner = user;

            object status = articleService.AddNewArticle(ref article);

            if (status is true)
                return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<ArticleViewModel>(article));

            return Request.CreateResponse(HttpStatusCode.BadRequest, status.ToString());
        }

        [HttpGet,Route("getComments/{articleId}"),JwtAuthentication(RoleEnum.Admin),CacheControl]
        public HttpResponseMessage GetArticleComments([FromUri] Guid articleId) 
        {
            var articleComments = articleCommentService.GetCommentsDto(articleId);

            if (articleComments.Count() > 0)
                return Request.CreateResponse(Mapper.Map<IEnumerable<ArticleCommentViewModel>>(articleComments));

            return Request.CreateResponse(HttpStatusCode.BadRequest, "there isn't any comment in this article");
        }

        [HttpPost,Route("addComment"),JwtAuthentication(RoleEnum.Admin),CacheControl]
        public HttpResponseMessage PostComment([FromBody] ArticleCommentPostModel articleCommentPost)
        {
            if(articleCommentPost==null)
                return  Request.CreateResponse(HttpStatusCode.BadRequest, "article comment is null");

            var articleComment = Mapper.Map<ArticleCommentEntity>(articleCommentPost);

            string userName = ActionContext.RequestContext.Principal.Identity.Name;
            var user = loginService.GetUser(x => x.Username == userName);
            var article = articleService.GetArticleEntityById(articleComment.ArticleId);

            if (user == null || article == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "user or/and article is/are not found");
            
            articleComment.CommentOwner= user;
            articleComment.Article = article;

            var result = articleCommentService.AddComment(ref articleComment);

            if (result is true)
                return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<ArticleCommentViewModel>(articleComment));

            return Request.CreateResponse(HttpStatusCode.BadRequest, result);

        }

        [HttpDelete,Route("deleteComment/{commentId}"),JwtAuthentication(RoleEnum.Admin),CacheControl]
        public HttpResponseMessage DeleteComment([FromUri] Guid commentId)
        {
            var myComment = articleCommentService.GetComment(commentId);

            string userName = ActionContext.RequestContext.Principal.Identity.Name;

            Guid userId = loginService.GetUserId(userName).Value;

            if (myComment==null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "user comment is not found");

            if (myComment.CommentOwnerId != userId)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "this user is not owner of this comment");

            var result = articleCommentService.DeleteComment(myComment);

            if (result is true)
                return Request.CreateResponse(HttpStatusCode.Accepted, $"{myComment.Id} deleted");
            return Request.CreateResponse(HttpStatusCode.BadRequest, result.ToString());
        }

    }
}
