using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace First_Practice.Models.Domain
{
    public class ApplicationUser :IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
