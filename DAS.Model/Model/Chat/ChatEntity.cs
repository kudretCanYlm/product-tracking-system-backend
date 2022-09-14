using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Authentication;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAS.Model.Model.Chat
{
    public class ChatEntity : BaseEntity, IBaseTimeEntity
    {
        public ChatEntity() : base()
        {

        }

        public Guid UserFirstId { get; set; }
        public Guid UserSecondId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("UserFirstId")]
        public LoginEntity UserFirst { get; set; }

        [ForeignKey("UserSecondId")]
        public LoginEntity UserSecond { get; set; }

    }

    public interface IChatValidation : IValidator<ChatEntity>
    {

    }

    public class ChatValidation : AbstractValidator<ChatEntity>,IChatValidation
    {
        public ChatValidation()
        {
            RuleFor(x => x.CreatedAt)
                .NotNull().WithMessage("don't be null ")
                .NotEmpty().WithMessage("don't be empty");

            RuleFor(x => x.UserFirst).SetValidator(new LoginValidation());
            RuleFor(x => x.UserSecond).SetValidator(new LoginValidation());
        }
    }
}
