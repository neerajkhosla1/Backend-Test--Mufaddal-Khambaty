using BookManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookManagement.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            var authors = new List<Author> { new Author { AuthorId = Guid.NewGuid(),  Name = "John" }, new Author { AuthorId = Guid.NewGuid(), Name = "Rex" } };

            Book book = new Book
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow.Ticks,
                NumberOfPages=3,
                DateOfPublication=DateTime.UtcNow.Ticks,
                UpdateDate= DateTime.UtcNow.Ticks,
                Name = "Times",
                Authors = authors,
                IsDeleted=false
            };
            context.Books.Add(book);


            var authors2 = new List<Author> { new Author { AuthorId = Guid.NewGuid(), Name = "Michel" }, new Author { AuthorId = Guid.NewGuid(), Name = "Amay" } };

            Book book2 = new Book
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow.Ticks,
                NumberOfPages = 22,
                DateOfPublication = DateTime.UtcNow.Ticks,
                UpdateDate = DateTime.UtcNow.Ticks,
                Name = "God Father",
                Authors = authors2,
                IsDeleted = false
            };
            context.Books.Add(book2);
            context.SaveChanges();
        }
    }
}