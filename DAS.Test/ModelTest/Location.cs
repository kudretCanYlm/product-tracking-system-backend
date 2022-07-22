using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using DAS.Model.Model.Location;

namespace DAS.Test.ModelTest
{
    [TestClass]
    public class Location
    {
        #region fields
        CityValidation validatorCity = new CityValidation();
        CountryValidation validatorCountry = new CountryValidation(); 
        #endregion

        //cityValidator
        [TestMethod,Obsolete]
        public void Should_have_an_error_when_city_is_length_of_out()
        {
            //arrange
            CityEntity cityEntity = new CityEntity();
            cityEntity.CityName = null;

            //act
            var result = validatorCity.TestValidate(cityEntity);

            //assert
            result.ShouldHaveAnyValidationError();

        }

        //cityValidator
        [TestMethod,Obsolete]
        public void Should_work_when_city_is_length_of_in()
        {
            //arrange
            CityEntity cityEntity = new CityEntity();
            cityEntity.CityName = "Istanbul";

            //act
            var result = validatorCity.TestValidate(cityEntity);

            //assert
            result.ShouldNotHaveError();
        }

        //countryValidator
        [TestMethod]
        public void Should_have_an_error_when_CountryName_is_null()
        {
            //arrange
            CountryEntity countryEntity = new CountryEntity();
            countryEntity.CountryName = null;
            countryEntity.CountryCode = "424asdasd";

            //act
            var result = validatorCountry.TestValidate(countryEntity);

            //assert
            result.ShouldHaveAnyValidationError();
        }

        //countryValidator
        [TestMethod]
        public void Should_have_an_error_when_CountryCode_is_null()
        {
            //arrange
            CountryEntity countryEntity = new CountryEntity();
            countryEntity.CountryName = "test123";
            countryEntity.CountryCode = null;

            //act
            var result = validatorCountry.TestValidate(countryEntity);

            //assert
            result.ShouldHaveAnyValidationError();

        }

        //countryValidator
        [TestMethod]
        public void Should_have_an_error_when_length_of_out()
        {
            //arrange
            CountryEntity countryEntity = new CountryEntity();
            for (int i = 0; i < 50; i++)
            {
                countryEntity.CountryCode += "testCode";
                countryEntity.CountryName += "testCountry";
            }

            //act
            var result = validatorCountry.TestValidate(countryEntity);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.CountryName);
            result.ShouldNotHaveValidationErrorFor(x => x.CountryCode);
        }

        [TestMethod,Obsolete]
        public void Should_work_when_entered_perfectly()
        {
            //arrange
            CountryEntity countryEntity = new CountryEntity();
            countryEntity.CountryName = "Turkiye";
            countryEntity.CountryCode = "+90";

            //act
            var result = validatorCountry.TestValidate(countryEntity);

            //assert
            result.ShouldNotHaveError();

        }

    }
}
