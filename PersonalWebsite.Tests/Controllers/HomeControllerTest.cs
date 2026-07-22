using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalWebsite;
using PersonalWebsite.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PersonalWebsite.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void About()
        {
            // Arrange
            AboutController controller = new AboutController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}
