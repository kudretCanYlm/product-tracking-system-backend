using DAS.Model.Model.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.TestHelper;

namespace DAS.Test.ModelTest
{
    [TestClass]
    public class PersonValidatorTest
    {

        LoginValidation validator = new LoginValidation();

        [TestMethod]
        public void Should_have_error_when_Username_and_Password_and_created_is_null()
        {
            //arrange
            LoginEntity loginEntity = new LoginEntity();
            loginEntity.CreatedAt = null;

            //act
            var result= validator.TestValidate(loginEntity);

            //assert
            result.ShouldHaveAnyValidationError();

        }
    }
}
