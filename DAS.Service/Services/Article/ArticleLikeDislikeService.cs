using DAS.Core.Infrastructure;
using DAS.Core.Repository.Article;
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
    public interface IArticleLikeDislikeService
    {
        object LikeDislikeArticle(ArticleLikeDislikeEntity articleLikeDislikeEntity);
        ArticleLikeDislikeEntity GetMyStatus(Guid articleId,Guid userId);
    }
    public class ArticleLikeDislikeService : IArticleLikeDislikeService
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

        public object LikeDislikeArticle(ArticleLikeDislikeEntity articleLikeDislikeEntity)
        {
            articleLikeDislikeEntity.CreatedAt = DateTime.Now;

            if (validator.IsValidEntity(articleLikeDislikeEntity))
            {
                string message;

                if (articleLikeDislikeEntity.Isliked)
                {
                    
                    //like sets
                    if(articleLikeDislikeRepository.isArticleLikedOrDisliked(articleLikeDislikeEntity))
                    {
                        //delete myliked row
                        articleLikeDislikeRepository.DeleteLikeOrDislike(articleLikeDislikeEntity);
                        message = "liked is removed";
                    }
                    else
                    {
                        //remove mydisliked row
                        articleLikeDislikeRepository.DeleteOpposite(articleLikeDislikeEntity);

                        //add meliked row
                        articleLikeDislikeRepository.Add(articleLikeDislikeEntity);
                        message = "liked is added";
                    }
                }
                else
                {
                    //dislike sets
                    if(articleLikeDislikeRepository.isArticleLikedOrDisliked(articleLikeDislikeEntity))
                    {
                        //remove mydisliked row
                        articleLikeDislikeRepository.DeleteLikeOrDislike(articleLikeDislikeEntity);
                        message = "disliked is removed";
                    }
                    else
                    {
                        //remove liked row
                        articleLikeDislikeRepository.DeleteOpposite(articleLikeDislikeEntity);
                        //add mydisliked row
                        articleLikeDislikeRepository.Add(articleLikeDislikeEntity);
                        message =  "disliked is added" ;
                    }
                }

                try
                {
                    SaveArticleLikeDislike();
                }
                catch (Exception)
                {

                    return false;
                }


                return message;
            }

            return validator.GetValidErrorMessages(articleLikeDislikeEntity);
        }

        public ArticleLikeDislikeEntity GetMyStatus(Guid articleId, Guid userId)
        {
            return articleLikeDislikeRepository.Get(x => x.DeletedAt == null && x.ArticleId==articleId && x.ArticleLikeDislikeOwnerId==userId) ?? null;
        }

        private object SaveArticleLikeDislikeWithMessage()
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

        private void SaveArticleLikeDislike()
        {
            unitOfWork.Commit();
        }

        private dynamic TransactionArticleLikeDislike()
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

        
    }
}
