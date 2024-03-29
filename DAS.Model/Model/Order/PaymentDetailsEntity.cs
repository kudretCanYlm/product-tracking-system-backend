﻿using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DAS.Model.Model.Order
{
    public class PaymentDetailsEntity:BaseEntity,IBaseTimeEntity
    {
        public PaymentDetailsEntity() : base()
        {

        }
        public PaymentDetailsEntity(int amount,string provider,string status):base()
        {
            //provider control
            if (String.IsNullOrEmpty(provider))
                this.Provider= "Empty";
            else
                this.Provider = provider.ToLower();

            //description control
            if (String.IsNullOrEmpty(status))
                this.Status = "Empty";
            else
                this.Status = status.ToLower();
        }

        public int Amount { get; set; } = 0;
        public string Provider { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class PaymentDetailsValidation:AbstractValidator<PaymentDetailsEntity>
    {
        public PaymentDetailsValidation()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CreatedAt).NotEmpty().NotNull();
            RuleFor(x => x.Provider).Length(5, 50);
            RuleFor(x => x.Status).Length(5, 50);
        }
    }
}
