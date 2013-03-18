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
using CrossfitBenchmarks.WebUi.Controllers;

namespace CrossfitBenchmarks.WebUi.Tests.Controllers
{
    [TestFixture]
    public class WorkoutControllerTests
    {
        [Test]
        public void History_Returns_ExpectedView()
        {
            var result = controller.History();
                ((ViewResult)result).ViewName.Should().Be("History");
        }

        [SetUp]
        public void Setup()
        {
            RhinoMocksMockingKernel kernel = new RhinoMocksMockingKernel();
            controller = kernel.Get<WorkoutController>();
        }


        private WorkoutController controller;
    }
}

