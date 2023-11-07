using _00011725Api.Model;

namespace _00011725Api.Repository
{
    public interface IBookRepository
    {
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
        Book GetBookById (int Id);
        IEnumerable<Book> GetAllBooks();


    }
}
