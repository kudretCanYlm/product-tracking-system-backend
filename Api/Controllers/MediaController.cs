using Api.Infrastructure.Attributes;
using DAS.Model.Base.Enums;
using DAS.Model.Model.Enums;
using DAS.Model.Model.Media;
using DAS.Service.Services.Authentication;
using DAS.Service.Services.Media;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("media")]
    public class MediaController : ApiController
    {

        IImageService imageService;
        ILoginService loginService;

        public MediaController(IImageService imageService, ILoginService loginService)
        {
            this.imageService = imageService;
            this.loginService = loginService;
        }

        public class ImageView
        {
            public string ImageData { get; set; }
            public string ImageMimeType { get; set; }
            public Guid? OwnerId { get; set; }
            public ImageTypesEnum? ImageType { get; set; }
            public bool IsPublic { get; set; } = true;
        }

        public class ImageGetView
        {
            public Guid OwnerId { get; set; }
            public ImageTypesEnum ImageType { get; set; }
        }

        [Route("uploadImageToLogin"), HttpPost,JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage PostLogin([FromBody] ImageView image)
        {
            //access controll
            //string name=ActionContext.RequestContext.Principal.Identity.Name;

            ImageEntity imageEntity = new ImageEntity();

            imageEntity.ImageData = Convert.FromBase64String(image.ImageData.Split(',')[1] ?? image.ImageData); ;
            imageEntity.ImageMimeType = image.ImageData.Split(',')[0] ?? image.ImageMimeType;
            imageEntity.ImageType = ImageTypesEnum.UserIcon;

            //find the user by token and search on db find Id
            string userName = ActionContext.RequestContext.Principal.Identity.Name;
            var userId = loginService.GetUserId(userName);

            if (userId != image.OwnerId)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User not auth");

            imageEntity.OwnerId = image.OwnerId ?? new Guid();
            imageEntity.IsPublic = true;

            string message = imageService.AddImage(imageEntity);

            return Request.CreateResponse(HttpStatusCode.Accepted, message);

        }

        [Route("uploadImageToProject"), HttpPost]
        public HttpResponseMessage PostProject([FromBody] ImageView image)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "not working");
        }

        [Route("getSingleGlobalImage"), HttpPost,JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage GetSingle([FromBody] ImageGetView imageGetView)
        {
            var imageDb = imageService.GetSingleGlobalImage(imageGetView.OwnerId, imageGetView.ImageType);

            ImageView imageView = new ImageView();

            imageView.ImageData = imageDb.ImageMimeType + "," + Convert.ToBase64String(imageDb.ImageData);
            imageView.ImageMimeType = imageView.ImageMimeType;
            imageView.ImageType = imageDb.ImageType;

            return Request.CreateResponse(HttpStatusCode.Accepted, imageView);
        }
    }
}
