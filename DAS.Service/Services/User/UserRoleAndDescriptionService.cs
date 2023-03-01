using DAS.Core.Infrastructure;
using DAS.Core.Repository.Authentication;
using DAS.Core.Repository.Project;
using DAS.Core.Repository.User;
using DAS.Model.Model.Project;
using DAS.Model.Model.User;
using DAS.Service.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services.User
{
	public interface IUserRoleAndDescriptionService
	{
		object AddUserRoleAndDescription(UserRoleAndDescriptionEntity entity);
		IEnumerable<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionByRange<TOrder>(int pageNumber, int pageSize, Expression<Func<UserRoleAndDescriptionEntity, TOrder>> orderBy);
	}
	public class UserRoleAndDescriptionService:IUserRoleAndDescriptionService
	{
		private readonly ILoginRepository loginRepository;
		private readonly IUserRoleAndDescriptionRepository userRoleAndDescriptionRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IUserRoleAndDescriptionValidation validator;

		public UserRoleAndDescriptionService(ILoginRepository loginRepository, IUserRoleAndDescriptionRepository userRoleAndDescriptionRepository, IUnitOfWork unitOfWork)
		{
			this.loginRepository = loginRepository;
			this.userRoleAndDescriptionRepository = userRoleAndDescriptionRepository;
			this.unitOfWork = unitOfWork;
		}

		public object AddUserRoleAndDescription(UserRoleAndDescriptionEntity entity)
		{
			entity.CreatedAt = DateTime.Now;
			if(validator.IsValidEntity(entity))
			{
				userRoleAndDescriptionRepository.Add(entity);
				return SaveProjectWithMessage();
			}

			return validator.GetValidErrorMessages(entity);
		}

		public IEnumerable<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionByRange<TOrder>(int pageNumber, int pageSize, Expression<Func<UserRoleAndDescriptionEntity, TOrder>> orderBy)
		{
			var page = new Page(pageNumber, pageSize);
			if (page.Skip < 0)
				return null;
			var userRoleAndDescriptions = userRoleAndDescriptionRepository.GetPage(page, x => true, orderBy).ToList();

			return userRoleAndDescriptions;
		}

		private object SaveProjectWithMessage()
		{
			try
			{
				unitOfWork.Commit();
				return true;
			}
			catch (Exception ex)
			{

				return ex.Message.ToString();
			}
		}
	}

}
