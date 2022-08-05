using DAS.Core.Infrastructure;
using DAS.Model.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
