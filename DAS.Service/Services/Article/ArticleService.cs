using AutoMapper;
using DAS.Core.Infrastructure;
using DAS.Core.PagingAndFiltering;
using DAS.Core.Repository.Article;
using DAS.Core.Repository.Authentication;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
using DAS.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Article
{
    public interface IArticleService
    {
        ArticleDtoView GetArticleById(Guid articleId);
        ArticleEntity GetArticleEntityById(Guid articleId);
        IEnumerable<ArticleDtoView> GetAllArticles();
        object AddNewArticle(ref ArticleEntity article);
        IEnumerable<ArticleDtoView> GetUserArticles(Guid ownerId);
        IEnumerable<ArticleDtoView> GetUserArticles<TOrder>(Guid ownerId, int pageNumber, int pageSize, Expression<Func<ArticleEntity, TOrder>> orderBy);
        IPagedList<ArticleEntity> GetUserArticles(PageSearchArgs args);
        IEnumerable<ArticleDtoView> GetArticleByName(string name);

	}
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly ILoginRepository loginRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleValidation validator;

        public ArticleService(IArticleRepository articleRepository, ILoginRepository loginRepository, IUnitOfWork unitOfWork, IArticleValidation validator)
        {
            this.articleRepository = articleRepository;
            this.loginRepository = loginRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public object AddNewArticle(ref ArticleEntity article)
        {
            article.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(article))
            {
                articleRepository.Add(article);

                return SaveArticleWithMessage();
            }

            return validator.GetValidErrorMessages(article);

        }

        public ArticleDtoView GetArticleById(Guid articleId)
        {
            var article = articleRepository.GetArticleWithDto(x => x.Id == articleId && x.IsPublic);

            return article;
        }

        public ArticleEntity GetArticleEntityById(Guid articleId)
        {
            var article = articleRepository.GetById(articleId);
            return article;
        }

        public IEnumerable<ArticleDtoView> GetUserArticles(Guid ownerId)
        {
            var articles = articleRepository.GetArticlesWithDto(x => x.ArticleOwnerId == ownerId && x.IsPublic);

            return articles;
        }

        public IEnumerable<ArticleDtoView> GetUserArticles<TOrder>(Guid ownerId, int pageNumber, int pageSize, Expression<Func<ArticleEntity, TOrder>> orderBy)
        {
            var page = new Page(pageNumber, pageSize);

            var articles = articleRepository.GetArticlesPage(page, x=>x.IsPublic==true && x.ArticleOwnerId==ownerId, orderBy);

            return articles;
        }

        public IPagedList<ArticleEntity> GetUserArticles(PageSearchArgs args)
        {
            var articlePagedList = articleRepository.GetArticlesPagedList(args);

			return articlePagedList;

		}

        public IEnumerable<ArticleDtoView> GetArticleByName(string name)
        {
            var articles = articleRepository.GetArticlesByName(name);

            if(articles== null)
                return null;

            return Mapper.Map<IEnumerable<ArticleDtoView>>(articles);
        }

        public IEnumerable<ArticleDtoView> GetAllArticles()
        {
            var articles = articleRepository.GetArticlesWithDto(null);
            return articles;
        }


        private object SaveArticleWithMessage()
        {
            try
            {
                unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        private dynamic TransactionArticle()
        {
            try
            {
                unitOfWork.BeginTransaction();

                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();

                return ex.Message;
            }
        }

        private void SaveArticle()
        {
            unitOfWork.Commit();
        }


    }
}
