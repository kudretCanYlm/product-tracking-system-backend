using DAS.Core.Infrastructure;
using DAS.Core.Repository.Article;
using DAS.Core.Repository.Authentication;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
using DAS.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Article
{
    public interface IArticleService
    {
        ArticleDtoView GetArticleById(Guid articleId);
        IEnumerable<ArticleDtoView> GetAllArticles();
        object AddNewArticle(ref ArticleEntity article);
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
