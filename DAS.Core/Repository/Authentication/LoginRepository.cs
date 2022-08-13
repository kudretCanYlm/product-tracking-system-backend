using DAS.Core.Infrastructure;
using DAS.Model.Model.Authentication;

namespace DAS.Core.Repository.Authentication
{
    public class LoginRepository:RepositoryBase<LoginEntity>,ILoginRepository
    {
        public LoginRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }

    public interface ILoginRepository : IRepository<LoginEntity>
    {

    }
}
