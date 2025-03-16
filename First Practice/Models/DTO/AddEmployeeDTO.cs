using System.ComponentModel.DataAnnotations;

namespace First_Practice.Models.DTO
{
    public class AddEmployeeDTO
    {

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }
       
        public string? Email { get; set; }

        public string? Phone { get; set; }
        [Required]
        [Range(1,100)]
        public int DepartmentId { get; set; }
    }
}
