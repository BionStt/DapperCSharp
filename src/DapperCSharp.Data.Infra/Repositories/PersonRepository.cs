using DapperCSharp.Domain.Entities;
using DapperCSharp.Domain.ValueObjects;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DapperCSharp.Infra.Data.Repositories
{
    public class PersonRepository
    {
        private readonly SqlConnection _connection;

        public PersonRepository()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperCSharpDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _connection.Open();
        }

        public bool Add(Person entity)
        {
            var parameters = new { id = entity.Id,
                                   name = entity.Name,
                                   email = entity.Email.Address,
                                   categoryId = entity.CategoryId };
            var sql = "INSERT INTO " +
                        "Person (Id, Name, Email, CategoryId) " + 
                        "VALUES (@id, @name, @email, @CategoryId);";
            return _connection.Execute(sql,
                                       parameters) > 0;
        }

        public bool Update(Person entity)
        {
            var parameters = new { id = entity.Id,
                                   name = entity.Name,
                                   email = entity.Email.Address,
                                   categoryId = entity.CategoryId };
            var sql = "UPDATE Person " +
                        "SET Name = @name, Email = @email, CategoryId = @categoryId " + 
                        "WHERE Id = @id;";
            return _connection.Execute(sql,
                                       parameters) > 0;
        }

        public bool Delete(Person entity)
        {
            var parameters = new { id = entity.Id };
            return _connection.Execute("Delete FROM Person WHERE Id = @id;",
                                        parameters) > 0;
        }
    }
}