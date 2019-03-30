using System;
using System.Collections.Generic;
using System.Data;
using ConsensusLibrary.DebateContext.Mappers;
using ConsensusLibrary.Tools;
using Dapper;
using EnsureThat;
using Newtonsoft.Json.Serialization;
using Npgsql;

namespace ConsensusLibrary.DebateContext
{
    public class InPostgreSqlDebateRepository : IDebateRepository
    {
        public void AddDebate(Debate newDebate)
        {
            Ensure.Any.IsNotNull(newDebate);

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string createDebateQuery = 
                    "INSERT INTO debates (id, startdatetime, title, category, " +
                    "roundcount, roundlength, state) VALUES (@Id, @StartDateTime, " +
                    "@Title, @Category, @RoundCount, @RoungLength, @State);";

                connection.Open();

                using(var transaction = connection.BeginTransaction()) { 
                    connection.Execute(createDebateQuery, new
                    {
                        Id = newDebate.Identifier.Id,
                        StartDateTime = newDebate.StartDateTime,
                        Title = newDebate.Title,
                        Category = newDebate.DebateCategory.CategoryTitle,
                        RoundCount = newDebate.RoundCount,
                        RoungLength = newDebate.RoundLength,
                        State = newDebate.State
                    }, transaction: transaction);

                    transaction.Commit();
                }

                const string createDebateMemberQuery =
                    "INSERT INTO members (userid, debateid, memberrole, translationlink, " +
                    "ready) VALUES (@UserId, @DebateId, @MemberRole, @TranslationLink, " +
                    "@Ready)";

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var m in newDebate.Members)
                    {
                        connection.Execute(createDebateMemberQuery, new
                        {
                            UserId = m.UserIdentifier.Id,
                            DebateId = newDebate.Identifier.Id,
                            MemberRole = m.MemberRole,
                            TranslationLink = m.TranslationLink,
                            Ready = m.Ready
                        }, transaction: transaction);
                    }

                    transaction.Commit();
                }

