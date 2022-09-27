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
    public class ArticleCommentReply : BaseEntity, IBaseTimeEntity
    {
        public ArticleCommentReply() : base()
        {

        }
        public string CommentReply { get; set; }
        public Guid CommentId { get; set; }
        public Guid ArticleCommentReplyOwnerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ArticleCommentReplyOwnerId")]
        public LoginEntity ArticleCommentReplyOwner { get; set; }
    }

    public interface IArticleCommentReplyValidation : IValidator<ArticleCommentReply>
    {

    }

    public class ArticleCommentReplyValidation : AbstractValidator<ArticleCommentReply>, IArticleCommentReplyValidation
    {
        public ArticleCommentReplyValidation()
        {
            RuleFor(x => x.CommentReply).Length(1, 50)
                .WithMessage("Comment Reply length must be 1-50")
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.CommentId)
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.ArticleCommentReplyOwner).SetValidator(new LoginValidation());
        }
    }
}
