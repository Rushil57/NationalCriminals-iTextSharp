using System.Web.Mvc;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NationalCriminals.Controllers;

namespace NationalCriminals.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        [TestCategory("Controller:Home | Action:Default:POST")]
        public void Search_Should_Check_Model_Is_Valid()
        {
            var homeController = new HomeController();

            //arrange
            var model = A.Fake<NationalCriminals.Models.PersonViewModels>();

            var result = (homeController.Search(model)) as JsonResult;
            // set up a call to return a value

            Assert.IsNotNull(result);
        }
    }
}
