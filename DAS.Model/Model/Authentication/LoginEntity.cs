using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
            this.Password = Password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get ; set ; }
        public DateTime? DeletedAt { get; set ;}        
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
