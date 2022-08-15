using DAS.Model.Base;
using DAS.Model.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using DAS.Model.Model.Authentication;

namespace DAS.Model.Model.User
{
    public class UserPaymentEntity:BaseEntity
    {
        public UserPaymentEntity():base()
        {

        }

        public UserPaymentEntity(string provider,string accountNo,PaymentEnum paymentType):base()
        {
                this.Provider = provider;
                this.AccountNo = accountNo;
                this.AccountNo = accountNo;
        }

        public string Provider { get; set; }
        public string AccountNo { get; set; }
        public DateTime Expiry { get; set; }
        public PaymentEnum PaymentType { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public LoginEntity LoginEntity { get; set; }
    }

    public class UserPaymentValidation:AbstractValidator<UserPaymentEntity>
    {
        public UserPaymentValidation()
        {
            //could change
            RuleFor(x => x.AccountNo).NotEmpty().NotNull();
            RuleFor(x => x.Expiry).NotEmpty().NotNull();
            RuleFor(x => x.PaymentType).NotNull().NotEmpty();
        }
    }
}
