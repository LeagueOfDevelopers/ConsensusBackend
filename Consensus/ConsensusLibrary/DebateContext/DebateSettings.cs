namespace ConsensusLibrary.DebateContext
{
    public class DebateSettings
    {
        public int DebateMinutesDuration { get; }

        public DebateSettings(int debateMinutesDuration)
        {
            DebateMinutesDuration = debateMinutesDuration;
        }
    }
}
