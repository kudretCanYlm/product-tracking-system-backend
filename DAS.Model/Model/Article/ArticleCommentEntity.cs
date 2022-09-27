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
    public class ArticleCommentEntity : BaseEntity, IBaseTimeEntity
    {
        public ArticleCommentEntity() : base()
        {

        }

        public string Comment { get; set; }
        public Guid CommentOwnerId { get; set; }
        public Guid ArticleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CommentOwnerId")]
        public LoginEntity CommentOwner { get; set; }
        [ForeignKey("ArticleId")]
        public ArticleEntity Article { get; set; }
    }

    interface IArticleCommentValidation : IValidator<ArticleCommentEntity>
    {

    }

    public class ArticleCommentValidation : AbstractValidator<ArticleCommentEntity>, IArticleCommentValidation
    {
        public ArticleCommentValidation()
        {
            RuleFor(x => x.Comment).Length(1, 50)
                .WithMessage("Comment Reply length must be 1-50")
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.CommentOwner).SetValidator(new LoginValidation());
            RuleFor(x => x.Article).SetValidator(new ArticleValidation());
        }
    }
}
