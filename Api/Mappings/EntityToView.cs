using Api.Models.Chat;
using Api.Models.Location;
using AutoMapper;
using DAS.Model.Model.Chat;
using DAS.Model.Model.Location;
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

            Mapper.CreateMap<CountryEntity, CountryViewModel>()
                .ForMember(x => x.Id, act => act.MapFrom(x => x.Id))
                .ForMember(x => x.CountryName, act => act.MapFrom(x => x.CountryName))
                .ForMember(x => x.CountryCode, act => act.MapFrom(x => x.CountryCode));
        }
    }
}