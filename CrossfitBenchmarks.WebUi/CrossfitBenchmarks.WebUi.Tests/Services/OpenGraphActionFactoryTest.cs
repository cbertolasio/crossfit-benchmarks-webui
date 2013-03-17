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

namespace CrossfitBenchmarks.WebUi.Tests.Services
{


    [TestFixture]
    public class OpenGraphActionFactoryTest
    {
        private OpenGraphActionContext GetActionContext(string logEntryType, bool isaPersonalRecord)
        {
            return new OpenGraphActionContext { LogEntryType = logEntryType, IsAPersonalRecord = isaPersonalRecord };
        }

        [TestCase("z", false, ExpectedException=typeof(InvalidOperationException))]
        [TestCase("a", false, ExpectedException = typeof(InvalidOperationException))]
        [TestCase("x", false, ExpectedException = typeof(InvalidOperationException))]
        public void Get_Throws_InvalidOperationException(string logEntryType, bool isaPersonalRecord)
        {
            var context = GetActionContext(logEntryType, isaPersonalRecord);
            factory.Get(context);
        }

        [Test]
        public void FActory_Returns_TheHerosOpenGraphAction()
        {
            var result = factory.Get(GetActionContext("H", false));
            result.Should().BeOfType<TheHeroesOpenGraphAction>();
        }

        [Test]
        public void FActory_Returns_TheGirlsOpenGraphAction()
        {
            var result = factory.Get(GetActionContext("G", false));
            result.Should().BeOfType<TheGirlsOpenGraphAction>();
        }
        [Test]
        public void Factory_Returns_BenchmarkOpenGraphAction()
        {
            var result = factory.Get(GetActionContext("B", false));
            result.Should().BeOfType<BenchmarkOpenGraphAction>();
        }

        [TestCase("B")]
        [TestCase("G")]
        [TestCase("H")]
        [TestCase("A")]
        public void Factory_Returns_PersonalRecordOpenGraphAction(string logEntryType)
        {
            var result = factory.Get(GetActionContext(logEntryType, true));
            result.Should().BeOfType<PersonalRecordOpenGraphAction>();
        }

        [Test]
        public void Factory_Returns_BasicWodOpengraphAction()
        {
            var result = factory.Get(GetActionContext("A", false));
            result.Should().BeOfType<BasicWodOpenGraphAction>();
        }

        [SetUp]
        public void Setup()
        {
            AutomapBootstrap.Initialize();
            RhinoMocksMockingKernel kernel = new RhinoMocksMockingKernel();

            factory = kernel.Get<OpenGraphActionFactory>();
        }
        private OpenGraphActionFactory factory;
    }
}
