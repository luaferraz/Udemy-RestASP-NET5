using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        
        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public DbSet<PersonVO> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
