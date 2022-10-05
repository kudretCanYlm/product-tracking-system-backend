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

namespace DAS.Model.Model.Post
{
    public class PostEntity : BaseEntity, IBaseTimeEntity
    {
        public PostEntity():base()
        {

        }
        public Guid PostOwnerId { get; set; }
        public string PostText { get; set; }
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }

        [ForeignKey("PostOwnerId")]
        public LoginEntity PostOwner { get; set; }
    }

    public interface IPostValidation: IValidator<PostEntity>
    {

    }

    public class PostValidation:AbstractValidator<PostEntity>,IPostValidation
    {
        public PostValidation()
        {
            RuleFor(x=>x.PostText)
                .Length(5).WithMessage("text length greater than 5")
                .NotNull().WithMessage("don't be null")
                .NotEmpty().WithMessage("don't be empty");

            RuleFor(x => x.PostOwner).SetValidator(new LoginValidation());
        }
    }
}
