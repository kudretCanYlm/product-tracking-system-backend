using Api.Infrastructure.Attributes;
using Api.Models.Authentication;
using Api.Models.Project;
using AutoMapper;
using DAS.Core.PagingAndFiltering;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Enums;
using DAS.Model.Model.Project;
using DAS.Model.Model.User;
using DAS.Service.Services.Authentication;
using DAS.Service.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
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

		[Route("adduser"),AllowAnonymous,HttpPost]
        public HttpResponseMessage AddNewUser([FromBody] LoginPostModelNewUser loginEntity)
        {
          object result= loginService.AddNewUser(Mapper.Map<LoginPostModelNewUser,LoginEntity>(loginEntity));

            if (result is true)
                return Request.CreateResponse(HttpStatusCode.Accepted, "New user added as successfuly");
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
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


	}
}
