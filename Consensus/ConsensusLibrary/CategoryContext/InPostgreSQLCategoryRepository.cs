using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ConsensusLibrary.CategoryContext.Dtos;
using ConsensusLibrary.CategoryContext.Exceptions;
using Dapper;
using EnsureThat;
using Npgsql;

namespace ConsensusLibrary.CategoryContext
{
    public class InPostgreSqlCategoryRepository : ICategoryRepository
    {
        public InPostgreSqlCategoryRepository(string connectionString)
        {
            _connectionString = Ensure.String.IsNotNullOrWhiteSpace(connectionString);
        }

        public void AddCategory(Category category)
        {
            Ensure.Any.IsNotNull(category);

            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var dto = category.FromEntityToDto();
                var sqlQuery = "INSERT INTO categories (title) VALUES (@Title)";
                db.Execute(sqlQuery, dto);
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var resultDto = db.Query<CategoryDto>("SELECT * FROM categories").ToList();

                var result = new List<Category>();

                resultDto.ForEach(c => result.Add(new Category(c.Title)));

                return result;
            }
        }

        public bool DoesCategoryExist(string category)
        {
            Ensure.String.IsNotNullOrWhiteSpace(category);

            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var resultDto = db.QueryFirstOrDefault<CategoryDto>("SELECT * FROM categories WHERE title = @Title",
                    new { Title = category });

                return resultDto != null;
            }
        }

        public Category GetCategoryByTitle(string title)
        {
            Ensure.String.IsNotNullOrWhiteSpace(title);

            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var resultDto = db.QueryFirstOrDefault<CategoryDto>("SELECT * FROM categories WHERE title = @Title",
                    new { Title = title });

                Ensure.Any.IsNotNull(resultDto, nameof(resultDto),
                    opt => opt.WithException(new CategoryNotFoundException()));

                return resultDto.FromDtoToEntity();
            }
        }

        private readonly string _connectionString;
    }
}
