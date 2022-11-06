using Api.Infrastructure.Attributes;
using Api.Models.Project;
using AutoMapper;
using DAS.Model.Model.Enums;
using DAS.Model.Model.Project;
using DAS.Service.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("project")]
    public class ProjectController : ApiController
    {
        IProjectService projectService;
        IProjectRateService projectRateService;
        IProjectPersonService projectPersonService;
        IProjectSupporterService projectSupporterService;
        IProjectUserService projectUserService;

        public ProjectController(IProjectService projectService, IProjectRateService projectRateService, IProjectPersonService projectPersonService, IProjectSupporterService projectSupporterService, IProjectUserService projectUserService)
        {
            this.projectService = projectService;
            this.projectRateService = projectRateService;
            this.projectPersonService = projectPersonService;
            this.projectSupporterService = projectSupporterService;
            this.projectUserService = projectUserService;
        }

        [HttpPost, JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage Post([FromBody] ProjectPostModel projectPostModel)
        {
            if (projectPostModel == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "projectPostModel is null");

            var projectEntity = Mapper.Map<ProjectEntity>(projectPostModel);

            var status = projectService.AddNewProject(ref projectEntity);

            if (status is true)
                Request.CreateResponse(HttpStatusCode.Created, Mapper.Map<ProjectViewModel>(status));

            return Request.CreateResponse(HttpStatusCode.BadRequest, status.ToString());
        }
    }
}
