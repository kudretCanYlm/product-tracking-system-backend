using Api.Models.Article;
using Api.Models.Authentication;
using Api.Models.Chat;
using Api.Models.Location;
using Api.Models.Project;
using AutoMapper;
using DAS.Model.Model.Article;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Chat;
using DAS.Model.Model.Location;
using DAS.Model.Model.Project;
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

            Mapper.CreateMap<ArticlePostModel, ArticleEntity>()
                .ForMember(x => x.ArticleTitle, act => act.MapFrom(x => x.ArticleTitle))
                .ForMember(x => x.Summary, act => act.MapFrom(x => x.Summary))
                .ForMember(x => x.ArticleContent, act => act.MapFrom(x => x.Content))
                .ForMember(x => x.IsPublic, act => act.MapFrom(x => x.IsPublic));

            Mapper.CreateMap<ArticleLikeDislikePostModel, ArticleLikeDislikeEntity>()
                .ForMember(x => x.ArticleId, act => act.MapFrom(x => x.ArticleId))
                .ForMember(x => x.Isliked, act => act.MapFrom(x => x.Isliked));

            Mapper.CreateMap<ArticleCommentPostModel, ArticleCommentEntity>()
                .ForMember(x => x.ArticleId, act => act.MapFrom(x => x.ArticleId))
                .ForMember(x => x.Comment, act => act.MapFrom(x => x.Comment));

            Mapper.CreateMap<ProjectPostModel, ProjectEntity>()
                .ForMember(x => x.Name, act => act.MapFrom(x => x.Name))
                .ForMember(x => x.ContentText, act => act.MapFrom(x => x.ContentText))
                .ForMember(x => x.Price, act => act.MapFrom(x => x.Price))
                .ForMember(x => x.MoneyType, act => act.MapFrom(x => x.MoneyType));
        }
    }
}