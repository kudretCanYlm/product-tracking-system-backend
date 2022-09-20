using Api.Models.Chat;
using AutoMapper;
using DAS.Model.Model.Chat;
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
            Mapper.CreateMap<MessageEntity, MessageViewModel>()
                .ForMember(x => x.Id, act => act.MapFrom(x => x.Id))
                .ForMember(x => x.Message, act => act.MapFrom(x => x.Message))
                .ForMember(x=>x.Name,act=>act.MapFrom(x=>x.User.Name+" "+x.User.Surname))
                .ForMember(x => x.UserId, act => act.MapFrom(x => x.UserId))
                .ForMember(x => x.CreatedAt, act => act.MapFrom(x => x.CreatedAt));
        }
    }
}