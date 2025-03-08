namespace First_Practice.Models.Domain
{
    public class Employee  
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Email {  get; set; }
        public string? Phone { get; set; }
    
        public string DepartmentName { get; set; }

    }
}
