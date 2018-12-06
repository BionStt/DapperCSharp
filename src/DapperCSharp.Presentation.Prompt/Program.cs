using System;
using DapperCSharp.Domain.Entities;
using DapperCSharp.Domain.ValueObjects;
using DapperCSharp.Infra.Data.Queries;
using DapperCSharp.Infra.Data.Repositories;

namespace DapperCSharp.Presentation.Prompt
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(":::    PERSON    :::");

            var personQuery = new PersonQuery();
            var personCount = personQuery.Count();
            var personId = Guid.Empty;
            Console.WriteLine($"Count: {personCount}\n");

            foreach (var person in personQuery.GetAll())
            {
                personId = person.Id;
                Console.WriteLine($"{person.Id} | {person.Name} | {person.Email.Address} | {person.Category?.Description ?? "Sem categoria"}");
            }

            var personById = personQuery.GetById(personId);
            Console.WriteLine($"{personById.Id} | {personById.Name} | {personById.Email.Address} | {personById.Category?.Description ?? "Sem categoria"}\n");

            // ADD
            var personRepository = new PersonRepository();
            var personNewId = Guid.NewGuid();
            var newPerson = new Person(personNewId, "Tiago Pariz", new Email("tiago@email.com"));
            if (personRepository.Add(newPerson))
                Console.WriteLine($"Cadastrado com sucesso: {newPerson.Id} | {newPerson.Name}\n");
            
            // UPDATE
            var personDb = personQuery.GetById(personNewId);
            var personToUpdate = new Person(personDb.Id, 
                                            "Test " + DateTime.Now.Millisecond,
                                            personDb.Email,
                                            personDb.CategoryId,
                                            personDb.Category);
            if (personRepository.Update(personToUpdate))
                Console.WriteLine($"Atualizado com sucesso: {personToUpdate.Id} | {personToUpdate.Name}\n");

            // DELETE
            var personToDelete = personQuery.GetById(personId);
            if (personRepository.Delete(personToDelete))
                Console.WriteLine($"Excluído com sucesso: {personToDelete.Id} | {personToDelete.Name}\n");

            Console.WriteLine(":::    CATEGORY    :::");

            var categoryQuery = new CategoryQuery();
            var categoryCount = categoryQuery.Count();
            var categoryId = Guid.Empty;
            Console.WriteLine($"Count: {categoryCount}\n");

            foreach (var category in categoryQuery.GetAll())
            {
                categoryId = category.Id;
                Console.WriteLine($"{category.Id} | {category.Description}");
            }

            var categoryById = categoryQuery.GetById(categoryId);
            Console.WriteLine($"{categoryById.Id} | {categoryById.Description}\n");

            // ADD
            var categoryRepository = new CategoryRepository();
            var categoryNewId = Guid.NewGuid();
            var newCategory = new Category(categoryNewId, "Test category insert " + DateTime.Now.Millisecond);
            if (categoryRepository.Add(newCategory))
                Console.WriteLine($"Cadastrado com sucesso: {newCategory.Id} | {newCategory.Description}\n");
            
            // UPDATE
            var categoryDb = categoryQuery.GetById(categoryNewId);
            var categoryToUpdate = new Category(categoryDb.Id, 
                                                "Test category update " + DateTime.Now.Millisecond);
            if (categoryRepository.Update(categoryToUpdate))
                Console.WriteLine($"Atualizado com sucesso: {categoryToUpdate.Id} | {categoryToUpdate.Description}\n");

            // DELETE
            var categoryToDelete = categoryQuery.GetById(categoryId);
            if (categoryRepository.Delete(categoryToDelete))
                Console.WriteLine($"Excluído com sucesso: {categoryToDelete.Id} | {categoryToDelete.Description}\n");

            Console.Read();
        }
    }
}