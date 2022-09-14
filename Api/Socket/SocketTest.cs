using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Api.Socket
{
    public interface IChatSocketHandler
    {

    }
    public class ChatSocketHandler : WebSocketHandler
    {
        public class UserModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string UserLogo { get; set; }
            public string UserId { get; set; }

        }

        private static WebSocketCollection all_clients = new WebSocketCollection();
        private static Dictionary<string, WebSocketCollection> clients_list = new Dictionary<string, WebSocketCollection>();


        private UserModel connectedUser = new UserModel();

        public ChatSocketHandler(string username)
        {
            this.connectedUser.UserName = username;
           // this.connectedUser.UserId = this.WebSocketContext.Headers["userId"];
        }

        public void CreateNewChannel()
        {

        }

        public void LoadOldChannel()
        {

        }

        private void AddUsersToChannel()
        {

        }



        public override void OnOpen()
        {
            //this.connectedUser.UserName = this.WebSocketContext.QueryString["UserName"];

            string channel = this.WebSocketContext.Headers["channel"].ToString();
            all_clients.Add(this);

            //clients.Add(this);
            //clients.Broadcast(this.WebSocketContext.QueryString["UserName"] + " katıldı");
            //  clients.Broadcast(this.WebSocketContext.AnonymousID);


            if (!clients_list.Keys.Contains(channel))
            {
                //clients.Broadcast("kanal: " + this.WebSocketContext.Headers["kanal"]);
                WebSocketCollection clients = new WebSocketCollection();
                
                clients.Add(this);

                clients_list.Add(channel, clients);

                var clients_new = clients_list.Where(x => x.Key == channel).FirstOrDefault().Value;
                clients_new.Broadcast(channel + " oluşturukdu");
            }
            else
            {
                //auth control
                clients_list.Where(x => x.Key == channel).FirstOrDefault().Value.Add(this);

                var clients = clients_list.Where(x => x.Key == channel).FirstOrDefault().Value;

                clients.Broadcast(this.WebSocketContext.QueryString["UserName"] + " katıldı");
                clients.Broadcast("toplam kişi: " + clients.Count());

            }

            //OnMessage("ConnectedUserList#" + this.GetConnectedUserListJson(clients));
        }

        public override void OnMessage(string message)
        {
            string channel = this.WebSocketContext.Headers["channel"].ToString();

            if (clients_list.Keys.Contains(channel))
            {
                var clients = clients_list.Where(x => x.Key == channel).FirstOrDefault().Value;
                clients.Broadcast(string.Format("{0} = {1}", this.connectedUser.UserName, message));
            }

        }

        //public override void OnError()
        //{

        //    clients.Broadcast("error");
        //}

        public override void OnClose()
        {
            string channel = this.WebSocketContext.Headers["channel"].ToString();

            clients_list.Where(x => x.Key == channel).FirstOrDefault().Value.Remove(this);

            int onlineCount = clients_list.Where(x => x.Key == channel).FirstOrDefault().Value.Count;

            if (onlineCount== 0)
            {
                clients_list.Remove(channel);
            }
            else
            {
                clients_list.Where(x => x.Key == channel).FirstOrDefault().Value.Broadcast(this.connectedUser.UserName + " ayrıldı. Kalan kişi: " + onlineCount);
            }
        }

        /// <summary>
        /// Returns User List With Json Format
        /// </summary>
        /// <param name="clientcollection"></param>
        /// <returns></returns>
        public string GetConnectedUserListJson(WebSocketCollection clientcollection)
        {
            string connectedUserListJsonStr = "";
            try
            {
                List<UserModel> connectedUserList = new List<UserModel>();
                foreach (var item in clientcollection)
                {
                    UserModel newUser = new UserModel();
                    newUser.UserName = item.WebSocketContext.QueryString["UserName"].ToString();
                    newUser.UserLogo = "1";
                    connectedUserList.Add(newUser);
                }
                connectedUserListJsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(connectedUserList);
            }
            catch (Exception ex)
            {
                connectedUserListJsonStr = "-1";
            }
            return connectedUserListJsonStr;
        }
    }
}