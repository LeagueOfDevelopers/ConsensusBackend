using System;
using System.Collections.Generic;
using ConsensusLibrary.BackgroundProcessService;
using ConsensusLibrary.DebateContext;
using Xunit;

namespace ConsensusLibrary.Tests.BackgroundProcessServiceTests.ArgumentsGenerator
{
    public class TimerBackgroundServiceTestsGenerator
    {
        public static IEnumerable<object[]> GenerateForConstructor()
        {
            yield return new object[]
            {
                null,
                new InMemoryDebateRepository()
            };

            yield return new object[]
            {
                new BackgroundProcessServiceSettings(TimeSpan.MinValue, TimeSpan.MinValue),
                null
            };
        }
    }
}
