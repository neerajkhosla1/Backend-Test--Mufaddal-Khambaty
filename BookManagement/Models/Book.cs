using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookManagement.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public long DateOfPublication { get; set; }
        public long CreateDate { get; set; }
        public long UpdateDate { get; set; }
        public List<Author> Authors { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        //Adding Foreign Key constraints for book  
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }

    public class BookPatchRequest
    {
        public int NumberOfPages { get; set; }
        public string Name { get; set; }
    }
}