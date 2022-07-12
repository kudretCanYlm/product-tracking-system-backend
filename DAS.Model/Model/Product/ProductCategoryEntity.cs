using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
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
            Name = "Empty";
            Description = "Empty";
        }
        public ProductCategoryEntity(string name,string description) :base()
        {
            //name control
            if (String.IsNullOrEmpty(name))
                name = "Empty";
            else
                this.Name = name.ToLower();

            //description control
            if (String.IsNullOrEmpty(description))
                description = "Empty";
            else
                this.Description = description.ToLower();

        }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
