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

    [AllowAnonymous,RoutePrefix("socket")]
    public class SocketController : ApiController
    {
        [CacheControl,Route("test"),AcceptVerbs("GET", "POST"),AllowAnonymous]
        public HttpResponseMessage Get(string username)
        {
            if (!HttpContext.Current.IsWebSocketRequest)
                return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            HttpContext.Current.AcceptWebSocketRequest(new ChatSocketHandler(username));
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            
        }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }


}
