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

        /// <summary>Obter todos os livros cadastrados</summary>
        /// <response code="200">Todos os livros foram retornados com sucesso!</response>>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _database.GetAllBooks());
        }

        ///<summary>Obter um livro cadastrado por ID</summary>
        /// <response code="200">Livro achado!</response>>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookDetails(string id)
        {
            return Ok(await _database.GetBookById(id));
        }

        ///<summary>Adicionar um livro</summary>
        /// <response code="201">Livro criado!</response>>
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

        ///<summary>Atualizar um livro</summary>
        /// <response code="201">Livro atualizado com sucesso!</response>>
        /// <response code="400">Erro ao atualizar um livro!</response>>
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

        ///<summary>Deletar um livro</summary>
        /// <response code="204">Livro deletado com sucesso!</response>>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            await _database.DeleteBook(id);

            return NoContent();
        }
    }
}
