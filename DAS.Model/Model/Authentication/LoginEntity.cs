using DAS.Model.Base;
using DAS.Model.Base.Enums;
using DAS.Model.Base.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Authentication
{
    public class LoginEntity:BaseEntity,IBaseTimeEntity
    {
        public LoginEntity():base()
        {

        }

        public LoginEntity(string username,string password):base()
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get; set ;}

        //we will add recursive function
        public void CreateMD5ForUsernameAndPassword()
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytesUsername = Encoding.ASCII.GetBytes(this.Username);
                byte[] inputBytesPassword = Encoding.ASCII.GetBytes(this.Password);

                byte[] hashBytesUsername = md5.ComputeHash(inputBytesUsername);
                byte[] hashBytesPassword = md5.ComputeHash(inputBytesPassword);

                // Convert the byte array to hexadecimal string
                StringBuilder sbUserName = new StringBuilder();
                StringBuilder sbPassword = new StringBuilder();

                for (int i = 0; i < hashBytesUsername.Length; i++)
                    sbUserName.Append(hashBytesUsername[i].ToString("X2"));

                for (int i = 0; i < hashBytesPassword.Length; i++)
                    sbPassword.Append(hashBytesPassword[i].ToString("X2"));

                this.Username = sbUserName.ToString();
                this.Password = sbPassword.ToString();
            }
        }
    }

    public class LoginValidation:AbstractValidator<LoginEntity>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Username).Length(5, 50).NotEmpty().NotNull();
            RuleFor(x => x.Password).Length(5, 50).NotEmpty().NotNull();
            RuleFor(x => x.CreatedAt).NotEmpty().NotNull();
        }
    }
}
