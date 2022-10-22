using DAS.Core.Infrastructure;
using DAS.Model.Model.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Core.Repository.Article
{
    public class ArticleLikeDislikeRepository : RepositoryBase<ArticleLikeDislikeEntity>, IArticleLikeDislikeRepository
    {
        public ArticleLikeDislikeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public bool isArticleLikedOrDisliked(ArticleLikeDislikeEntity articleLikeDislikeEntity)
        {
            return GetManyQuery(
                    x => x.ArticleId == articleLikeDislikeEntity.Article.Id
                    &&
                    x.ArticleLikeDislikeOwnerId == articleLikeDislikeEntity.ArticleLikeDislikeOwner.Id
                    &&
                    x.Isliked == articleLikeDislikeEntity.Isliked
                    &&
                    x.DeletedAt.Value.ToString().Length <= 4
                   ).Count() > 0;
        }

        public void DeleteLikeOrDislike(ArticleLikeDislikeEntity articleLikeDislikeEntity)
        {
            var entityDelete = Get(
                    x => x.ArticleId == articleLikeDislikeEntity.Article.Id
                    &&
                    x.ArticleLikeDislikeOwnerId == articleLikeDislikeEntity.ArticleLikeDislikeOwner.Id
                    &&
                    x.Isliked == articleLikeDislikeEntity.Isliked
                    &&
                    x.DeletedAt == null
                );

            if (entityDelete != null)
            {
                entityDelete.DeletedAt = DateTime.Now;

                Update(entityDelete);
            }
        }

        public void DeleteOpposite(ArticleLikeDislikeEntity articleLikeDislikeEntity)
        {
            var entityDelete = Get(
                    x => x.ArticleId == articleLikeDislikeEntity.Article.Id
                    &&
                    x.ArticleLikeDislikeOwnerId == articleLikeDislikeEntity.ArticleLikeDislikeOwner.Id
                    &&
                    x.Isliked == !articleLikeDislikeEntity.Isliked
                    &&
                    x.DeletedAt == null
            );

            if (entityDelete != null)
            {
                entityDelete.DeletedAt = DateTime.Now;

                Update(entityDelete);
            }
        }
    }

    public interface IArticleLikeDislikeRepository : IRepository<ArticleLikeDislikeEntity>
    {
        bool isArticleLikedOrDisliked(ArticleLikeDislikeEntity articleLikeDislikeEntity);
        void DeleteLikeOrDislike(ArticleLikeDislikeEntity articleLikeDislikeEntity);
        void DeleteOpposite(ArticleLikeDislikeEntity articleLikeDislikeEntity);
    }
}
