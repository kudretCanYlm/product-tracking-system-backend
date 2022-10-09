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
        [HttpGet, JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage Get()
        {
            var articles = articleService.GetAllArticles();

            if (articles == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "didn't find any article");

            return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<IEnumerable<ArticleViewModel>>(articles));
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
    }
}
