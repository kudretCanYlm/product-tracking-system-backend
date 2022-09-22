using Api.Infrastructure.Attributes;
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
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        ILoginService loginService;

        public UserController(ILoginService loginService)
        {
            this.loginService = loginService;
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
    }
}
