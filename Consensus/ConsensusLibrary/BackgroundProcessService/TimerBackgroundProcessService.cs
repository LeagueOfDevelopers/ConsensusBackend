using System;
using System.Timers;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using EnsureThat;

namespace ConsensusLibrary.BackgroundProcessService
{
    public class TimerBackgroundProcessService : IBackgroundProcessService
    {
        public TimerBackgroundProcessService(
            BackgroundProcessServiceSettings backgroundProcessServiceSettings,
            IDebateRepository debateRepository)
        {
            _settings = Ensure.Any.IsNotNull(backgroundProcessServiceSettings);
            _debateRepository = Ensure.Any.IsNotNull(debateRepository);
        }

        public void DelayedCheckDebateOverdue(Identifier debateId, DateTimeOffset startDebate)
        {
            Ensure.Any.IsNotNull(debateId);
            Ensure.Any.IsNotDefault(startDebate);

            var timer = new Timer();
            var fromNowToStart = startDebate - DateTimeOffset.UtcNow;
            timer.Interval = fromNowToStart.Add(_settings.AllowedOverdueForDebateStart).TotalMilliseconds;
            timer.Start();

            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                timer.Stop();
                var debate = _debateRepository.GetDebate(debateId);
                debate.CheckDebateStatus();
                _debateRepository.UpdateDebate(debate);
            };

        }

        public void DelayedDebateChangingProcess(Debate debate)
        {

        }

        private readonly BackgroundProcessServiceSettings _settings;
        private readonly IDebateRepository _debateRepository;
    }
}
