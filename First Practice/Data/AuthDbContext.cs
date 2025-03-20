using First_Practice.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace First_Practice.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> option ) : base(option) 
        {
        
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var role = new List<IdentityRole>()
            { new  IdentityRole
             {
                 Id = Guid.NewGuid().ToString(),
                 Name = "User",
                 NormalizedName = "User".ToUpper(),
                 ConcurrencyStamp =Guid.NewGuid().ToString()
              },
              new  IdentityRole
             {
                 Id = Guid.NewGuid().ToString(),
                 Name = "Admin",
                 NormalizedName = "Admin".ToUpper(),
                 ConcurrencyStamp =Guid.NewGuid().ToString()
              },
            };
            builder.Entity<IdentityRole>().HasData(role);


        }
    }
}
