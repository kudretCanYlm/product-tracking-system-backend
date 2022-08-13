using Api.Infrastructure.Attributes;
using Api.Infrastructure.Jwt;
using DAS.Model.Model.Enums;
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
   public class User
    {
        public string Username { get; set; }
        public string password { get; set; }
    }
    [RoutePrefix("auth")]
    public class AuthController : ApiController
    {
        [AllowAnonymous,Route("taketoken")]
        public string Get([FromBody]User user)
        {
            //user database control
            return JwtManager.GenerateToken(user.Username,RoleEnum.Admin);
        }

        [Route("test"), JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage GetTest()
        {
            string name=ActionContext.RequestContext.Principal.Identity.Name;

            return Request.CreateResponse(HttpStatusCode.Accepted,name);
        }

    }
}
