using Api.Infrastructure.Attributes;
using Api.Models.Login;
using AutoMapper;
using DAS.Core.Common;
using DAS.Core.PagingAndFiltering;
using DAS.Model.Model.Enums;
using DAS.Model.Model.User;
using DAS.Service.Services.Authentication;
using DAS.Service.Services.User;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Api.Models.User.UserModel;

namespace Api.Controllers
{
	[RoutePrefix("user")]
    public class UserController : ApiController
    {
        ILoginService loginService;
        IUserRoleAndDescriptionService userRoleAndDescriptionService;

		public UserController(ILoginService loginService, IUserRoleAndDescriptionService userRoleAndDescriptionService)
		{
			this.loginService = loginService;
			this.userRoleAndDescriptionService = userRoleAndDescriptionService;
		}

		[Route("AddUserRoleAndDescription"),HttpPost, JwtAuthentication(RoleEnum.Admin)]
		public HttpResponseMessage AddUserRoleAndDescription([FromBody] UserRoleAndDescriptionPostModel userRoleAndDescriptionPostModel)
        {
            if(userRoleAndDescriptionPostModel==null)
				return Request.CreateResponse(HttpStatusCode.BadRequest, "userRoleAndDescriptionPostModel is null");
			string userName = ActionContext.RequestContext.Principal.Identity.Name;
			var userId = loginService.GetUserId(userName);
			userRoleAndDescriptionPostModel.OwnerUserId = userId;

			var userRoleAndDescriptionEntity = Mapper.Map<UserRoleAndDescriptionEntity>(userRoleAndDescriptionPostModel);

            var status=userRoleAndDescriptionService.AddUserRoleAndDescription(userRoleAndDescriptionEntity);
            
            if (status is true)
				return Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<UserRoleAndDescriptionViewModel>(userRoleAndDescriptionEntity));

			return Request.CreateResponse(HttpStatusCode.BadRequest, status.ToString());

		}


		[HttpGet,Route("getUserRoleAndDescriptionByRange/{pageNumber}/{pageSize}"), ]
        public HttpResponseMessage GetUserRoleAndDescriptionByRange(int pageNumber, int pageSize)
        {
            var userRoleAndDescriptions = userRoleAndDescriptionService.GetUserRoleAndDescriptionByRange(pageNumber, pageSize, x => x.OwnerUser.Username);

			return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<UserRoleAndDescriptionViewModel>>(userRoleAndDescriptions));

		}

		[HttpPost,Route("getUserRoleAndDescriptionByPagedList"), JwtAuthentication(RoleEnum.Admin)]
		public HttpResponseMessage getUserRoleAndDescriptionByPagedList([FromBody] PageSearchArgs args)
		{
			var userRoleAndDescriptions=userRoleAndDescriptionService.GetUserRoleAndDescriptionByRangeWithFilterAndSorting(args);

			var userRolesAndDescriptionsView = Mapper.Map<IEnumerable<UserRoleAndDescriptionViewModel>>(userRoleAndDescriptions.Items);

			return Request.CreateResponse(HttpStatusCode.OK, userRolesAndDescriptionsView);

		}

		[HttpGet,Route("getAuthorizedUserInformationMini"),JwtAuthentication(RoleEnum.Admin)]
		public HttpResponseMessage getAuthorizedUserInformationMini()
		{
			string userName = ActionContext.RequestContext.Principal.Identity.Name;
			var user = loginService.GetUser(x => x.Username == userName);
				
			if(user==null)
				return Request.CreateResponse(HttpStatusCode.NotFound);

			return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<AuthorizedUserInformationMiniModel>(user));

		}


	}
}
