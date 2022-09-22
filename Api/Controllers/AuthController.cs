using Api.Infrastructure.Attributes;
using Api.Infrastructure.Jwt;
using Api.Models.Authentication;
using AutoMapper;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Enums;
using DAS.Service.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Routing;

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

        [Route("getUsername"), JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage GetTest()
        {
            string name=ActionContext.RequestContext.Principal.Identity.Name;

            return Request.CreateResponse(HttpStatusCode.Accepted,name);
        }

    }
}
