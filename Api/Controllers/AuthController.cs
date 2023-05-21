using Api.Infrastructure.Attributes;
using Api.Infrastructure.Jwt;
using Api.Models.Authentication;
using AutoMapper;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Enums;
using DAS.Service.Services.Authentication;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{

    [RoutePrefix("auth")]
    public class AuthController : ApiController
    {
        ILoginService loginService;

        public AuthController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous,Route("login"),HttpPost]
        public HttpResponseMessage Login([FromBody]LoginPostModel user)
        {
            //will change by users role from db
            var userDb=loginService.LogIn(user.Username,user.Password);

            if(userDb!=null)
            {
                var token = JwtManager.GenerateToken(userDb.Username, RoleEnum.Admin);

                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Username or Password is wrong");
            }
                
        }

		[Route("signUp"), AllowAnonymous, HttpPost]
		public HttpResponseMessage SignUp([FromBody] LoginPostModelNewUser loginEntity)
		{
			object result = loginService.AddNewUser(Mapper.Map<LoginPostModelNewUser, LoginEntity>(loginEntity));

			if (result is true)
            {
				var token = JwtManager.GenerateToken(loginEntity.Username, RoleEnum.Admin);
				return Request.CreateResponse(HttpStatusCode.Created,token);
			}
			else
				return Request.CreateResponse(HttpStatusCode.BadRequest, result);
		}

		[Route("getUsername"), JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage GetTest()
        {
            string name=ActionContext.RequestContext.Principal.Identity.Name;

            return Request.CreateResponse(HttpStatusCode.Accepted,name);
        }

    }
}
