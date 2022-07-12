using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Product
{
    public class ProductInventoryEntity:BaseEntity,IBaseTimeEntity
    {
        public ProductInventoryEntity():base()
        {

        }
        public ProductInventoryEntity(int quantity):base()
        {
            if (quantity >= 0)
                this.Quantity = quantity;
            else
                this.Quantity = 0;
        }

        public int Quantity { get; set; } = 0;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
