using System.Threading.Tasks;
using DesafioRestApi.Model;
using DesafioRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DesafioRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookCollection _database;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookCollection database, ILogger<BookController> logger)
        {
            _database = database;
            _logger = logger;
        }

        /// <summary>Obter todos os livros cadastrados</summary>
        /// <response code="200">Todos os livros foram retornados com sucesso!</response>>
        /// <response code="500">Ocorreu um erro!</response>>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                _logger.LogInformation("Request info GetAllBooks");
                return Ok(await _database.GetAllBooks());
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return new StatusCodeResult(500);
            }      
        }

        ///<summary>Obter um livro cadastrado por ID</summary>
        /// <response code="200">Livro achado!</response>>
        /// <response code="500">Ocorreu um erro!</response>>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookDetails(string id)
        {
            try
            {
                _logger.LogInformation("Request info GetBookDetails");
                return Ok(await _database.GetBookById(id));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return new StatusCodeResult(500);
            }

        }

        ///<summary>Adicionar um livro</summary>
        /// <response code="201">Livro criado!</response>>
        /// <response code="500">Ocorreu um erro!</response>>
        /// <response code="400">Ocorreu um erro no corpo da requisição! Verifique os campos.</response>>
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            try
            {
                _logger.LogInformation("Request info CreateBook");

                if (book == null) { return BadRequest(); }

                if (book.Autor == string.Empty)
                {
                    ModelState.AddModelError("Nome", "O nome do livro não pode estar vazio!");
                }

                await _database.InsertBook(book);

                return Created("Criado", true);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return new StatusCodeResult(500);
            }
        }

        ///<summary>Atualizar um livro</summary>
        /// <response code="201">Livro atualizado com sucesso!</response>>
        /// <response code="400">Erro ao atualizar um livro!</response>>
        /// <response code="500">Ocorreu um erro!</response>> 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book, string id)
        {
            try
            {
                _logger.LogInformation("Request info UpdateBook");
                if (book == null) { return BadRequest(); }

                if (book.Autor == string.Empty)
                {
                    ModelState.AddModelError("Nome", "O nome do livro não pode estar vazio!");
                }

                book.Id = id;

                await _database.UpdateBook(book);

                return Created("Criado", true);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return new StatusCodeResult(500);
            }
        }

        ///<summary>Deletar um livro</summary>
        /// <response code="204">Livro deletado com sucesso!</response>>
        /// <response code="500">Ocorreu um erro!</response>>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                _logger.LogInformation("Request info DeleteBook");
                await _database.DeleteBook(id);

                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
