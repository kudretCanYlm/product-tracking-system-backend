using DAS.Core.Infrastructure;
using DAS.Core.Repository.Authentication;
using DAS.Core.Repository.User;
using DAS.Model.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.User
{
	public interface IUserRoleAndDescriptionService
	{
		object AddUserRoleAndDescription(UserRoleAndDescriptionEntity entity);
	}
	public class UserRoleAndDescriptionService:IUserRoleAndDescriptionService
	{
		private readonly ILoginRepository loginRepository;
		private readonly IUserRoleAndDescriptionRepository userRoleAndDescriptionRepository;
		private readonly IUnitOfWork unitOfWork;

		public UserRoleAndDescriptionService(ILoginRepository loginRepository, IUserRoleAndDescriptionRepository userRoleAndDescriptionRepository, IUnitOfWork unitOfWork)
		{
			this.loginRepository = loginRepository;
			this.userRoleAndDescriptionRepository = userRoleAndDescriptionRepository;
			this.unitOfWork = unitOfWork;
		}

		public object AddUserRoleAndDescription(UserRoleAndDescriptionEntity entity)
		{
			throw new NotImplementedException();
		}
	}

}
