using Api.Models.Article;
using Api.Models.Chat;
using Api.Models.Location;
using AutoMapper;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
using DAS.Model.Model.Chat;
using DAS.Model.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Mappings
{
    public class EntityToView : Profile
    {
        public override string ProfileName => "EntityToViewMap";

        protected override void Configure()
        {
            Mapper.CreateMap<MessageEntity, MessageViewModel>()
                .ForMember(x => x.Id, act => act.MapFrom(x => x.Id))
                .ForMember(x => x.Message, act => act.MapFrom(x => x.Message))
                .ForMember(x => x.Name, act => act.MapFrom(x => x.User.Name + " " + x.User.Surname))
                .ForMember(x => x.UserId, act => act.MapFrom(x => x.UserId))
                .ForMember(x => x.CreatedAt, act => act.MapFrom(x => x.CreatedAt));

            Mapper.CreateMap<CountryEntity, CountryViewModel>()
                .ForMember(x => x.Id, act => act.MapFrom(x => x.Id))
                .ForMember(x => x.CountryName, act => act.MapFrom(x => x.CountryName))
                .ForMember(x => x.CountryCode, act => act.MapFrom(x => x.CountryCode));

            Mapper.CreateMap<ArticleEntity, ArticleViewModel>()
                .ForMember(x => x.Id, act => act.MapFrom(x => x.Id))
                .ForMember(x => x.ArticleTitle, act => act.MapFrom(x => x.ArticleTitle))
                .ForMember(x => x.Summary, act => act.MapFrom(x => x.Summary))
                .ForMember(x => x.Content, act => act.MapFrom(x => x.ArticleContent))
                .ForMember(x => x.CreatedAt, act => act.MapFrom(x => x.CreatedAt))
                .ForMember(x => x.ArticleOwnerId, act => act.MapFrom(x => x.ArticleOwner.Id))
                .ForMember(x => x.OwnerName, act => act.MapFrom(x => x.ArticleOwner.Name))
                .ForMember(x => x.OwnerSurname, act => act.MapFrom(x => x.ArticleOwner.Surname));

            Mapper.CreateMap<ArticleDtoView, ArticleViewModel>()
                .ForMember(x => x.Id, act => act.MapFrom(x => x.Id))
                .ForMember(x => x.ArticleTitle, act => act.MapFrom(x => x.ArticleTitle))
                .ForMember(x => x.Summary, act => act.MapFrom(x => x.Summary))
                .ForMember(x => x.Content, act => act.MapFrom(x => x.ArticleContent))
                .ForMember(x => x.CreatedAt, act => act.MapFrom(x => x.CreatedAt))
                .ForMember(x => x.ArticleOwnerId, act => act.MapFrom(x => x.OwnerId))
                .ForMember(x => x.OwnerName, act => act.MapFrom(x => x.OwnerName))
                .ForMember(x => x.OwnerSurname, act => act.MapFrom(x => x.OwnerSurname));
        }
    }
}