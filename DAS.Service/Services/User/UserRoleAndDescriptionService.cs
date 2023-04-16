using DAS.Core.Infrastructure;
using DAS.Core.PagingAndFiltering;
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
		IPagedList<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionByRangeWithFilterAndSorting(PageSearchArgs args);
	}
	public class UserRoleAndDescriptionService:IUserRoleAndDescriptionService
	{
		private readonly ILoginRepository loginRepository;
		private readonly IUserRoleAndDescriptionRepository userRoleAndDescriptionRepository;
		private readonly IUnitOfWork unitOfWork;
		private readonly IUserRoleAndDescriptionValidation validator;

		public UserRoleAndDescriptionService(ILoginRepository loginRepository, IUserRoleAndDescriptionRepository userRoleAndDescriptionRepository, IUnitOfWork unitOfWork, IUserRoleAndDescriptionValidation validator)
		{
			this.loginRepository = loginRepository;
			this.userRoleAndDescriptionRepository = userRoleAndDescriptionRepository;
			this.unitOfWork = unitOfWork;
			this.validator = validator;
		}

		public object AddUserRoleAndDescription(UserRoleAndDescriptionEntity entity)
		{
			entity.CreatedAt = DateTime.Now;
			if(validator.IsValidEntity(entity))
			{
				userRoleAndDescriptionRepository.Add(entity);
				return SaveUserRoleAndDescriptionWithMessage();
			}

			return validator.GetValidErrorMessages(entity);
		}

		public IEnumerable<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionByRange<TOrder>(int pageNumber, int pageSize, Expression<Func<UserRoleAndDescriptionEntity, TOrder>> orderBy)
		{
			var page = new Page(pageNumber, pageSize);
			if (page.Skip < 0)
				return null;
			var userRoleAndDescriptions = userRoleAndDescriptionRepository.GetUserRoleAndDescriptionsByPage(page, x => true, orderBy).ToList();

			return userRoleAndDescriptions;
		}

		public IPagedList<UserRoleAndDescriptionEntity> GetUserRoleAndDescriptionByRangeWithFilterAndSorting(PageSearchArgs args)
		{
			var userRoleAndDescription = userRoleAndDescriptionRepository.GetUserRoleAndDescriptionsByPagedList(args);

			return userRoleAndDescription;
		}

		private object SaveUserRoleAndDescriptionWithMessage()
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
