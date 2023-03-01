using DAS.Core.Infrastructure;
using DAS.Model.Model.User;

namespace DAS.Core.Repository.User
{
	public class UserRoleAndDescriptionRepository : RepositoryBase<UserRoleAndDescriptionEntity>, IUserRoleAndDescriptionRepository
	{
		public UserRoleAndDescriptionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
		{
		}
	}

	public interface IUserRoleAndDescriptionRepository:IRepository<UserRoleAndDescriptionEntity>
	{

	}
}
