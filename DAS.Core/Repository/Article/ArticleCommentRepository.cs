using DAS.Core.Infrastructure;
using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Article
{
    public class ArticleCommentRepository:RepositoryBase<ArticleCommentEntity>,IArticleCommentRepository
    {
        public ArticleCommentRepository(DatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }

    public interface IArticleCommentRepository:IRepository<ArticleCommentEntity>
    {

    }
}
