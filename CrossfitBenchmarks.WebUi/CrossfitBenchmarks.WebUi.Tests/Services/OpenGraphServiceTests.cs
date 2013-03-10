using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ninject.MockingKernel.RhinoMock;
using CrossfitBenchmarks.WebUi.Services;
using Ninject;
using FluentAssertions;
using Rhino.Mocks;
using CrossfitBenchmarks.Data.DataTransfer;
using System.Security.Principal;
using System.Security.Claims;

namespace CrossfitBenchmarks.WebUi.Tests.Services
{
    [TestFixture]
    public class OpenGraphServiceTests
    {
        /// <remarks>
        /// logEntryType and isAPersonalRecord simply pass through to the factory
        /// </remarks>
        [TestCase("G", true)]
        [TestCase("G", false)]
        [TestCase("H", false)]
        [TestCase("H", true)]
        [TestCase("B", true)]
        [TestCase("B", false)]
        public void Service_Gets_Action_From_Factory(string logEntryType, bool isAPersonalRecord)
        {
            var action = kernel.Get<IOpenGraphAction>();
            factory.Expect(it => it.Get(Arg<OpenGraphActionContext>.Is.Anything)).Return(action);

            service.PublishAction(dto, identity, logEntryType, isAPersonalRecord);

            factory.VerifyAllExpectations();
            
        }

        [Test]
        public void Service_Publishes_Action()
        {
            var action = kernel.Get<IOpenGraphAction>();
            factory.Stub(it => it.Get(Arg<OpenGraphActionContext>.Is.Anything)).Return(action);

            action.Expect(it => it.Publish()).Return("newActionIdentifier");

            service.PublishAction(dto, identity, "anyValidLogEntryType", true);

            action.VerifyAllExpectations();
        }

        [SetUp]
        public void Setup()
        {
            AutomapBootstrap.Initialize();
            kernel = new RhinoMocksMockingKernel();
            factory = kernel.Get<IOpenGraphActionFactory>();
            service = kernel.Get<OpenGraphService>();

            dto = kernel.Get<LogEntryDto>();
            identity = kernel.Get<ClaimsIdentity>();
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Facebook-460497347351482"));
        }

        //private IOpenGraphAction action;
        private LogEntryDto dto;
        private ClaimsIdentity identity;
        private RhinoMocksMockingKernel kernel;
        private OpenGraphService service;
        private IOpenGraphActionFactory factory;
    }
}
