using DAS.Core.Infrastructure;
using DAS.Core.Repository.Authentication;
using DAS.Model.Base.Enums;
using DAS.Model.Base.Extensions;
using DAS.Model.Model.Authentication;
using DAS.Service.common;
using FluentValidation;
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

        LoginEntity GetUser(Expression<Func<LoginEntity, bool>> where);
        
    }
    public class LoginService:ILoginService
    {
        private readonly ILoginRepository loginRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoginValidation validator;


        public LoginService(ILoginRepository loginRepository,IUnitOfWork unitOfWork,ILoginValidation validator)
        {
            this.loginRepository = loginRepository;
            this.unitOfWork = unitOfWork;
            this.validator = validator;
        }

        public string AddNewUser(LoginEntity loginEntity)
        {
            loginEntity.SetTimeNow(DateTypesEnum.CreatedAt);
            
            

            if (validator.IsValidEntity(loginEntity))
            {
                loginEntity.CreateMD5ForUsernameAndPassword();
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

            return validator.GetValidErrorMessages(loginEntity);
        }

        public LoginEntity GetUser(Expression<Func<LoginEntity, bool>> where)
        {
            return loginRepository.Get(where);
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
