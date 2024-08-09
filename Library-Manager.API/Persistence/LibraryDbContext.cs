using Microsoft.EntityFrameworkCore;

namespace Library_Manager.API.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base (options)
        {

        }

    }
}
