using System;
using System.Collections.Generic;
using System.Linq;
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
