using Api.Models.Article;
using Api.Models.Chat;
using Api.Models.Location;
using Api.Models.Project;
using AutoMapper;
using DAS.Model.Base.Enums;
using DAS.Model.Dto.Article;
using DAS.Model.Model.Article;
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
    public class EntityToView : Profile
    {
        public override string ProfileName => "EntityToViewMap";

        protected override void Configure()
        {
            Mapper.CreateMap<MessageEntity, MessageViewModel>()
                .ForMember(x => x.Name, act => act.MapFrom(x => x.User.Name + " " + x.User.Surname));

            Mapper.CreateMap<CountryEntity, CountryViewModel>();

            Mapper.CreateMap<ArticleEntity, ArticleViewModel>()
                .ForMember(x => x.Content, act => act.MapFrom(x => x.ArticleContent))
                .ForMember(x => x.ArticleOwnerId, act => act.MapFrom(x => x.ArticleOwner.Id))
                .ForMember(x => x.OwnerName, act => act.MapFrom(x => x.ArticleOwner.Name))
                .ForMember(x => x.OwnerSurname, act => act.MapFrom(x => x.ArticleOwner.Surname));

            Mapper.CreateMap<ArticleDtoView, ArticleViewModel>()
                .ForMember(x => x.Content, act => act.MapFrom(x => x.ArticleContent))
                .ForMember(x => x.ArticleOwnerId, act => act.MapFrom(x => x.OwnerId));

            Mapper.CreateMap<ArticleLikeDislikeEntity, ArticleLikeDislikeSingleViewModel>()
                .ForMember(x => x.Isliked, act => act.MapFrom(x => x.Isliked));

            Mapper.CreateMap<ArticleCommentEntity, ArticleCommentViewModel>()
                .ForMember(x => x.CommentOwnerName, act => act.MapFrom(x => x.CommentOwner.Name))
                .ForMember(x => x.CommentOwnerSurname, act => act.MapFrom(x => x.CommentOwner.Surname));

            Mapper.CreateMap<ArticleCommentDto, ArticleCommentViewModel>();

            Mapper.CreateMap<ProjectEntity, ProjectViewModel>()
                .ForMember(x => x.MoneyType, act => act.MapFrom(x => Enum.GetName(typeof(MoneyTypesEnum), x.MoneyType)));

            Mapper.CreateMap<UserRoleAndDescriptionEntity, UserRoleAndDescriptionViewModel>()
                .ForMember(x => x.FullName, act => act.MapFrom(x => x.OwnerUser.Name + " " + x.OwnerUser.Surname));
        }
    }
}