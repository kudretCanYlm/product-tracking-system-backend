using DAS.Model.Base.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAS.Model.Base
{
    public class BaseEntity:IBaseEntity
    {
        public BaseEntity(bool isNewGuid=true)
        {
            //create new guid when created the instance
            if(isNewGuid)
                Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; } 
    }
}
