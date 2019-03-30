using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsensusLibrary.CategoryContext;
using ConsensusLibrary.Tools;

namespace ConsensusLibrary.DebateContext.Mappers
{
    internal static class DebateMapper
    {
        internal static Debate FromDynamicToDebate(this IEnumerable<dynamic> debateTable)
        {
            return ParseOneDebate(debateTable);
        }

        internal static IEnumerable<Debate> FromDynamicToDebateIEnumerable(this IEnumerable<dynamic> debateTable)
        {
            var result = new List<Debate>();
            debateTable = debateTable.ToList();
            var currentId = Guid.Empty;

            var count = 0;

            foreach (var d in debateTable)
            {
                if (currentId != d.id)
                {
                    currentId = d.id;
                    result.Add(
                        ParseOneDebate(
                            debateTable.ToList().GetRange(count, debateTable.Count() - count)
                            )
                        );
                }

                count++;
            }

            return result;
        }

        private static Debate ParseOneDebate(IEnumerable<dynamic> debateTable)
        {
            var members = new List<DebateMember>();
            var messages = new List<Message>();
            var rounds = new List<Round>();
            var votes = new List<VoteInfo>();
            dynamic debateInfo = null;

            foreach (var debate in debateTable)
            {
                if (debateInfo == null)
                {
                    debateInfo = new
                    {
                        id = new Identifier(debate.id),
                        startDateTime = debate.startdatetime,
                        title = debate.title,
                        category = new Category(debate.category),
                        state = (DebateState)debate.state
                    };
                }

                if (debate.id != debateInfo.id.Id)
                    break;

                if (members.All(m => m.UserIdentifier.Id != debate.memberuserid))
                {
                    members.Add(new DebateMember((MemberRole)debate.memberrole,
                        new Identifier(debate.memberuserid),
                        debate.translationlink, debate.ready));
                }

                if (debate.messageid != null
                    && messages.All(m => m.Id.Id != debate.messageid))
                {
                    messages.Add(new Message(
                        new Identifier(debate.messageid),
                        new Identifier(debate.messageuserid), 
                        new Identifier(debate.id),
                        debate.messagetext, debate.senton));
                }

                if (debate.speakerid != null)
                {
                    var round = new Round(
                        debate.roundstartdatetime,
                        debate.enddatetime,
                        new Identifier(debate.speakerid));

                    if (rounds.All(r => !r.Equals(round)))
                    {
                        rounds.Add(round);
                    }
                }

                if (debate.fromuser != null)
                {
                    var vote = new VoteInfo(debate.fromuser, debate.touser);

                    if (votes.All(v => !v.Equals(vote)))
                    {
                        votes.Add(vote);
                    }
                }
            }

            var firstRound = rounds.FirstOrDefault();
            var roundCount = rounds.Count;
            var roundLength = firstRound.EndRoundDateTime - firstRound.StartRoundDatetime;

            var result = new Debate(members, messages, votes, debateInfo.id,
                debateInfo.startDateTime, debateInfo.title, debateInfo.category, rounds,
                debateInfo.state, roundCount, roundLength);

            return result;
        }
    }
}
