    using Api.Infrastructure.Attributes;
using Api.Socket;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("socket")]
    public class SocketController : ApiController
    {

        private ChatSocketHandler chatSocketHandler = new ChatSocketHandler();

        [Route("test"), AcceptVerbs("GET", "POST"), AllowAnonymous]
        public HttpResponseMessage ConnectSocket()
        {
            if (!HttpContext.Current.IsWebSocketRequest)
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Socket değil!");

            HttpContext.Current.AcceptWebSocketRequest(chatSocketHandler);
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
           
        }

        [Route("control"),AllowAnonymous, AcceptVerbs("GET", "POST")]
        public string Test()
        {
            return "alive";
        }
    }

}
