using DAS.Core.Infrastructure;
using DAS.Model.Model.Company;

namespace DAS.Core.Repository.Company
{
    public class CompanyWorkersRepository:RepositoryBase<CompanyWorkersEntity>, ICompanyWorkersRepository
    {
        public CompanyWorkersRepository(DatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }
    
    public interface ICompanyWorkersRepository : IRepository<CompanyWorkersEntity>
    {

    }
}