                AddRounds(newDebate, connection);
            }
        }

        public Debate GetDebate(Identifier identifier)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string getDebateQuery =
                    "SELECT debates.id, debates.startdatetime, debates.title, debates.category, " +
                    "debates.roundcount, debates.roundlength, debates.state, members.userid as memberuserid, " +
                    "members.memberrole, members.translationlink, members.ready, rounds.speakerid, " +
                    "rounds.startdatetime as roundstartdatetime, rounds.enddatetime, messages.id as messageid, " +
                    "messages.userid as messageuserid, " +
                    "messages.messagetext, messages.senton, votes.fromuser, votes.touser " +
                    "FROM debates LEFT JOIN members ON debates.id = members.debateid " +
                    "LEFT JOIN rounds ON rounds.debateid = debates.id LEFT JOIN messages " +
                    "ON messages.debateid = debates.id LEFT JOIN votes ON debates.id = votes.debateid " +
                    "WHERE debates.id = @Id";

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var currentDebateInfo = connection.Query<dynamic>(getDebateQuery,
                        new {Id = identifier.Id},
                        transaction: transaction);

                    return currentDebateInfo.FromDynamicToDebate();
                }
            }
        }

        public IEnumerable<Debate> GetDebatesForUser(Identifier identifier)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string getDebateQuery =
                    "SELECT debates.id, debates.startdatetime, debates.title, debates.category, " +
                    "debates.roundcount, debates.roundlength, debates.state, members.userid as memberuserid, " +
                    "members.memberrole, members.translationlink, members.ready, rounds.speakerid, " +
                    "rounds.startdatetime as roundstartdatetime, rounds.enddatetime, messages.id as messageid, " +
                    "messages.userid as messageuserid, " +
                    "messages.messagetext, messages.senton, votes.fromuser, votes.touser " +
                    "FROM debates LEFT JOIN members ON debates.id = members.debateid " +
                    "LEFT JOIN rounds ON rounds.debateid = debates.id LEFT JOIN messages " +
                    "ON messages.debateid = debates.id LEFT JOIN votes ON debates.id = votes.debateid " +
                    "WHERE members.id = @Id";

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var currentDebateInfo = connection.Query<dynamic>(getDebateQuery,
                        new { Id = identifier.Id },
                        transaction: transaction);


                    return currentDebateInfo.FromDynamicToDebateIEnumerable();
                }
            }
        }

        public IEnumerable<Debate> GetActualDebates()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string getDebateQuery =
                    "SELECT debates.id, debates.startdatetime, debates.title, debates.category, " +
                    "debates.roundcount, debates.roundlength, debates.state, members.userid as memberuserid, " +
                    "members.memberrole, members.translationlink, members.ready, rounds.speakerid, " +
                    "rounds.startdatetime as roundstartdatetime, rounds.enddatetime, messages.id as messageid, " +
                    "messages.userid as messageuserid, " +
                    "messages.messagetext, messages.senton, votes.fromuser, votes.touser " +
                    "FROM debates LEFT JOIN members ON debates.id = members.debateid " +
                    "LEFT JOIN rounds ON rounds.debateid = debates.id LEFT JOIN messages " +
                    "ON messages.debateid = debates.id LEFT JOIN votes ON debates.id = votes.debateid " +
                    "WHERE debates.startdatetime > @startDateTime AND " +
                    "debates.enddatetime < @endDateTime";

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var currentDebateInfo = connection.Query<dynamic>(getDebateQuery,
                        new
                        {
                            startDateTime = DateTimeOffset.UtcNow,
                            endDateTime = DateTimeOffset.UtcNow
                        },
                        transaction: transaction);

                    return currentDebateInfo.FromDynamicToDebateIEnumerable();
                }
            }
        }

        public IEnumerable<Debate> SearchDebate(string debateTitle, string category, bool isLive, int pageSize, int pageNumber)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                debateTitle = debateTitle.Replace("[", "[[]").Replace("%", "[%]");
                var titleForLike = $"%{debateTitle}%";

                const string getDebateQuery =
                    "WITH parent AS(SELECT * from debates LIMIT @PageSize OFFSET @Offset)" +
                    "SELECT debates.id, debates.startdatetime, debates.title, debates.category, " +
                    "debates.roundcount, debates.roundlength, debates.state, members.userid as memberuserid, " +
                    "members.memberrole, members.translationlink, members.ready, rounds.speakerid, " +
                    "rounds.startdatetime as roundstartdatetime, rounds.enddatetime, messages.id as messageid, " +
                    "messages.userid as messageuserid, " +
                    "messages.messagetext, messages.senton, votes.fromuser, votes.touser " +
                    "FROM parent as debates LEFT JOIN members ON debates.id = members.debateid " +
                    "LEFT JOIN rounds ON rounds.debateid = debates.id LEFT JOIN messages " +
                    "ON messages.debateid = debates.id LEFT JOIN votes ON debates.id = votes.debateid " +
                    "WHERE debates.title LIKE @title AND debates.category = @category";

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var currentDebateInfo = connection.Query<dynamic>(getDebateQuery,
                        new
                        {
                            title = titleForLike,
                            category = category,
                            PageSize = pageSize,
                            Offset = pageSize * (pageNumber - 1)

                        },
                        transaction: transaction);

                    return currentDebateInfo.FromDynamicToDebateIEnumerable();
                }
            }
        }

        public void SendMessage(Message newMessage) 
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {

                const string sendMesageQuery = "INSERT INTO messages(id, userid, " +
                                               "debateid, messagetext, senton) VALUES(@id, @userId, " +
                                               "@debateId, @messageText, @sentOn)";

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(sendMesageQuery, new
                    {
                        id = newMessage.Id.Id,
                        userId = newMessage.UserId.Id,
                        debateId = newMessage.DebateId.Id,
                        messageText = newMessage.Text,
                        sentOn = newMessage.SentOn
                    });

                    transaction.Commit();
                }
            }
        }

        public void UpdateDebateStatus(Debate debate)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                _updateDebateStatus(debate, connection);
            }
        }

        public void SetReadyStatus(Debate debate, Identifier memberId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string updateMemberStatusQuery = "UPDATE members SET ready = true " +
                                                       "WHERE debateid = @debateId AND " +
                                                       "userid = @userId";

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(updateMemberStatusQuery, new
                    {
                        debateId = debate.Identifier.Id,
                        userId = memberId.Id
                    });

                    transaction.Commit();
                }

                _updateDebateStatus(debate, connection);

                UpdateRounds(debate, connection);
            }
        }

        private void UpdateRounds(Debate debate, IDbConnection connection)
        {
            const string deleteRounds = "DELETE FROM rounds WHERE debateid = @debateId";

            using (var transaction = connection.BeginTransaction())
            {
                connection.Execute(deleteRounds, new
                {
                    debateId = debate.Identifier.Id
                });

                transaction.Commit();
            }

            AddRounds(debate, connection);
        }

        private void AddRounds(Debate debate, IDbConnection connection)
        {
            const string createDebateRoundsQuery =
            "INSERT INTO rounds (debateid, speakerid, startdatetime, enddatetime) " +
            "VALUES (@DebateId, @SpeakerId, @StartDateTime, @EndDateTime)";

            using (var transaction = connection.BeginTransaction())
            {
                foreach (var m in debate.Rounds)
                {
                    connection.Execute(createDebateRoundsQuery, new
                    {
                        DebateId = debate.Identifier.Id,
                        SpeakerId = m.SpeakerId.Id,
                        StartDateTime = m.StartRoundDatetime,
                        EndDateTime = m.EndRoundDateTime
                    }, transaction: transaction);
                }

                transaction.Commit();
            }
        }

        private void _updateDebateStatus(Debate debate, IDbConnection connection)
        {
            const string updateDebateStatusQuery = "UPDATE debates SET state = @state " +
                                                   "WHERE id = @debateId";

            using (var transaction = connection.BeginTransaction())
            {
                connection.Execute(updateDebateStatusQuery, new
                {
                    debateId = debate.Identifier.Id,
                    state = debate.State
                });

                transaction.Commit();
            }
        }

        public InPostgreSqlDebateRepository(string connectionString)
        {
            _connectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString);
        }

        private readonly string _connectionString;
    }
}
