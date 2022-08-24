using DAS.Core.Infrastructure;
using DAS.Core.Repository.Authentication;
using DAS.Model.Base.Enums;
using DAS.Model.Base.Extensions;
using DAS.Model.Model.Authentication;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Authentication
{
    public interface ILoginService
    {
        string AddNewUser(LoginEntity loginEntity);
        LoginEntity LogIn(string username, string password);
        Guid? GetUserId(string usernameMd5);
        
    }
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository loginRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly LoginValidation validator = new LoginValidation();

        public LoginService(ILoginRepository loginRepository,IUnitOfWork unitOfWork)
        {
            this.loginRepository = loginRepository;
            this.unitOfWork = unitOfWork;
        }

        public string AddNewUser(LoginEntity loginEntity)
        {
            loginEntity.SetTimeNow(DateTypesEnum.CreatedAt);
            
            ValidationResult result = validator.Validate(loginEntity);

            loginEntity.CreateMD5ForUsernameAndPassword();

            if (result.IsValid)
            {
                bool isEmailUsing = loginRepository.IsMailUsing(loginEntity.Email);
                bool isUsernameUsing = loginRepository.IsUsernameUsing(loginEntity.Username);

                if (isEmailUsing)
                    return "this Mail is already using";
                if (isUsernameUsing)
                    return "this Username is already using";

                
                loginRepository.Add(loginEntity);

                try
                {
                    Savelogin();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                return "New user added as successfuly";
                
            }

            StringBuilder errors = new StringBuilder();

            foreach (var error in result.Errors)
                errors.AppendLine(error.ErrorMessage);

            return errors.ToString();
        }

        public Guid? GetUserId(string usernameMd5)
        {
            return loginRepository.Get(x => x.Username == usernameMd5).Id;
        }

        public LoginEntity LogIn(string username, string password)
        {
            LoginEntity user = loginRepository.GetUser(username, password);

            if (user.Name == null)
                return null;

            user.Password = "";


            return user;
            
        }

        private void Savelogin()
        {
            unitOfWork.Commit();
        }
    }
}
