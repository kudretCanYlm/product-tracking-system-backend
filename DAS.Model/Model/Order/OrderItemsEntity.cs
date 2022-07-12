using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;

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
}
