using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasMany(a => a.Books).WithOne(b => b.Author);
        modelBuilder.Entity<Book>().HasMany(b => b.BorrowRecords).WithOne(br => br.Book);
        modelBuilder.Entity<Member>().HasMany(m => m.BorrowRecords).WithOne(br => br.Member);
    }
}
