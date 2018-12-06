using System;
using DapperCSharp.Domain.ValueObjects;

namespace DapperCSharp.Domain.Entities
{
    public class Person
    {
        protected Person(){ }

        public Person(Guid id,
                      string name,
                      Email email,
                      Guid? categoryId = null,
                      Category category = null)
        {
            if (id == Guid.Empty)
                Id = Guid.NewGuid();
            else
                Id = id;

            Name = name;
            Email = email;
            CategoryId = categoryId;
            Category = category;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; set; }
        public Guid? CategoryId { get; private set; }
        public Category Category { get; private set; }
    }
}