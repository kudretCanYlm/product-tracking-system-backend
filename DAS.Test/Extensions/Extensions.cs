using DAS.Model.Model.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using DAS.Model.Base.Extensions;
using DAS.Model.Base.Enums;

namespace DAS.Test.Extensions
{
    [TestClass]
    public class Extensions
    {

        #region field
        OrderDetailsValidation validator = new OrderDetailsValidation(); 
        #endregion

        //EntitiesExtencions
        [TestMethod]
        public void Should_work_SetTimeNow_method()
        {
            //arrange
            OrderDetailsEntity orderDetails = new OrderDetailsEntity();
            orderDetails.SetTimeNow();
            orderDetails.ModifiedAt = new DateTime(2022, 7, 24, 20, 5, 5);

            //act
            var result = validator.TestValidate(orderDetails);

            //assert
            result.ShouldNotHaveValidationErrorFor(x => x.CreatedAt);

        }

        [TestMethod]
        public void Should_work_DiffrenceOf_methods()
        {
            //arrange
            OrderDetailsEntity orderDetails = new OrderDetailsEntity();
            orderDetails.SetTimeNow();
            orderDetails.ModifiedAt = new DateTime(2022,10,24,20,5,5);
            orderDetails.DeletedAt = new DateTime(2022,11,15,6,1,6);

            //act
            TimeSpan diff_a = orderDetails.DiffrenceOfCreatedAtAndDeletedAt();
            TimeSpan diff_b = orderDetails.DiffrenceOfCreatedAtAndModifiedAt();
            TimeSpan diff_c = orderDetails.DiffrenceOfModifiedAtAndDeletedAt();

            //assert
            Assert.IsNotNull(diff_a, " DiffrenceOfCreatedAtAndDeletedAt didnt work");
            Assert.IsNotNull(diff_b, " DiffrenceOfCreatedAtAndModifiedAt didnt work");
            Assert.IsNotNull(diff_c, " DiffrenceOfModifiedAtAndDeletedAt didnt work");

        }
    }
}
