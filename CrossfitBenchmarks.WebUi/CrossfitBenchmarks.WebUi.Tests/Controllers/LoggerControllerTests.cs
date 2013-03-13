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
using System.Security.Principal;
using CrossfitBenchmarks.WebUi.Tests.UnitTests;
using System.Web;
using System.Web.Routing;

namespace CrossfitBenchmarks.WebUi.Tests.Controllers
{
    [TestFixture]
    public class LoggerControllerTests
    {

        private static AddLogEntryViewModel GetDataToSave(string dateString, string timeString)
        {
            var dataToSave = new AddLogEntryViewModel();
            dataToSave.DateOfWod = DateTimeOffset.Parse(dateString);
            dataToSave.TimeCreated = timeString;
            return dataToSave;
        }

        [TestCase("01/01/2013", "01:30 AM")]
        [TestCase("01/01/2013", "6:30 PM")]
        public void AddLogEntry_PublishesTo_OpenGraph(string dateString, string timeString)
        {
            
            var item = new WorkoutLogEntryDto { LastEntry = new LogEntryDto { DateOfWod = DateTimeOffset.Now }, LastPersonalRecord = new LogEntryDto { DateOfWod = DateTimeOffset.Now } };
            webServiceApi.Stub(it => it.CreateLogEntry(Arg<LogEntryDto>.Is.NotNull)).Return(item);
            
            ogService.Expect(it => it.PublishAction(Arg<LogEntryDto>.Is.NotNull, Arg<IIdentity>.Is.NotNull, 
                Arg<string>.Is.Null, Arg<bool>.Is.Anything));

            controller.AddLogEntry(GetDataToSave(dateString, timeString));

            ogService.VerifyAllExpectations();

        }

        [TestCase("01/01/2013", "01:30 AM")]
        [TestCase("01/01/2013", "6:30 PM")]
        public void AddLogEntry_Returns_JsonResut(string dateString, string timeString)
        {
            var testResult = new WorkoutLogEntryDto();
            
            webServiceApi.Stub(it => it.CreateLogEntry(Arg<LogEntryDto>.Is.NotNull)).Return(testResult);

            controller.AddLogEntry(GetDataToSave(dateString, timeString))
                .As<CustomJsonResult>()
                .Data.As<AddLogEntryViewModel>();
        }

        [TestCase("benchmark")]
        public void Index_Uses_WebApi(string val)
        {
            List<WorkoutLogEntryDto> testData = new List<WorkoutLogEntryDto>();
            var item = new WorkoutLogEntryDto { LastEntry = new LogEntryDto { DateOfWod = DateTimeOffset.Now }, LastPersonalRecord = new LogEntryDto { DateOfWod = DateTimeOffset.Now } };
            testData.Add(item);

            webServiceApi.Expect(it => it.GetTheBenchmarks()).Return(testData);

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

        [TestCase("theheroes")]
        public void Index_Returns_ExpectedView_ForTheHeros(string arg)
        {
            controller.Index(arg)
                .As<ViewResult>()
                .ViewName.Should().Be("TheHeroes");
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

            ogService = kernel.Get<IOpenGraphServices>();
            controller = kernel.Get<LoggerController>();

            var context = kernel.Get<HttpContextBase>();
            
            IPrincipal principal = kernel.Get<IPrincipal>();
            IIdentity identity = kernel.Get<IIdentity>();
            principal.Stub(it => it.Identity).Return(identity);
            context.Stub(it => it.User).Return(principal);


            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            
        }

        private LoggerController controller;
        private IOpenGraphServices ogService;
        private ICrossfitBenchmarksServices webServiceApi;
    }
}
