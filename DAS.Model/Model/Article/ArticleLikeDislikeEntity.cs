using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Article
{
    public class ArticleLikeDislikeEntity : BaseEntity, IBaseTimeEntity
    {
        public ArticleLikeDislikeEntity() : base()
        {

        }
        public bool Isliked { get; set; }
        public Guid ArticleLikeDislikeOwnerId { get; set; }
        public Guid ArticleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ArticleLikeDislikeOwnerId")]
        public LoginEntity ArticleLikeDislikeOwner { get; set; }
        [ForeignKey("ArticleId")]
        public ArticleEntity Article { get; set; }
    }

    public interface IArticleLikeDislikeValidation : IValidator<ArticleLikeDislikeEntity>
    {

    }

    public class ArticleLikeDislikeValidation : AbstractValidator<ArticleLikeDislikeEntity>, IArticleLikeDislikeValidation
    {
        public ArticleLikeDislikeValidation()
        {
            RuleFor(x => x.Isliked)
                .NotNull().WithMessage("Isliked don't be null ");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("CreatedAt be empty")
                .NotNull().WithMessage("CreatedAt don't be null ");

            RuleFor(x => x.ArticleLikeDislikeOwner).SetValidator(new LoginValidation());
            RuleFor(x => x.Article).SetValidator(new ArticleValidation());
        }
    }
}
