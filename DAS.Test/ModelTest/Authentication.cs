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
    public class Authentication
    {

        #region fields
        LoginValidation validator = new LoginValidation(); 
        #endregion

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

        [TestMethod]
        public void Should_have_error_when_Username_and_Password_and_createdat_is_range_of_out()
        {
            //arrange
            LoginEntity loginEntity = new LoginEntity();
            loginEntity.CreatedAt = DateTime.Now;
            
            for (int i = 0; i < 60; i++)
            {
                loginEntity.Username += "user";
                loginEntity.Password += "password";
            }

            //act
            var results = validator.TestValidate(loginEntity);

            //assert
            results.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Should_have_error_when_Username_and_Password_and_createdat_is_range_of_out_2()
        {
            //arrange
            LoginEntity loginEntity = new LoginEntity();
            loginEntity.CreatedAt = DateTime.Now;
            loginEntity.Username = "a";
            loginEntity.Password = "a";

            //act
            var result = validator.TestValidate(loginEntity);

            //asserts
            result.ShouldHaveAnyValidationError();

        }

        [TestMethod, Obsolete]
        public void Should_work_successly_when_created_as_perfectly()
        {
            //arrange
            LoginEntity loginEntity = new LoginEntity();
            loginEntity.CreatedAt = DateTime.Now;
            loginEntity.Username = "kudret123";
            loginEntity.Password = "kudret123";

            //act
            var result = validator.TestValidate(loginEntity);

            //assert
            result.ShouldNotHaveError();
        }

        [TestMethod, Obsolete]
        public void Should_have_an_error_when_just_createdat_null()
        {
            //arrange
            LoginEntity loginEntity = new LoginEntity();
            loginEntity.CreatedAt = null;
            loginEntity.Username = "kudret123";
            loginEntity.Password = "kudret123";

            //act
            var result = validator.TestValidate(loginEntity);

            //assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
