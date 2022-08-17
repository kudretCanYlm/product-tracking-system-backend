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
            return Get(x => x.Email == email) != default;
        }

        public bool IsUsernameUsing(string  username)
        {
            return Get(x => x.Username == username) != null;
        }
        public LoginEntity GetUser(string username, string password)
        {
            LoginEntity login = new LoginEntity();

            login.Name = null;
            login.Surname = null;
            login.Username = username;
            login.Password = password;
            login.CreateMD5ForUsernameAndPassword();

            return Get(x => x.Username == login.Username && x.Password == login.Password) ?? login;
        }
    }

    public interface ILoginRepository : IRepository<LoginEntity>
    {
        bool IsMailUsing(string email);
        bool IsUsernameUsing(string username);
        LoginEntity GetUser(string username, string password);
    }
}
