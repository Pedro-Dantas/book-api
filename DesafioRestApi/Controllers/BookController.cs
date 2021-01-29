using System.Threading.Tasks;
using DesafioRestApi.Model;
using DesafioRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DesafioRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookCollection _database;

        public BookController(IBookCollection database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _database.GetAllBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookDetails(string id)
        {
            return Ok(await _database.GetBookById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if(book == null) { return BadRequest(); }

            if(book.Autor == string.Empty)
            {
                ModelState.AddModelError("Nome", "O nome do livro não pode estar vazio!");
            }

            await _database.InsertBook(book);

            return Created("Criado", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book, string id)
        {
            if (book == null) { return BadRequest(); }

            if (book.Autor == string.Empty)
            {
                ModelState.AddModelError("Nome", "O nome do livro não pode estar vazio!");
            }

            book.Id = id;

            await _database.UpdateBook(book);

            return Created("Criado", true);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            await _database.DeleteBook(id);

            return NoContent();
        }
    }
}
