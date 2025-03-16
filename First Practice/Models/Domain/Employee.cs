namespace First_Practice.Models.Domain
{
    public class Employee  
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Email {  get; set; }
        public string? Phone { get; set; }
    
        public int DepartmentId { get; set; }


        //Navifation
        public Department Department { get; set; }
    }
}
