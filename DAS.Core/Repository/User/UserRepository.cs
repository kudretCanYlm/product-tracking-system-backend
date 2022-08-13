using DAS.Core.Infrastructure;
using DAS.Model.Model.User;

namespace DAS.Core.Repository.User
{
    public class UserRepository:RepositoryBase<UserEntity>,IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }

    public interface IUserRepository :IRepository<UserEntity>
    {

    }
}
