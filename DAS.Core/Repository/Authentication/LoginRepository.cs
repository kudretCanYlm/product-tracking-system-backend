using DAS.Core.Infrastructure;
using DAS.Model.Model.Authentication;

namespace DAS.Core.Repository.Authentication
{
    public class LoginRepository:RepositoryBase<LoginEntity>,ILoginRepository
    {
        public LoginRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }

        public bool IsMailUsing(string email)
        {
            return Get(x => x.Email == email) == default;
        }

        public bool IsUsernameUsing(string  username)
        {
            return Get(x => x.Username == username) == null;
        }
    }

    public interface ILoginRepository : IRepository<LoginEntity>
    {
        bool IsMailUsing(string email);
        bool IsUsernameUsing(string username);
    }
}
