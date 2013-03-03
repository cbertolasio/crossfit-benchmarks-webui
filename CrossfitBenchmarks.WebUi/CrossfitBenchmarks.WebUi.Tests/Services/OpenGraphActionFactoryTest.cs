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
        [TestCase("z", false, ExpectedException=typeof(InvalidOperationException))]
        [TestCase("a", false, ExpectedException = typeof(InvalidOperationException))]
        [TestCase("x", false, ExpectedException = typeof(InvalidOperationException))]
        public void Get_Throws_InvalidOperationException(string logEntryType, bool isaPersonalRecord)
        {
            factory.Get(logEntryType, isaPersonalRecord);
        }

        [Test]
        public void FActory_Returns_TheHerosOpenGraphAction()
        {
            var result = factory.Get("H", false);
            result.Should().BeOfType<TheHerosOpenGraphAction>();
        }

        [Test]
        public void FActory_Returns_TheGirlsOpenGraphAction()
        {
            var result = factory.Get("G", false);
            result.Should().BeOfType<TheGirlsOpenGraphAction>();
        }
        [Test]
        public void Factory_Returns_BenchmarkOpenGraphAction()
        {
            var result = factory.Get("B", false);
            result.Should().BeOfType<BenchmarkOpenGraphAction>();
        }

        [Test]
        public void Factory_Returns_PersonalRecordOpenGraphAction()
        {
            var result = factory.Get("B", true);
            result.Should().BeOfType<PersonalRecordOpenGraphAction>();
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
