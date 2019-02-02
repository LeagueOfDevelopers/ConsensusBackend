using ConsensusLibrary.Tools;
using Microsoft.AspNetCore.Http;

namespace ConsensusLibrary.DebateContext.Views
{
    public class LiveDebateView
    {
        public LiveDebateView(
            Identifier id,
            string title,
            Identifier firstDebaterId,
            Identifier secondDebaterId,
            string firstDebaterName,
            string secondDebaterName,
            int spectatorsCount,
            string theme,
            IFormFile thumbnail)
        {
            Id = id;
            Title = title;
            FirstDebaterId = firstDebaterId;
            SecondDebaterId = secondDebaterId;
            FirstDebaterName = firstDebaterName;
            SecondDebaterName = secondDebaterName;
            SpectatorsCount = spectatorsCount;
            Theme = theme;
            Thumbnail = thumbnail;
        }

        public Identifier Id { get; }
        public string Title { get; }
        public Identifier FirstDebaterId { get; }
        public Identifier SecondDebaterId { get; }
        public string FirstDebaterName { get; }
        public string SecondDebaterName { get; }
        public int SpectatorsCount { get; }
        public string Theme { get; }
        public IFormFile Thumbnail { get; }
    }
}