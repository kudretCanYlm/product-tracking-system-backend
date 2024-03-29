﻿using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Product
{
    public class ProductCategoryEntity:BaseEntity,IBaseTimeEntity
    {
        public ProductCategoryEntity():base()
        {

        }
        public ProductCategoryEntity(string name,string description) :base()
        {
                this.Name = name;
                this.Description = description;
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }

    public class ProductCategoryValidation:AbstractValidator<ProductCategoryEntity>
    {
        public ProductCategoryValidation()
        {
            RuleFor(x => x.Name).Length(5, 50);
            RuleFor(x => x.Description).Length(5, 50);
            RuleFor(x => x.CreatedAt).NotNull().NotEmpty();
        }
    }
}
