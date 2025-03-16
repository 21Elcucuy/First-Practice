using First_Practice.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace First_Practice.Data
{
    public class DbContexts : DbContext
    {
        public DbContexts(DbContextOptions option) : base(option)
        {
        }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Department> Department { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<Department> departments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "IT"
                },
                new Department()
                {
                    Id = 2,
                    Name = "Marketing"
                },
                new Department()
                {
                    Id = 3,
                    Name = "HR"
                },
                new Department()
                {
                    Id = 4,
                    Name = "Sales"
                }


            };
            modelBuilder.Entity<Department>().HasData(departments);


        }
    }
}