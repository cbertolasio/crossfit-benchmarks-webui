using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CrossFitTools.Web.Controllers;
using Ninject.MockingKernel.RhinoMock;
using Ninject;
using System.Web.Mvc;
using FluentAssertions;
namespace CrossfitBenchmarks.WebUi.Tests.Controllers
{

    [TestFixture]
    public class HomeControllerTests
    {

        [Test]
        public void Terms_Returns_Expected_View()
        {
            var result = controller.Terms();

            ((ViewResult)result).ViewName.Should().Be("Terms");
        }
        [Test]
        public void Privacy_Returns_Expected_View()
        {
            var result = controller.Privacy();
            ((ViewResult)result).ViewName.Should().Be("Privacy");
        }

        [SetUp]
        public void Setup()
        {
            RhinoMocksMockingKernel kernel = new RhinoMocksMockingKernel();
            controller = kernel.Get<HomeController>();
        }
        private HomeController controller;

    }
}
