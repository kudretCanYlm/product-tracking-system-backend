using DAS.Model.Base.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DAS.Model.Base
{
    [DataContract]
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
