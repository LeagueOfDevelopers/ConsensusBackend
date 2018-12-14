using System;
using ConsensusLibrary.BackgroundProcessService;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tests.BackgroundProcessServiceTests.ArgumentsGenerator;
using Xunit;

namespace ConsensusLibrary.Tests.BackgroundProcessServiceTests
{
    public class TimerBackgroundServiceTests
    {
        [Theory]
        [MemberData(nameof(TimerBackgroundServiceTestsGenerator.GenerateForConstructor),
            MemberType = typeof(TimerBackgroundServiceTestsGenerator))]
        public void Constructor_NullParams_Throws(BackgroundProcessServiceSettings settings,
            IDebateRepository repository)
        {
            Assert.Throws<ArgumentNullException>(() => 
                new TimerBackgroundProcessService(settings, repository));
        }
    }
}
