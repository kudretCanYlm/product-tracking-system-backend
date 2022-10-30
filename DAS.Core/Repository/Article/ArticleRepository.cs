using DAS.Core.Infrastructure;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Article
{
    public class ArticleRepository : RepositoryBase<ArticleEntity>, IArticleRepository
    {
        public ArticleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public IEnumerable<ArticleDtoView> GetArticlesWithDto(Expression<Func<ArticleEntity, bool>> where)
        {
            var articles = GetManyQuery(where == null ? x => x.IsPublic == true : where).Select(ArticleRepositorySelects.articleViewDto).ToList();
            return articles;
        }

        public ArticleDtoView GetArticleWithDto(Expression<Func<ArticleEntity, bool>> where)
        {
            var article = GetManyQuery(where == null ? x => x.IsPublic == true : where).Select(ArticleRepositorySelects.articleViewDto).FirstOrDefault();

            return article;
        }
    }

    public interface IArticleRepository : IRepository<ArticleEntity>
    {
        ArticleDtoView GetArticleWithDto(Expression<Func<ArticleEntity, bool>> where);
        IEnumerable<ArticleDtoView> GetArticlesWithDto(Expression<Func<ArticleEntity, bool>> where);
    }

    public class ArticleRepositorySelects
    {
        public static Expression<Func<ArticleEntity, ArticleDtoView>> articleViewDto = article => new ArticleDtoView
        {
            Id = article.Id,
            Summary = article.Summary,
            ArticleContent = article.ArticleContent,
            ArticleTitle = article.ArticleTitle,
            CreatedAt = article.CreatedAt,
            OwnerId = article.ArticleOwner.Id,
            OwnerName = article.ArticleOwner.Name,
            OwnerSurname = article.ArticleOwner.Surname
        };
    }
}
