using DAS.Core.Infrastructure;
using DAS.Core.Repository.Location;
using DAS.Model.Model.Location;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Service.Services
{
    public interface ICountyService
    {
        IEnumerable<CountryEntity> GetCountries();

        IEnumerable<CountryEntity> GetCountriesWithFilter(Expression<Func<CountryEntity, bool>> filter);
        string AddCountry(CountryEntity countryEntity);
        void SaveCountry();
    }

    public class CountryService : ICountyService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly CountryValidation validator = new CountryValidation();


        public CountryService(ICountryRepository countryRepository,IUnitOfWork unitOfWork)
        {
            this.countryRepository = countryRepository;
            this.unitOfWork = unitOfWork;
        }

        public string AddCountry(CountryEntity countryEntity)
        {

            ValidationResult result = validator.Validate(countryEntity);

            if (result.IsValid)
            {
                countryRepository.Add(countryEntity);
                SaveCountry();

                try
                {
                    SaveCountry();
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }

                return "saved";
            }

            StringBuilder errors = new StringBuilder();

            foreach (var error in result.Errors)
                errors.AppendLine(error.ErrorMessage);

            return errors.ToString();
        }

        public IEnumerable<CountryEntity> GetCountries()
        {
            var countries = countryRepository.GetAll();

            return countries;
        }

        public IEnumerable<CountryEntity> GetCountriesWithFilter(Expression<Func<CountryEntity,bool>> filter)
        {
            var countries = countryRepository.GetMany(filter);

            return countries;
        }

        public void SaveCountry()
        {
            unitOfWork.Commit();
        }
    }
}
