using DAS.Core.Infrastructure;
using DAS.Model.Model.Location;

namespace DAS.Core.Repository.Location
{
    public class CountryRepository:RepositoryBase<CountryEntity>,ICountryRepository
    {
        public CountryRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {

        }
    }

    public interface ICountryRepository:IRepository<CountryEntity>
    {

    }
}
