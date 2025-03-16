using First_Practice.Models.Domain;
using First_Practice.Models.DTO;

namespace First_Practice.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null, string? SortBy = null , bool isAscending = true, 
            int PageNumber = 1,int PageSize =1000);
         Task <Employee> GetAtAsync(int id);
        Task<Employee> CreateAsync(Employee employee);

        Task<Employee> UpdateAsync(int Id,Employee employee);
        Task<Employee> DeleteAsync(int Id);
        Task<Department> GetDepartmentAsync(int id);

    }
}
