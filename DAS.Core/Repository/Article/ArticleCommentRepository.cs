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
    public class ArticleCommentRepository : RepositoryBase<ArticleCommentEntity>, IArticleCommentRepository
    {
        public ArticleCommentRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public IEnumerable<ArticleCommentDto> GetDtos(Guid articleId)
        {
            return GetManyQuery(x => x.ArticleId == articleId && x.DeletedAt==null).Select(ArticleCommentRepositorySelects.articleCommmentViewDto).ToList();
        }
    }

    public interface IArticleCommentRepository : IRepository<ArticleCommentEntity>
    {
        IEnumerable<ArticleCommentDto> GetDtos(Guid articleId);
    }

    public  class ArticleCommentRepositorySelects
    {
        public static Expression<Func<ArticleCommentEntity, ArticleCommentDto>> articleCommmentViewDto = articleComment => new ArticleCommentDto { 
            
            Id=articleComment.Id,
            Comment=articleComment.Comment,
            CommentOwnerId=articleComment.CommentOwnerId,
            CommentOwnerName=articleComment.CommentOwner.Name,
            CommentOwnerSurname=articleComment.CommentOwner.Surname,
            CreatedAt=articleComment.CreatedAt
        };
    }
}
