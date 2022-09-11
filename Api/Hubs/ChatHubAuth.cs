//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//namespace Api.Hubs
//{
//    public interface IChatAuth
//    {
//        Task<KeyValuePair<Guid, bool>> SignUp(string displayname, string username, string email, string password);
//        Task<KeyValuePair<Guid, bool>> SignIn(string emailorusername, string password);
//        Task<bool> ChangePassword(string emailorusername, string oldpassword, string newpassword);
//    }
//    public partial class ChatHub : IChatAuth
//    {
//        public Task<bool> ChangePassword(string emailorusername, string oldpassword, string newpassword)
//        {
//            //username = username.ToLower();

//            //if (!string.IsNullOrWhiteSpace(email))
//            //{
//            //    email = email.ToLower();
//            //}

//            //if ((await UserService.Read(x => x.Username == username)).FirstOrDefault() != null)
//            //{
//            //    return new KeyValuePair<Guid, bool>(Guid.Empty, false);
//            //}

//            //User user = new()
//            //{
//            //    Id = Guid.NewGuid(),
//            //    Username = username,
//            //    Email = email,
//            //    Password = password,
//            //    DisplayName = displayname,
//            //    ConnectionId = Context.ConnectionId,
//            //    DateCreated = DateTime.UtcNow,
//            //    IsOnline = true
//            //};

//            //User[] users = new User[1] { user };
//            //if (await UserService.Create(users))
//            //{
//            //    return new KeyValuePair<Guid, bool>(user.Id, true);
//            //}

//            //return new KeyValuePair<Guid, bool>(Guid.Empty, false);
//        }

//        public Task<KeyValuePair<Guid, bool>> SignIn(string emailorusername, string password)
//        {
//            //emailorusername = emailorusername.ToLower();

//            //if (PatternMatchHelper.IsEmail(emailorusername))
//            //{
//            //    if ((await UserService.Read(x => x.Email == emailorusername)).FirstOrDefault() == null)
//            //    {
//            //        return new KeyValuePair<Guid, bool>(Guid.Empty, false);
//            //    }

//            //    if ((await UserService.Read(x => x.Email == emailorusername && x.Password == password)).FirstOrDefault() == null)
//            //    {
//            //        return new KeyValuePair<Guid, bool>(Guid.Empty, false);
//            //    }

//            //    User registeredUser = (await UserService.Read(x => x.Email == emailorusername)).FirstOrDefault();
//            //    registeredUser.ConnectionId = Context.ConnectionId;
//            //    registeredUser.IsOnline = true;

//            //    User[] users = new User[1] { registeredUser };
//            //    await UserService.Update(users);

//            //    return new KeyValuePair<Guid, bool>(registeredUser.Id, true);
//            //}
//            //else
//            //{
//            //    if ((await UserService.Read(x => x.Username == emailorusername)).FirstOrDefault() == null)
//            //    {
//            //        return new KeyValuePair<Guid, bool>(Guid.Empty, false);
//            //    }

//            //    if ((await UserService.Read(x => x.Username == emailorusername && x.Password == password)).FirstOrDefault() == null)
//            //    {
//            //        return new KeyValuePair<Guid, bool>(Guid.Empty, false);
//            //    }

//            //    User registeredUser = (await UserService.Read(x => x.Username == emailorusername)).FirstOrDefault();
//            //    registeredUser.ConnectionId = Context.ConnectionId;
//            //    registeredUser.IsOnline = true;

//            //    User[] users = new User[1] { registeredUser };
//            //    await UserService.Update(users);

//            //    return new KeyValuePair<Guid, bool>(registeredUser.Id, true);
//        }

//        public Task<KeyValuePair<Guid, bool>> SignUp(string displayname, string username, string email, string password)
//        {
//            //if (PatternMatchHelper.IsEmail(emailorusername))
//            //{
//            //    User registeredUser = (await UserService.Read(x => x.Email == emailorusername && x.Password == oldpassword)).FirstOrDefault();

//            //    if (registeredUser is null)
//            //    {
//            //        return false;
//            //    }

//            //    registeredUser.Password = newpassword;

//            //    User[] users = new User[1] { registeredUser };
//            //    await UserService.Update(users);

//            //    return true;
//            //}
//            //else
//            //{
//            //    User registeredUser = (await UserService.Read(x => x.Username == emailorusername && x.Password == oldpassword)).FirstOrDefault();

//            //    if (registeredUser is null)
//            //    {
//            //        return false;
//            //    }

//            //    registeredUser.Password = newpassword;

//            //    User[] users = new User[1] { registeredUser };
//            //    await UserService.Update(users);

//            //    return true;
//            //}
//        }
//    }
//}