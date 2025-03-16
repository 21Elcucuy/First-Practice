using AutoMapper;
using First_Practice.Models.Domain;
using First_Practice.Models.DTO;

namespace First_Practice.AutoMap
{
    public class AutoMappingProfile :Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee,AddEmployeeDTO>().ReverseMap();
            CreateMap<Employee , UpdateEmployeeDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
        }


    }
}
