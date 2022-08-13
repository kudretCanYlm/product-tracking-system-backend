using DAS.Core.Infrastructure;
using DAS.Core.Repository.User;
using DAS.Model.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.User
{
    public interface IUserService
    {
        //functions
    }

    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserValidation validator = new UserValidation();

        public UserService(IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        //functions
    }
}
