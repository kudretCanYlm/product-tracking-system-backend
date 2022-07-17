using DAS.Model.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAS.Model.Model.Location
{
    public class CityEntity:BaseEntity
    {
        public CityEntity():base()
        {

        }

        public CityEntity(string cityName):base()
        {
            if (String.IsNullOrEmpty(cityName))
                this.CityName = "Empty";
            else
                this.CityName = cityName;
        }

        public string CityName { get; set; }

        public Guid CountryId { get; set; }

        [ForeignKey("CountryId")]
        public CountryEntity Country { get; set; }
    }

    public class CityValidation:AbstractValidator<CityEntity>
    {
        public CityValidation()
        {
            RuleFor(x => x.CityName).Length(5, 50);
            RuleFor(x => x.CountryId).NotEmpty();
        }
    }

}
