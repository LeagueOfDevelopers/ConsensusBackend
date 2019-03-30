using System;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using EnsureThat;
using Hangfire;

namespace ConsensusLibrary.BackgroundProcessService
{
    public class HangFireProcessService : IBackgroundProcessService
    {
        public HangFireProcessService(
            BackgroundProcessServiceSettings settings,
            IDebateRepository repository,
            string connectionString)
        {
            _settings = Ensure.Any.IsNotNull(settings);
            _connectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString);
            _debateRepository = Ensure.Any.IsNotNull(repository);

            GlobalConfiguration.Configuration.UseSqlServerStorage(_connectionString);
            _server = new BackgroundJobServer();
        }

        public void DelayedCheckDebateOverdue(Identifier debateId, DateTimeOffset startDebate)
        {
            Ensure.Any.IsNotNull(debateId);

            var fromNowToStart = startDebate - DateTimeOffset.UtcNow;
            fromNowToStart = fromNowToStart.Add(_settings.AllowedOverdueForDebateStart);

            BackgroundJob.Schedule(() => CheckDebateOverdue(debateId), fromNowToStart);
        }

        public void DelayedDebateChangingProcess(Debate debate)
        {
            throw new NotImplementedException();
        }

        private void CheckDebateOverdue(Identifier debateId)
        {
            var debate = _debateRepository.GetDebate(debateId);
            debate.CheckDebateStatus();
            _debateRepository.UpdateDebateStatus(debate);
        }

        private readonly BackgroundProcessServiceSettings _settings;
        private readonly string _connectionString;
        private readonly BackgroundJobServer _server;
        private readonly IDebateRepository _debateRepository;
    }
}
