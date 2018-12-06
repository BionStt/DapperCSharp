using DapperCSharp.Domain.Entities;
using DapperCSharp.Domain.ValueObjects;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DapperCSharp.Infra.Data.Queries
{
    public class PersonQuery
    {
        private readonly SqlConnection _connection;

        public PersonQuery()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperCSharpDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _connection.Open();
        }

        public int Count()
        {
            return _connection.ExecuteScalar<int>("SELECT count(*) FROM Person;");
        }

        public List<Person> GetAll()
        {
            return  _connection.Query<Person, Email, Person>("SELECT Id, Name, Email FROM Person;",
                                                             (person, email) =>
                                                             {
                                                                 person.Email = email;
                                                                 return person;
                                                             },
                                                             splitOn: "Email")
                               .ToList();
        }

        public Person GetById(Guid id)
        {
            var parameters = new { id };
            return  _connection.Query<Person, Email, Person>("SELECT Id, Name, Email FROM Person;",
                                                             (person, email) =>
                                                             {
                                                                 person.Email = email;
                                                                 return person;
                                                             },
                                                             splitOn: "Email",
                                                             param: parameters)
                               .First();
        }
    }
}