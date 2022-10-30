using DAS.Core.Infrastructure;
using DAS.Core.Repository.Article;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
using DAS.Service.common;
using System;
using System.Collections.Generic;

namespace DAS.Service.Services.Article
{
    public interface IArticleCommentService
    {
        IEnumerable<ArticleCommentEntity> GetComments(Guid articleId);
        ArticleCommentEntity GetComment(Guid commentId);
        IEnumerable<ArticleCommentDto> GetCommentsDto(Guid articleId);
        object AddComment(ref ArticleCommentEntity articleComment);

        object DeleteComment(ArticleCommentEntity articleComment);
    }

    public class ArticleCommentService:IArticleCommentService
    {
        private readonly IArticleCommentRepository articleCommentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleCommentValidation validator;

        public ArticleCommentService(IArticleCommentRepository articleCommentRepository, IUnitOfWork unitOfWork, IArticleCommentValidation validator)
        {
            this.articleCommentRepository = articleCommentRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public IEnumerable<ArticleCommentEntity> GetComments(Guid articleId)
        {
            return articleCommentRepository.GetMany(x => x.ArticleId == articleId && x.DeletedAt.ToString().Length<1);
        }
        public ArticleCommentEntity GetComment(Guid commentId)
        {
            return articleCommentRepository.GetById(commentId);
        }

        public IEnumerable<ArticleCommentDto> GetCommentsDto(Guid articleId)
        {
            return articleCommentRepository.GetDtos(articleId);
        }

        public object AddComment(ref ArticleCommentEntity articleComment)
        {
            articleComment.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(articleComment))
            {
                articleCommentRepository.Add(articleComment);

                return SaveArticleCommentWithMessage();
            }

            return validator.GetValidErrorMessages(articleComment);
        }

        public object DeleteComment(ArticleCommentEntity articleComment)
        {
            articleComment.DeletedAt = DateTime.Now;

            articleCommentRepository.Update(articleComment);
            return SaveArticleCommentWithMessage();            
        }
        private object SaveArticleCommentWithMessage()
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


    }
}
