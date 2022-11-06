using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
    }
}