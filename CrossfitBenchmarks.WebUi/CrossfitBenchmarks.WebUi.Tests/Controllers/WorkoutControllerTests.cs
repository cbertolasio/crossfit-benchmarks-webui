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
using CrossfitBenchmarks.WebUi.Services;
using Rhino.Mocks;

namespace CrossfitBenchmarks.WebUi.Tests.Controllers
{
    [TestFixture]
    public class WorkoutControllerTests
    {
        [Test]
        public void History_Returns_ExpectedView()
        {
            webServicesApi.Stub(it => it.GetWorkoutHistory(Arg<int>.Is.GreaterThan(0))).Return(Properties.Resources.TestJson);

            var result = controller.History(123);
                ((ViewResult)result).ViewName.Should().Be("History");
        }

        [Test]
        public void History_GetsWorkoutHistory_FromService()
        {
            var jsonData = Properties.Resources.TestJson;
            var logEntryId = 254;
            webServicesApi.Expect(it => it.GetWorkoutHistory(logEntryId)).Return(jsonData);

            controller.History(logEntryId);

        }

        [SetUp]
        public void Setup()
        {
            RhinoMocksMockingKernel kernel = new RhinoMocksMockingKernel();
            controller = kernel.Get<WorkoutController>();
            webServicesApi = kernel.Get<ICrossfitBenchmarksServices>();
        }


        private ICrossfitBenchmarksServices webServicesApi;
        private WorkoutController controller;
    }
}

