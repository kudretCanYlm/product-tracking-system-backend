using DAS.Core.Infrastructure;
using DAS.Model.Model.Company;

namespace DAS.Core.Repository.Company
{
    public class CompanyRepository:RepositoryBase<CompanyEntity>,ICompanyRepository
    {
        public CompanyRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }
    public interface ICompanyRepository : IRepository<CompanyEntity>
    {

    }
}
