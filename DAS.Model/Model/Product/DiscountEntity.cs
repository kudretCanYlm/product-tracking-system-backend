using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DAS.Model.Model.Product
{
    public class DiscountEntity:BaseEntity,IBaseTimeEntity
    {
        public DiscountEntity():base()
        {

        }
        public DiscountEntity(string name,string description,double discountPercent, bool? active) :base()
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

            //discountPercent control
            if (discountPercent >= 0)
                this.DiscountPercent = discountPercent;
            else
                this.DiscountPercent = 0;

            //active control
            if (active != null)
                this.Active = (bool)active;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; } = 0;
        public bool Active { get; set; } = false;
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get ; set ; }
    }

    public class DiscountValidation:AbstractValidator<DiscountEntity>
    {
        public DiscountValidation()
        {
            RuleFor(x => x.Active).NotNull().NotEmpty();
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
            RuleFor(x => x.Description).Length(5, 500);
            RuleFor(x => x.DiscountPercent).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name).Length(5, 50);
        }
    }
}
