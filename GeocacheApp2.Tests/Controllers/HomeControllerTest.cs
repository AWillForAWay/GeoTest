using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeocacheApp2;
using GeocacheApp2.Controllers;
using System.Threading.Tasks;

namespace GeocacheApp2.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Geocaching App", result.ViewBag.Title);
        }
    }
}
