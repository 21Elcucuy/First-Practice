﻿using First_Practice.Models.Domain;

namespace First_Practice.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }

        public Department Department { get; set; }
    }
}
