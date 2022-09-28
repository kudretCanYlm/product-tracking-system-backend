using DAS.Core.Infrastructure;
using DAS.Core.Repository.Article;
using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Article
{
    public interface IArticleCommentService
    {

    }

    public class ArticleCommentService:IArticleLikeDislikeService
    {
        private readonly IArticleCommentRepository articleCommentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleLikeDislikeValidation validator;

        public ArticleCommentService(IArticleCommentRepository articleCommentRepository, IUnitOfWork unitOfWork, IArticleLikeDislikeValidation validator)
        {
            this.articleCommentRepository = articleCommentRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
    }
}
