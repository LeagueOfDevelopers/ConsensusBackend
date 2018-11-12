using System;
using System.Linq;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using Xunit;

namespace ConsensusLibrary.Tests.DebateTests
{
    public class DebateLifeTimeTests
    {
        [Fact]
        public void CreateDebate_RoundsPassed_IsRoundsGenerationCorrect()
        {
            //Arrange
            var debateStartDate = DateTimeOffset.UtcNow;
            var title = "Test Debates";
            var inviterId = new Identifier(Guid.NewGuid());
            var invitedId = new Identifier(Guid.NewGuid());
            var debateCategory = DebateCategory.Home;
            var expectedRoundCount = 4;
            var roundLength = new TimeSpan(0, 0, 5, 0);

            var expectedEndFirstRound = debateStartDate.Add(roundLength);
            var expectedEndLastRound = debateStartDate.Add(roundLength * expectedRoundCount);
            var expectedFirstRoundSpeaker = inviterId;
            var expectedLastRoundSpeaker = invitedId;
            //Act
            var newDebate = new Debate(debateStartDate, title, inviterId,
                invitedId, debateCategory, expectedRoundCount, roundLength);

            var firstRound = newDebate.Rounds.First();
            var lastRound = newDebate.Rounds.Last();
            //Assert
            Assert.Equal(expectedRoundCount, newDebate.Rounds.Count());
            Assert.Equal(expectedEndFirstRound, firstRound.EndRoundDateTime);
            Assert.Equal(expectedEndLastRound, lastRound.EndRoundDateTime);
            Assert.Equal(expectedFirstRoundSpeaker, firstRound.SpeakerId);
            Assert.Equal(expectedLastRoundSpeaker, lastRound.SpeakerId);
        }

        [Fact]
        public void StartDebate_TryToStartBattle_StateChanged()
        {
            //Arrange
            var debateStartDate = DateTimeOffset.UtcNow;
            var title = "Test Debates";
            var inviterId = new Identifier(Guid.NewGuid());
            var invitedId = new Identifier(Guid.NewGuid());
            var debateCategory = DebateCategory.Home;
            var roundCount = 4;
            var roundLength = new TimeSpan(0, 0, 5, 0);
            //Act
            var newDebates = new Debate(debateStartDate, title, inviterId, invitedId, debateCategory, roundCount, roundLength);
            newDebates.SetReadyStatus(inviterId);
            newDebates.SetReadyStatus(invitedId);
            //Assert
            Assert.NotEqual(debateStartDate, newDebates.StartDateTime);
            Assert.True(newDebates.State == DebateState.Approved);
        }
    }
}
