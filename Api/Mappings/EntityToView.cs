using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Mappings
{
    public class EntityToView:Profile
    {
        public override string ProfileName => "EntityToViewMap";

        protected override void Configure()
        {
            //https://github.com/MarlabsInc/SocialGoal/blob/master/source/SocialGoal/Mappings/DomainToViewModelMappingProfile.cs
            // Mapper.CreateMap<>
            base.Configure();
        }
    }
}