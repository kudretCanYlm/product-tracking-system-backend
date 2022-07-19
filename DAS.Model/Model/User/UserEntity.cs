using DAS.Model.Base;
using DAS.Model.Base.Interfaces;
using DAS.Model.Model.Authentication;
using DAS.Model.Model.Enums;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAS.Model.Model.User
{
    public class UserEntity:BaseEntity,IBaseTimeEntity
    {
        public UserEntity():base()
        {

        }

        public UserEntity(string name,string surname,string email,string username,string password,RoleEnum userRole=RoleEnum.User):base()
        {
                this.Name = name.ToLower();
                this.Surname = surname.ToLower();
                this.Email = email;
                this.UserRole = userRole;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Guid LoginId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public RoleEnum? UserRole { get; set; } = RoleEnum.User;

        [ForeignKey("LoginId")]
        public LoginEntity Login { get; set; }

    }

    public class UserValidation:AbstractValidator<UserEntity>
    {
        public UserValidation()
        {
            RuleFor(x => x.CreatedAt).NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.Net4xRegex);
            RuleFor(x => x.Name).Length(5, 50);
            RuleFor(x => x.UserRole).NotNull().NotEmpty();
        }
    }
}
