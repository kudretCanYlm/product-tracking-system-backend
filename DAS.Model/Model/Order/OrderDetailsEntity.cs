using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int UserId { get; set; }
        public decimal Total { get; set; } = 0;
        public int PaymentId { get; set; }
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }


        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }

        [ForeignKey("PaymentId")]
        public PaymentDetailsEntity PaymentDetails { get; set; }
        
    }
}
