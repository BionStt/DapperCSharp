using System;

namespace DapperCSharp.Domain.Entities
{
    public class Category
    {
        public Category(Guid id,
                      string description)
        {
            if (id == Guid.Empty)
                Id = Guid.NewGuid();
            else
                Id = id;

            Description = description;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
    }
}