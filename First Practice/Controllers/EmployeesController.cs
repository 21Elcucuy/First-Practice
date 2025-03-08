using System.Reflection.Metadata.Ecma335;
using First_Practice.Data;
using First_Practice.Models.DTO;
using First_Practice.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DbContexts _dbContext;
        public EmployeesController(DbContexts dbContexts)
        {
            _dbContext = dbContexts;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee> DbEmployeesDomain = _dbContext.Employee.ToList();

            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            foreach (var DbEmployee in DbEmployeesDomain)
            {
                employeeDTO.Add(new EmployeeDTO
                {
                    Id = DbEmployee.Id,
                    Name = DbEmployee.Name,
                    Email = DbEmployee.Email,
                    Phone = DbEmployee.Phone,
                    DepartmentName = DbEmployee.DepartmentName,
                });

            }
            return Ok(employeeDTO);
        }
        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetAt([FromRoute]int Id)
        {
            var DbEmployeeDomain = _dbContext.Employee.FirstOrDefault(x => x.Id == Id);
            if (DbEmployeeDomain == null)
            {
                return NotFound();
            }
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = DbEmployeeDomain.Id,
                Name = DbEmployeeDomain.Name,
                Email = DbEmployeeDomain.Email,
                Phone = DbEmployeeDomain.Phone,
                DepartmentName = DbEmployeeDomain.DepartmentName
            };
            return Ok(employeeDTO);

        }
        [HttpPost]
        public IActionResult Create([FromBody]AddEmployeeDTO addemployeeDTO)
        {
            Employee employeeModelDomain = new Employee()
            {
                Name = addemployeeDTO.Name,
                Email = addemployeeDTO.Email,
                Phone = addemployeeDTO.Phone,
                DepartmentName = addemployeeDTO.DepartmentName
            };

            _dbContext.Employee.Add(employeeModelDomain);
            _dbContext.SaveChanges();

            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = employeeModelDomain.Id,
                Name = employeeModelDomain.Name,
                Email = employeeModelDomain.Email,
                Phone = employeeModelDomain.Phone,
                DepartmentName = employeeModelDomain.DepartmentName
            };

            return CreatedAtAction(nameof(GetAt) ,  new { Id = employeeDTO.Id }  , employeeDTO );

        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update([FromRoute] int Id, [FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
           var employeeModelDomain = _dbContext.Employee.FirstOrDefault(x  => x.Id == Id);
            if (employeeModelDomain == null)
            {
                return NotFound();
            }
           employeeModelDomain.Phone = updateEmployeeDTO.Phone;
           employeeModelDomain.Email = updateEmployeeDTO.Email;
           employeeModelDomain.DepartmentName = updateEmployeeDTO.DepartmentName;


            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = employeeModelDomain.Id,
                Name = employeeModelDomain.Name,
                Email = employeeModelDomain.Email,
                Phone = employeeModelDomain.Phone,
                DepartmentName = employeeModelDomain.DepartmentName
            };
            return Ok(employeeDTO);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            var employeeModelDomain = _dbContext.Employee.FirstOrDefault(x=> x.Id ==Id);
             if(employeeModelDomain == null)
            {
                return NotFound();
            }
            _dbContext.Employee.Remove(employeeModelDomain);
            _dbContext.SaveChanges();

            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = employeeModelDomain.Id,
                Name = employeeModelDomain.Name,
                Email = employeeModelDomain.Email,
                Phone = employeeModelDomain.Phone,
                DepartmentName = employeeModelDomain.DepartmentName
            };
            return Ok(employeeDTO);

        }

    }
}