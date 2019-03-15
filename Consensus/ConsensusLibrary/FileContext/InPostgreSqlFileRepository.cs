using System.Data;
using ConsensusLibrary.FileContext.Exceptions;
using Dapper;
using EnsureThat;
using Npgsql;

namespace ConsensusLibrary.FileContext
{
    public class InPostgreSqlFileRepository : IFileRepository
    {
        public void AddFile(File newFile)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "INSERT INTO files (filename, physicalfilename, filetype) " +
                                     "VALUES (@FileName, @PhysicalFileName, @FileType)";

                connection.Execute(query, new
                {
                    FileName = newFile.FileName,
                    PhysicalFileName = newFile.PhysicalFileName,
                    FileType = newFile.FileType
                });
            }
        }

        public File GetFile(string fileName)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "SELECT filename, physicalfilename, filetype FROM files " +
                                     "WHERE filename = @FileName";

                var currentFile = connection.QueryFirstOrDefault(query, new {FileName = fileName});

                //TODO crap code
                if(currentFile == null)
                    throw new FileNotFoundException();

                return new File(currentFile.filename, currentFile.physicalfilename, 
                    currentFile.filetype);
            }
        }

        public InPostgreSqlFileRepository(string connectionString)
        {
            _connectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString);
        }

        private readonly string _connectionString;
    }
}
