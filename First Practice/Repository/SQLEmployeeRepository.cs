using First_Practice.Data;
using First_Practice.Models.Domain;
using First_Practice.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace First_Practice.Repository
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly DbContexts dbContext;
        public SQLEmployeeRepository(DbContexts dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {

            await dbContext.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return employee;


        }

        public async Task<Employee> DeleteAsync(int Id)
        {
           var employeeDomain = dbContext.Employee.FirstOrDefault(x => x.Id == Id);
            if (employeeDomain == null)
            {
                return null;
            }
            dbContext.Employee.Remove(employeeDomain);
            await dbContext.SaveChangesAsync();
            
            return employeeDomain;



        }

        public async Task<List<Employee>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null ,string? SortBy = null, bool isAscending = true,
            int PageNumber = 1, int PageSize = 1000)
        {
             
           var employee = dbContext.Employee.Include("Department").AsQueryable();
            // Filter
            if (string.IsNullOrEmpty(FilterOn) ==false && string.IsNullOrEmpty(FilterQuery) == false)
            {
                if (FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                 employee = employee.Where(x  => x.Name.Contains(FilterQuery));
                }
                if (FilterOn.Equals("DepartmentName", StringComparison.OrdinalIgnoreCase))
                {
                  employee = employee.Where(x => x.Department.Name.Contains(FilterQuery));
                }
            }
            //Sorting 
            if (string.IsNullOrEmpty(SortBy) == false)
            {
                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    employee = isAscending ? employee.OrderBy(x => x.Name) : employee.OrderByDescending(x => x.Name);
                }
            }
            int SkipPages = (PageNumber -1 ) * PageSize;

            return await employee.Skip(SkipPages).Take(PageSize).ToListAsync();
        }

        public async Task<Employee> GetAtAsync(int id)
        {
           return await dbContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
           var department  = await dbContext.Department.FirstOrDefaultAsync(x => x.Id == id);
            return department;
        }

        public async Task<Employee> UpdateAsync(int Id,Employee employee)
        {
            var EmployeeDomain = dbContext.Employee.FirstOrDefault(x => x.Id ==Id );
            if (EmployeeDomain == null)
            {
                return null;
            }
            EmployeeDomain.Email = employee.Email;
            EmployeeDomain.Phone = employee.Phone;
            EmployeeDomain.DepartmentId = employee.DepartmentId;
            await dbContext.SaveChangesAsync();
            return EmployeeDomain;

        }

      
    }
}
