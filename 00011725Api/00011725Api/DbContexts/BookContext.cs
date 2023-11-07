using _00011725Api.Model;
using Microsoft.EntityFrameworkCore;
using Type = _00011725Api.Model.Type;

namespace _00011725Api.DbContexts
{
    public class BookContext:DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) 
        { 

        }
        public DbSet<Book> Books { get; set; }
       
    }
}
