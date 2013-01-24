using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CrossfitBenchmarks.WebUi.Tests
{
    [TestFixture]
    public class AutomapBootstrapTests
    {
        [Test]
        public void MappingsAreValid()
        {
            AutomapBootstrap.Initialize();
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}
