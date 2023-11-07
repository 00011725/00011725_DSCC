using _00011725Api.Model;
using _00011725Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _00011725Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public  BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookRepository.GetAllBooks();
            return new OkObjectResult(books);
        }

        // GET api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return new OkObjectResult(book);
        }

        // POST api/Book
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            using (var scope = new TransactionScope())
            {
                _bookRepository.InsertBook(book);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new {id = book.Id}, book);
            }
        }

        // PUT api/Book/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            if (book != null)
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepository.UpdateBook(book);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return new OkResult();
        }
    }
}
