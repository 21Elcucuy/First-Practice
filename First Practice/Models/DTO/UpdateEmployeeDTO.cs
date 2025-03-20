using System.ComponentModel.DataAnnotations;

namespace First_Practice.Models.DTO
{
    public class UpdateEmployeeDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [Range(1,100)]
        public int DepartmentId { get; set; }
    }
}
