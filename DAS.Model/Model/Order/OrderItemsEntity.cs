using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;
using FluentValidation;

namespace DAS.Model.Model.Order
{
    public class OrderItemsEntity:BaseEntity,IBaseTimeEntity
    {
        public OrderItemsEntity():base()
        {

        }

        public OrderItemsEntity(int quantity) :base()
        {
            if (quantity > 0)
                this.Quantity = quantity;
        }

        public int Quantity { get; set; } = 0;
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }
    }

    public class OrderItemsusingValidation:AbstractValidator<OrderItemsEntity>
    {
        public OrderItemsusingValidation()
        {
            RuleFor(x => x.CreatedAt).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        }
    }

}
