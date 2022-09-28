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
    public interface IArticleCommentReplyService
    {

    }

    public class ArticleCommentReplyService:IArticleCommentReplyService
    {
        private readonly IArticleCommentReplyRepository articleCommentReplyRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleCommentReplyValidation validator;

        public ArticleCommentReplyService(IArticleCommentReplyRepository articleCommentReplyRepository, IUnitOfWork unitOfWork, IArticleCommentReplyValidation validator)
        {
            this.articleCommentReplyRepository = articleCommentReplyRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
    }
}
