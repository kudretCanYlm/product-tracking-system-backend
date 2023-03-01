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
using DAS.Model.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Api.Models.User.UserModel;

namespace Api.Mappings
{
    public class ViewToEntity:Profile
    {
        public override string ProfileName => "ViewToEntityMap";

        protected override void Configure()
        {
            Mapper.CreateMap<CountryPostModel, CountryEntity>();

            Mapper.CreateMap<LoginPostModelNewUser, LoginEntity>();

            Mapper.CreateMap<ArticlePostModel, ArticleEntity>()
                .ForMember(x => x.ArticleContent, act => act.MapFrom(x => x.Content));

            Mapper.CreateMap<ArticleLikeDislikePostModel, ArticleLikeDislikeEntity>();

            Mapper.CreateMap<ArticleCommentPostModel, ArticleCommentEntity>();

            Mapper.CreateMap<ProjectPostModel, ProjectEntity>();

            Mapper.CreateMap<UserRoleAndDescriptionPostModel, UserRoleAndDescriptionEntity>();
        }
    }
}