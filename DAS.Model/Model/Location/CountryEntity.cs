using DAS.Model.Base;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Location
{
    [DataContract]
    public class CountryEntity:BaseEntity
    {
        public CountryEntity():base()
        {

        }

        public CountryEntity(string countryName,string countryCode):base()
        {
                this.CountryName = countryName;
                this.CountryCode = countryCode;
        }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
    }

    public interface ICountryValidation : IValidator<CountryEntity>
    {

    }

    public class CountryValidation : AbstractValidator<CountryEntity>,ICountryValidation
    {
        public CountryValidation()
        {
            RuleFor(x => x.CountryName)
                .Length(5, 50).WithMessage("name length must be 5-50")
                .NotNull().WithMessage("don't be null")
                .NotEmpty().WithMessage("don't be empty");

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("don't be empty")
                .NotNull().WithMessage("don't be null");
        }
    }
}
