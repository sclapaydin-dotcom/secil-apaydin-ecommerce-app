using KB.BookStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KB.BookStore.WebApi.Models
{
    public class ecommercerDbContext : DbContext
    {
        public ecommercerDbContext(DbContextOptions<ecommercerDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
