using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel.RhinoMock;
using CrossFitTools.Web.Controllers;
using CrossfitBenchmarks.WebUi.Services;
using FluentAssertions;
using System.Web.Mvc;
using Rhino.Mocks;
using CrossfitBenchmarks.Data.DataTransfer;
using CrossfitBenchmarks.WebUi.Models.Logger;
using CrossFitTools.Web.CustomActionResults;

namespace CrossfitBenchmarks.WebUi.Tests.Controllers
{
    [TestFixture]
    public class LoggerControllerTests
    {
        [Test]
        public void AddLogEntry_Returns_JsonResut()
        {
            var testResult = new LogEntryDto();
            webServiceApi.Stub(it => it.CreateLogEntry(Arg<LogEntryDto>.Is.NotNull)).Return(testResult);
            var dataToSave = new AddLogEntryViewModel();
            controller.AddLogEntry(dataToSave)
                .As<CustomJsonResult>()
                .Data.As<AddLogEntryViewModel>();
        }

        [TestCase("benchmark")]
        public void Index_Uses_WebApi(string val)
        {
            IEnumerable<WorkoutLogEntryDto> testData = new List<WorkoutLogEntryDto>();
            webServiceApi.Expect(it => it.GetTheBenchmarks(Arg<string>.Is.NotNull)).Return(testData);

            controller.Index(val);

            webServiceApi.VerifyAllExpectations();
        }


        [TestCase("benchmark")]
        public void Index_Returns_ExpectedView_ForTheBenchmarks(string arg)
        {
            controller.Index(arg)
                .As<ViewResult>()
                .ViewName.Should().Be("Benchmarks");
        }

        [TestCase("thegirls")]
        public void Index_Returns_ExpectedView_ForTheGirls(string arg)
        {
            controller.Index(arg)
                .As<ViewResult>()
                .ViewName.Should().Be("TheGirls");
        }

        [TestCase("theheros")]
        public void Index_Returns_ExpectedView_ForTheHeros(string arg)
        {
            controller.Index(arg)
                .As<ViewResult>()
                .ViewName.Should().Be("TheHeros");
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
            AutomapBootstrap.Initialize();
            RhinoMocksMockingKernel kernel = new RhinoMocksMockingKernel();
            webServiceApi = kernel.Get<ICrossfitBenchmarksServices>();
            controller = kernel.Get<LoggerController>();
        }

        private LoggerController controller;
        private ICrossfitBenchmarksServices webServiceApi;
    }
}
