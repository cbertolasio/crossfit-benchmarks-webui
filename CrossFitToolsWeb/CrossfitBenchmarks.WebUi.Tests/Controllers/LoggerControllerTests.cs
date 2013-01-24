using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel.RhinoMock;
using CrossFitTools.Web.Controllers;
using CrossfitBenchmarks.WebUi.Services;
using FluentAssertions;
using System.Web.Mvc;
using Rhino.Mocks;
using CrossfitBenchmarks.Data.DataTransfer;

namespace CrossfitBenchmarks.WebUi.Tests.Controllers
{
    [TestFixture]
    public class LoggerControllerTests
    {
        [TestCase("benchmark")]
        public void Index_Uses_WebApi(string val)
        {
            IEnumerable<WorkoutLogEntryDto> testData = new List<WorkoutLogEntryDto>();
            webServiceApi.Expect(it => it.GetTheBenchmarks(Arg<string>.Is.NotNull)).Return(testData);

            controller.Index(val);

            webServiceApi.VerifyAllExpectations();
        }

        [TestCase("benchmark")]
        public void Index_Returns_ExpectedView(string arg)
        {
            controller.Index(arg)
                .As<ViewResult>()
                .ViewName.Should().Be("Benchmarks");
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        [TestCase("fooo")]
        [TestCase("UnknoWnValue")]
        public void Index_Returns_EmptyResult(string val)
        {
            controller.Index(val).Should().BeOfType<EmptyResult>();
        }


        [SetUp]
        public void Setup()
        {
            RhinoMocksMockingKernel kernel = new RhinoMocksMockingKernel();
            webServiceApi = kernel.Get<ICrossfitBenchmarksServices>();
            controller = kernel.Get<LoggerController>();
        }

        private LoggerController controller;
        private ICrossfitBenchmarksServices webServiceApi;
    }
}
