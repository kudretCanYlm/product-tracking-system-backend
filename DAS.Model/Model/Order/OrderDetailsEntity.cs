﻿using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.User;
using System;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAS.Model.Model.Authentication;

namespace DAS.Model.Model.Order
{
    public class OrderDetailsEntity:BaseEntity,IBaseTimeEntity
    {
        public OrderDetailsEntity():base()
        {

        }

        public OrderDetailsEntity(decimal total):base()
        {
            this.Total = total;
        }

        public Guid UserId { get; set; }
        public decimal Total { get; set; } = 0;
        public Guid PaymentId { get; set; }
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }


        [ForeignKey("UserId")]
        public LoginEntity UserEntity { get; set; }

        [ForeignKey("PaymentId")]
        public PaymentDetailsEntity PaymentDetails { get; set; }
        
    }

    public class OrderDetailsValidation:AbstractValidator<OrderDetailsEntity>
    {
        public OrderDetailsValidation()
        {
            RuleFor(x => x.CreatedAt).NotEmpty().NotEmpty();
            RuleFor(x => x.Total).GreaterThanOrEqualTo(0);
        }
    }
}
