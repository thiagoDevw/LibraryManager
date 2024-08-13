using Library_Manager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Manager.API.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>(e =>
                {
                    e.HasKey(b => b.Id);

                    e.Property(b => b.Title)
                        .IsRequired()
                        .HasMaxLength(50);

                    e.HasIndex(b => b.ISBN)
                        .IsUnique();

                    e.Property(b => b.Year)
                        .IsRequired();

                    e.HasOne(b => b.Author)
                        .WithMany(b => b.Books)
                        .HasForeignKey(b => b.AuthorId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<User>(e =>
                {
                    e.HasKey(u => u.Id);

                    e.Property(u => u.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                    e.Property(u => u.Email)
                        .IsRequired()
                        .HasMaxLength(100);

                    e.Property(u => u.Password)
                        .IsRequired()
                        .HasMaxLength(100);

                    e.Property(u => u.Phone)
                        .IsRequired()
                        .HasMaxLength(15);

                    e.HasMany(u => u.Loans)
                        .WithOne(l => l.User)
                        .HasForeignKey(u => u.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder
                .Entity<Loan>(e =>
                {
                    e.HasKey(l => l.Id);

                    e.Property(l => l.LoanDate)
                        .IsRequired();

                    e.Property(l => l.ReturnDate);

                    e.HasOne(l => l.Book)
                        .WithMany(b => b.Loans)
                        .HasForeignKey(l => l.BookId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(l => l.User)
                        .WithMany(u => u.Loans)
                        .HasForeignKey(u => u.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                });



            base.OnModelCreating(builder);
        }
    }
}
