using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookServices;

        public BooksController(BookService bookService)
        {
            _bookServices = bookService;
        }

        // GET: /<controller>/
        [HttpGet]
        public ActionResult<List<Book>> Get() => _bookServices.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookServices.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// Create a Book Item.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///     POST /api/v1/books/
        ///     {
        ///         "Name": "Lorem Name",
        ///         "Price": 12.43,
        ///         "Category": "Lorem category",
        ///         "Author": "Tuan Luu"
        ///     }
        /// </remarks>
        /// <param name="book"></param>
        /// <returns>A new book item created.</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Book> Create(Book book)
        {
            _bookServices.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookServices.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookServices.Update(id, bookIn);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific BookStore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookServices.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookServices.Remove(book.Id);

            return NoContent();
        }

    }
}
