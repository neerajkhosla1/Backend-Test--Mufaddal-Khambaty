using BookManagement.Context;
using BookManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookManagement.Controllers
{
    public class BookController : ApiController
    {
        #region Private Variable (Creating Instance of DatabaseContext class)
        private DatabaseContext db = new DatabaseContext();
        #endregion

        #region Get all books ("/books")
        [HttpGet]
        [Route("books")]
        public IHttpActionResult Get()
        {
            try
            {
                //Prepare data to be returned using Linq as follows  
                var result = from book in db.Books
                             where book.IsDeleted == false
                             select new
                             {
                                 id = book.Id,
                                 name = book.Name,
                                 numberOfPages = book.NumberOfPages,
                                 dateOfPublication = book.DateOfPublication,
                                 authors = from author in book.Authors
                                           select new { author.Name }
                             };
                return Ok(content: result);
            }
            catch (Exception ex)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }
        #endregion
      
        #region Get book by Guid ("/book/{id}")
        [HttpGet]
        [Route("book/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                //Parse String to GUID
                Guid newGuid;
                if (Guid.TryParse(id, out newGuid))
                {
                    newGuid = Guid.Parse(id);
                    var result = (from book in db.Books
                                  where book.Id == newGuid && book.IsDeleted == false
                                  select new
                                  {
                                      id = book.Id,
                                      name = book.Name,
                                      numberOfPages = book.NumberOfPages,
                                      dateOfPublication = book.DateOfPublication,
                                      authors = from author in book.Authors
                                                select new { author.Name }
                                  }).FirstOrDefault();
                    if (result != null)
                        return Ok(content: result);
                    else
                        return NotFound();

                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        #endregion
      
        #region Delete Book by Guid ("book/{id}")
        [HttpDelete]
        [Route("book/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                //Parse String to GUID
                Guid newGuid;
                if (Guid.TryParse(id, out newGuid))
                {
                    newGuid = Guid.Parse(id);
                    var result = (from book in db.Books
                                  where book.Id == newGuid && book.IsDeleted == false
                                  select book
                                 ).FirstOrDefault();
                    if (result != null)
                    {
                        result.IsDeleted = true;
                        db.SaveChanges();
                        return Ok(content: "Book " + id + " deleted successfully.");
                    }

                    else
                        return NotFound();

                }
                return BadRequest();
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }
        #endregion
     
        #region Create new book ("book/{id}")
        [HttpPost]
        [Route("book/{id}")]
        public IHttpActionResult Post(string id, Book book)
        {
            try
            {

                if (book != null)
                {
                    Guid newGuid;
                    if (Guid.TryParse(id, out newGuid))
                    {
                        newGuid = Guid.Parse(id);
                        var result = (from books in db.Books
                                      where books.Id == newGuid && books.IsDeleted == false
                                      select books
                                     ).FirstOrDefault();
                        if (result == null)
                        {
                            var newBook = new Book
                            {
                                CreateDate = DateTime.UtcNow.Ticks,
                                IsDeleted = false,
                                DateOfPublication = book.DateOfPublication,
                                NumberOfPages = book.NumberOfPages,
                                Id = newGuid,
                                UpdateDate = DateTime.UtcNow.Ticks,
                                Name = book.Name,
                                Authors = new List<Author>()
                            };
                            newBook.Authors = book.Authors;
                            db.Books.Add(newBook);
                            db.SaveChanges();
                            return Ok(content: "Book " + id + " created successfully.");

                        }
                        else
                            return Content(HttpStatusCode.Conflict, id);

                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }
        #endregion

        #region Partial Update ("book/{id}")
        [HttpPatch]
        [Route("book/{id}")]
        public IHttpActionResult PatchBook(string id, [FromBody] BookPatchRequest request)
        {
            try
            {
                Guid newGuid;
                if (Guid.TryParse(id, out newGuid))
                {
                    var result = (from books in db.Books
                                  where books.Id == newGuid && books.IsDeleted == false
                                  select books
                                          ).FirstOrDefault();
                    if (result == null)
                        return NotFound();
                    else
                    {
                        result.Name = request.Name;
                        result.NumberOfPages = request.NumberOfPages;
                        db.SaveChanges();
                    }
                    return Ok(content: "Book " + id + " updated successfully.");
                }

                return BadRequest();
            }
            catch (Exception)
            {
                //If any exception occurs Internal Server Error i.e. Status Code 500 will be returned  
                return InternalServerError();
            }
        }
        #endregion

    }
}
