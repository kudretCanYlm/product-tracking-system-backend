using Api.Infrastructure.Attributes;
using Api.Infrastructure.Jwt;
using Api.Models.Chat;
using Api.Socket;
using AutoMapper;
using DAS.Model.Model.Chat;
using DAS.Model.Model.Enums;
using DAS.Service.Services.Authentication;
using DAS.Service.Services.Chat;
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
    [RoutePrefix("chat")]
    public class ChatController : ApiController
    {

        IMessageService messageService;
        IChatService chatService;
        ILoginService loginService;

        public ChatController(IMessageService messageService, IChatService chatService, ILoginService loginService)
        {
            this.messageService = messageService;
            this.chatService = chatService;
            this.loginService = loginService;
        }

        ChatSocketHandler chatSocketHandler = new ChatSocketHandler();

        [Route("connectSocket"), AcceptVerbs("GET", "POST"), AllowAnonymous]
        public HttpResponseMessage ChatSocket()
        {

            if (!HttpContext.Current.IsWebSocketRequest)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "that isn't a socket connection");

            }

            var token = Request.Headers.Where(x => x.Key == "token").FirstOrDefault().Value.FirstOrDefault().ToString();
            var principal = JwtManager.GetPrincipal(token) ?? null;
            if (principal != null)
            {

                var userId = loginService.GetUserId(principal.Identity.Name).ToString();

                if (userId.Length > 1)
                {
                    chatSocketHandler.SetUserId(userId);
                    HttpContext.Current.AcceptWebSocketRequest(chatSocketHandler);
                }
            }

            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }


        [Route("getOldMessages"), HttpPost, JwtAuthentication(RoleEnum.Admin)]
        public HttpResponseMessage GetOldMessages(GetMessagePostModel getMessage)
        {
            string name = ActionContext.RequestContext.Principal.Identity.Name;

            var userId = loginService.GetUserId(name);
            var userSecondId = Guid.Parse(getMessage.TargetUserId);

            var messages = messageService
                .GetMessages(
                x => x.UserId == userId
                &&
                (x.Chat.UserFirstId == userSecondId || x.Chat.UserSecondId == userSecondId));

            var messagesView = new List<MessageViewModel>();



            if (messages == null)
                return Request.CreateResponse(HttpStatusCode.Accepted, "There isn't any message");

            foreach (var message in messages)
            {
                messagesView.Add(Mapper.Map<MessageEntity, MessageViewModel>(message));
            }


            return Request.CreateResponse(HttpStatusCode.OK, messagesView);
        }

        [Route("sendMessage"), JwtAuthentication(RoleEnum.Admin), HttpPost]
        public HttpResponseMessage SendMessage([FromBody] SendMessagePostModel sendMessage)
        {
            string name = ActionContext.RequestContext.Principal.Identity.Name;

            var userFirst = loginService.GetUser(x => x.Username == name);

            Guid TargetUserId = Guid.Parse(sendMessage.TargetUserId);
            var userSecond = loginService.GetUser(x => x.Id == TargetUserId);


            var chat = chatService.FindChat(userFirst.Id, userSecond.Id);

            if (chat == null)
            {
                var chatEntity = new ChatEntity();

                chatEntity.UserFirst = userFirst;
                chatEntity.UserSecond = userSecond;

                object chatState = chatService.AddChat(ref chatEntity);


                if (chatState is true)
                    chat = chatEntity;

                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, chatState.ToString());

            }

            bool state = chatSocketHandler.NewChat(chat);

            MessageEntity messageEntity = new MessageEntity()
            {
                Chat = chat,
                Message = sendMessage.message,
                User = userSecond,
                IsViewed = state,
            };

            object messageState = messageService.AddMessage(messageEntity);

            if (messageState is true)
            {
                chatSocketHandler.SendMessage(messageEntity);

                return Request.CreateResponse(HttpStatusCode.OK, "Message saved and sended");
            }

            //will add to return the  message
            return Request.CreateResponse(HttpStatusCode.BadRequest, messageState.ToString());
        }

    }
}
