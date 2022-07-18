using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Product
{
    public class ProductEntity:BaseEntity,IBaseTimeEntity
    {
        public ProductEntity():base()
        {

        }

        public ProductEntity(string name,string description,string sku,decimal price) :base()
        {
            //name control
            if (String.IsNullOrEmpty(name))
                this.Name = "Empty";
            else
                this.Name = name.ToLower();

            //description control
            if (String.IsNullOrEmpty(description))
                this.Description = "Empty";
            else
                this.Description = description.ToLower();

            //sku control
            if (String.IsNullOrEmpty(sku))
                 this.SKU= "Empty";
            else
                this.SKU = sku.ToLower();




        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; } = 0;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid CategoryId { get; set; }
        public Guid InventoryId { get; set; }
        public Guid DiscountId { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategoryEntity ProductCategory { get; set; }

        [ForeignKey("InventoryId")]
        public ProductInventoryEntity ProductInventory { get; set; }
        
        [ForeignKey("DiscountId")]
        public DiscountEntity Discount { get; set; }

    }

    public class ProductValidation: AbstractValidator<ProductEntity>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).Length(5, 50);
            RuleFor(x => x.Description).Length(5, 50);
            RuleFor(x => x.SKU).NotEmpty().NotNull();
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
        }
    }
}
