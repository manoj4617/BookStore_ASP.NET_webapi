using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookrepo;
        public BooksController(IBookRepository bookrepo)
        {
            _bookrepo = bookrepo;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookrepo.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookrepo.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook([FromBody] BooksModel bookmodel)
        {
            var id = await _bookrepo.AddBookAsync(bookmodel);
            return CreatedAtAction(nameof(GetBookById), new { id = id , controller = "books"} , id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id,[FromBody] BooksModel toUpdateBook)
        {
            await _bookrepo.UpdateBookAsync(id, toUpdateBook);
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument bookmodel, [FromRoute] int id)
        {
            await _bookrepo.UpdateBookPatchAsync(id, bookmodel);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookrepo.DeleteBookAsync(id);
            return Ok();
        }

    }
}
