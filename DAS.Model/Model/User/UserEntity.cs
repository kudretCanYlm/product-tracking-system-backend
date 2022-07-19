using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Enums;
using FluentValidation;
using FluentValidation.Validators;
using System;

namespace DAS.Model.Model.User
{
    public class UserEntity:BaseEntity,IBaseTimeEntity
    {
        public UserEntity():base()
        {

        }

        public UserEntity(string name,string surname,string email,string username,string password,RoleEnum userRole=RoleEnum.User):base()
        {
            //name control
            if (String.IsNullOrEmpty(name))
                this.Name = "Empty";
            else
                this.Name = name.ToLower();

            //surname control
            if (String.IsNullOrEmpty(surname))
                this.Surname = surname;
            else
                this.Surname = surname.ToLower();

            //email
            if (String.IsNullOrEmpty(email))
                this.Email = "Empty";
            else
                this.Email = email;

            //username
            if (String.IsNullOrEmpty(username))
                this.Username = "Empty";
            else
                this.Username = username;

            //password
            if (String.IsNullOrEmpty(password))
                this.Password = "Empty";
            else
                this.Password = password;

            //User Role control
            if (userRole != RoleEnum.User)
                this.UserRole = userRole;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum? UserRole { get; set; } = RoleEnum.User;

    }

    public class UserValidation:AbstractValidator<UserEntity>
    {
        public UserValidation()
        {
            RuleFor(x => x.CreatedAt).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.Net4xRegex);
            RuleFor(x => x.Name).Length(5, 50);
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.UserRole).NotNull().NotEmpty();
        }
    }
}
