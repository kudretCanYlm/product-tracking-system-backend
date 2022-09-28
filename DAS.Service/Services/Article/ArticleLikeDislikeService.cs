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
    public interface IArticleLikeDislikeService
    {

    }
    public class ArticleLikeDislikeService:IArticleLikeDislikeService
    {
        private readonly IArticleLikeDislikeRepository articleLikeDislikeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleLikeDislikeValidation validator;

        public ArticleLikeDislikeService(IArticleLikeDislikeRepository articleLikeDislikeRepository, IUnitOfWork unitOfWork, IArticleLikeDislikeValidation validator)
        {
            this.articleLikeDislikeRepository = articleLikeDislikeRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }
    }
}
