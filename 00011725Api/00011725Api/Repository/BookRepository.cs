using _00011725Api.DbContexts;
using _00011725Api.Model;
using Microsoft.EntityFrameworkCore;

namespace _00011725Api.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _dbContext;
        public BookRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Books.Remove(book);
            Save();
        }

        public Book GetBookById(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Entry(book).Reference(s => s.Type).Load();
            return book;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _dbContext.Books.Include(s => s.Type).ToList();
        }

        public void InsertBook(Book book)
        {
            _dbContext.Add(book);
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void UpdateBook(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            Save();
        }
    }
}
