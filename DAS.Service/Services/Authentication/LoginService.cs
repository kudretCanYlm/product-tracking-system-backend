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
        LoginEntity GetUser(string username, string password);
        
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
                    unitOfWork.Commit();
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

        public LoginEntity GetUser(string username, string password)
        {
            //apply auto mapping
            throw new NotImplementedException();
        }
    }
}
