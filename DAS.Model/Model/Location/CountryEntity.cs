using DAS.Model.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Location
{
    public class CountryEntity:BaseEntity
    {
        public CountryEntity():base()
        {

        }

        public CountryEntity(string countryName,string countryCode)
        {
                this.CountryName = countryName;
                this.CountryCode = countryCode;
        }

        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class CountryValidation : AbstractValidator<CountryEntity>
    {
        public CountryValidation()
        {
            RuleFor(x => x.CountryName).Length(5, 50);
            RuleFor(x => x.CountryCode).NotEmpty();
        }
    }
}
