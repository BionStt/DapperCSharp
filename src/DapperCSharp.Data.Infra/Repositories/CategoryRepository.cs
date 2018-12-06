using DapperCSharp.Domain.Entities;
using DapperCSharp.Domain.ValueObjects;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DapperCSharp.Infra.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperCSharpDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _connection.Open();
        }

        public bool Add(Category entity)
        {
            var parameters = new { id = entity.Id,
                                description = entity.Description};
            var sql = "INSERT INTO " +
                        "Category (Id, Description) " + 
                        "VALUES (@id, @description);";
            return _connection.Execute(sql,
                                    parameters) > 0;
        }

        public bool Update(Category entity)
        {
            var parameters = new { id = entity.Id,
                                description = entity.Description};
            var sql = "UPDATE Category " +
                        "SET Description = @description " + 
                        "WHERE Id = @id;";
            return _connection.Execute(sql,
                                    parameters) > 0;
        }

        public bool Delete(Category entity)
        {
            var parameters = new { id = entity.Id };
            return _connection.Execute("Delete FROM Category WHERE Id = @id;",
                                        parameters) > 0;
        }
    }
}