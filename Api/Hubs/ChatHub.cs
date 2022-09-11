//using Microsoft.AspNet.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//namespace Api.Hubs
//{
//    public partial class ChatHub:Hub
//    {
//        //[Inject]
//        //private IEntity<User> UserService { get; set; }
//        //[Inject]
//        //private IEntity<UserFriend> UserFriendsService { get; set; }
//        //[Inject]
//        //private IEntity<Channel> ChannelService { get; set; }
//        //[Inject]
//        //private IEntity<ChannelUser> ChannelUsersService { get; set; }
//        //[Inject]
//        //private IEntity<Message> MessageService { get; set; }
//        //public ChatHub(IEntity<User> UserService, IEntity<UserFriend> UserFriendsService,
//        //    IEntity<Channel> ChannelService, IEntity<ChannelUser> ChannelUsersService,
//        //    IEntity<Message> MessageService)
//        //{
//        //    this.UserService = UserService;
//        //    this.UserFriendsService = UserFriendsService;
//        //    this.ChannelService = ChannelService;
//        //    this.ChannelUsersService = ChannelUsersService;
//        //    this.MessageService = MessageService;
//        //}
//        public override Task OnConnected()
//        {
//            var id = Context.ConnectionId;
//            //set user IsOnline true when he connects or reconnects
//            //User connectedUser = (await UserService.Read(x => x.ConnectionId == Context.ConnectionId)).FirstOrDefault();
//            //if (connectedUser != null)
//            //{
//            //    connectedUser.IsOnline = true;

//            //    User[] connectedUsers = new User[1] { connectedUser };
//            //    await UserService.Update(connectedUsers);
//            //}


//            return base.OnConnected();
//        }

//        public override Task OnDisconnected(bool stopCalled)
//        {
//            var id = Context.ConnectionId;
//            //set user IsOnline false when he disconnects
//            //User connectedUser = (await UserService.Read(x => x.ConnectionId == Context.ConnectionId)).FirstOrDefault();
//            //if (connectedUser != null)
//            //{
//            //    connectedUser.IsOnline = false;

//            //    User[] connectedUsers = new User[1] { connectedUser };
//            //    await UserService.Update(connectedUsers);
//            //}


//            return base.OnDisconnected(stopCalled);
//        }
//    }
//}