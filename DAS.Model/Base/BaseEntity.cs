using DAS.Model.Base.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAS.Model.Base
{
    public class BaseEntity:IBaseEntity
    {
        public BaseEntity()
        {
            //create new guid when created the instance
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; } 
    }
}
