using Api.Models.Authentication;
using Api.Models.Chat;
using Api.Models.Location;
using AutoMapper;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Chat;
using DAS.Model.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Mappings
{
    public class ViewToEntity:Profile
    {
        public override string ProfileName => "ViewToEntityMap";

        protected override void Configure()
        {
            Mapper.CreateMap<CountryPostModel, CountryEntity>()
                .ForMember(x => x.CountryName, act => act.MapFrom(x => x.CountryName))
                .ForMember(x => x.CountryCode, act => act.MapFrom(x => x.CountryCode));

            Mapper.CreateMap<LoginPostModelNewUser, LoginEntity>()
                .ForMember(x => x.Username, act => act.MapFrom(x => x.Username))
                .ForMember(x => x.Password, act => act.MapFrom(x => x.Password))
                .ForMember(x => x.Email, act => act.MapFrom(x => x.Email))
                .ForMember(x => x.Name, act => act.MapFrom(x => x.Name))
                .ForMember(x => x.Surname, act => act.MapFrom(x => x.Surname));                

        }
    }
}