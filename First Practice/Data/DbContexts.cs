using First_Practice.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace First_Practice.Data
{
    public class DbContexts : DbContext
    {
        public DbContexts(DbContextOptions option ) : base(option) 
        {
        }
        public DbSet <Employee> Employee { get; set;}
    }
}
