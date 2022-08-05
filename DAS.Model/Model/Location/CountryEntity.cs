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

    public class CountryValidation : AbstractValidator<CountryEntity>
    {
        public CountryValidation()
        {
            RuleFor(x => x.CountryName).Length(5, 50).
                NotNull().
                WithMessage("Country name length must be between 5-50").
                NotEmpty().
                WithMessage("Country name length must be between 5-50");

            RuleFor(x => x.CountryCode).NotEmpty().NotNull().WithMessage("Country code not null or empty");
        }
    }
}
