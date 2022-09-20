using Api.Infrastructure.Jwt;
using DAS.Model.Model.Chat;
using DAS.Service.Services.Authentication;
using DAS.Service.Services.Chat;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Web;

namespace Api.Socket
{

    public class ChatSocketHandler : WebSocketHandler
    {
        public class UserModel
        {
            public string UserName { get; set; }
            public string UserId { get; set; }
        }

        private static WebSocketCollection all_clients = new WebSocketCollection();
        private static Dictionary<string, WebSocketCollection> clients_list = new Dictionary<string, WebSocketCollection>();


        private UserModel connectedUser = new UserModel();
        private List<string> userChannals = new List<string>();

        private ILoginService loginService;

        public ChatSocketHandler()
        {
        }

        public void SetUserId(string userId)
        {
            this.connectedUser.UserId = userId;
        }

        public bool NewChat(ChatEntity chat)
        {

            var userFirst = (ChatSocketHandler)all_clients
                .Where(x => ((ChatSocketHandler)x).connectedUser.UserId == chat.UserFirstId.ToString()).FirstOrDefault();

            var userSecond = (ChatSocketHandler)all_clients
                .Where(x => ((ChatSocketHandler)x).connectedUser.UserId == chat.UserSecondId.ToString()).FirstOrDefault();

            if (userSecond != null && userFirst != null)
            {
                var client_old = clients_list.Where(x => x.Key == chat.Id.ToString()).FirstOrDefault().Value ?? null;

                string channelId = chat.Id.ToString();

                if (client_old == null)
                {
                    userFirst.userChannals.Add(channelId);
                    userSecond.userChannals.Add(channelId);

                    WebSocketCollection newChat = new WebSocketCollection();

                    newChat.Add(userFirst);
                    newChat.Add(userSecond);

                    newChat.Broadcast(channelId + " oluşturukdu");
                    clients_list.Add(channelId, newChat);
                }

                else
                {
                    client_old.Broadcast(channelId + " bağlanıldı");
                }


                return true;
            }
            else
                return false;

        }

        public bool SendMessage(MessageEntity message)
        {
            var chat = clients_list.Where(x => x.Key == message.Chat.Id.ToString()).FirstOrDefault().Value ?? null;

            if (chat != null)
            {
                chat.Broadcast(message.Message);
                return true;
            }

            return false;
        }

        public int? ChatSizeControl(string chatId)
        {
            return clients_list.Where(x => x.Key == chatId).FirstOrDefault().Value.Count;
        }

        public override void OnOpen()
        {
            var userName = this.WebSocketContext.Headers["userName"].ToString();

            this.connectedUser.UserName = userName;

            all_clients.Add(this);

        }

        public override void OnMessage(string message)
        {


        }

        //public override void OnError()
        //{

        //    clients.Broadcast("error");
        //}

        public override void OnClose()
        {
            var channals = clients_list.Where(x => x.Value.Contains(this)).ToList();

            foreach (var chan in channals)
            {
                clients_list.Remove(chan.Key);
            }

            
        }

    }
}