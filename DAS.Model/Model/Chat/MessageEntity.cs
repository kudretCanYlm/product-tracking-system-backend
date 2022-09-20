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

namespace DAS.Model.Model.Chat
{
    public class MessageEntity:BaseEntity,IBaseTimeEntity
    {
        public MessageEntity():base()
        {

        }

        public string Message { get; set; }
        public bool IsViewed { get; set; } = true;
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }

        [ForeignKey("UserId")]
        public LoginEntity User { get; set; }
        [ForeignKey("ChatId")]
        public ChatEntity Chat { get; set; }
    }

    public interface IMessageValidation : IValidator<MessageEntity>
    {

    }

    public class MessageValidation : AbstractValidator<MessageEntity>,IMessageValidation
    {
        public MessageValidation()
        {
            RuleFor(x => x.Message).Length(1, 50).NotNull().NotEmpty();
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
            RuleFor(x => x.User).SetValidator(new LoginValidation());
            RuleFor(x => x.Chat).SetValidator(new ChatValidation());
        }
    }
}
