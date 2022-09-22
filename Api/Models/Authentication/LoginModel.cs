using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Authentication
{
    public class LoginPostModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginPostModelNewUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}