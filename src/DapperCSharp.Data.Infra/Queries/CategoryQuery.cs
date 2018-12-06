using DapperCSharp.Domain.Entities;
using DapperCSharp.Domain.ValueObjects;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DapperCSharp.Infra.Data.Queries
{
    public class CategoryQuery
    {
        private readonly SqlConnection _connection;

        public CategoryQuery()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperCSharpDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _connection.Open();
        }

        public int Count()
        {
            return _connection.ExecuteScalar<int>("SELECT count(*) FROM Category;");
        }

        public List<Category> GetAll()
        {
            return  _connection.Query<Category>("SELECT Id, Description FROM Category;")
                               .ToList();
        }

        public Category GetById(Guid id)
        {
            var parameters = new { id };
            return _connection
                    .Query<Category>("SELECT Id, Description FROM Category WHERE Id = @id;",
                                     parameters)
                    .First();
        }
    }
}