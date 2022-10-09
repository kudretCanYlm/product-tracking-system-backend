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
    public class ArticleEntity : BaseEntity, IBaseTimeEntity
    {
        public ArticleEntity() : base()
        {

        }

        public string ArticleTitle { get; set; }
        public Guid ArticleOwnerId { get; set; }
        public string Summary { get; set; }
        public string ArticleContent { get; set; }
        public bool IsPublic { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ArticleOwnerId")]
        public LoginEntity ArticleOwner { get; set; }

    }

    public interface IArticleValidation : IValidator<ArticleEntity>
    {

    }

    public class ArticleValidation : AbstractValidator<ArticleEntity>, IArticleValidation
    {
        public ArticleValidation()
        {
            RuleFor(x => x.ArticleTitle).Length(1, 50)
                .WithMessage("Article Title length must be 1-50")
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.Summary).Length(1, 100)
                .WithMessage("Summary length must be 1-100")
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.ArticleContent).MinimumLength(100)
                .WithMessage("Content length greater then 100")
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null ");

            RuleFor(x => x.ArticleOwner).SetValidator(new LoginValidation());
        }
    }
}
