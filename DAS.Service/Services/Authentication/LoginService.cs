using DAS.Core.Infrastructure;
using DAS.Core.Repository.Authentication;
using DAS.Model.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.Authentication
{
    public interface ILoginService
    {
        //functions
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

        //functions
    }
}
