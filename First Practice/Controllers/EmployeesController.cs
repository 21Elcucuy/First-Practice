using System.Reflection.Metadata.Ecma335;
using First_Practice.Data;
using First_Practice.Models.DTO;
using First_Practice.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using First_Practice.Repository;
using First_Practice.AutoMap;
using AutoMapper;
using First_Practice.CustomActionFilter;
using Microsoft.AspNetCore.Authorization;

namespace First_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DbContexts _dbContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper mapper;
        public EmployeesController(DbContexts dbContexts , IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _dbContext = dbContexts;
            this._employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        // /api/
        [HttpGet]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll([FromQuery] string? FilterOn , [FromQuery] string? FilterQuery,
            [FromQuery] string ?SortBy , [FromQuery] bool? isAscending ,
            [FromQuery] int PageNumber = 1 , int PageSize = 1000)
        {
            List<Employee> DbEmployeesDomain = await _employeeRepository.GetAllAsync(FilterOn, FilterQuery,SortBy,isAscending ?? true ,PageNumber,PageSize);

            //var employeeDTO = mapper.Map<List<EmployeeDTO>>(DbEmployeesDomain);

            List<EmployeeDTO> employeeDTO = new List<EmployeeDTO>();
            foreach(Employee DbEmployeeDomain in DbEmployeesDomain )
            {
                EmployeeDTO employee = new EmployeeDTO
                {
                    Id = DbEmployeeDomain.Id,
                    Name = DbEmployeeDomain.Name,
                    Email = DbEmployeeDomain.Email,
                    Phone = DbEmployeeDomain.Phone,
                    Department = await _employeeRepository.GetDepartmentAsync(DbEmployeeDomain.DepartmentId)
                };
                employeeDTO.Add(employee);
            }
  
                return Ok(employeeDTO);
        }
        [HttpGet]
        [Route("{Id}")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAt([FromRoute]int Id)
        {
            var DbEmployeeDomain = await _employeeRepository.GetAtAsync(Id);
            if (DbEmployeeDomain == null)
            {
                return NotFound();
            }
            //EmployeeDTO employeeDTO = mapper.Map<EmployeeDTO>(DbEmployeeDomain);
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = DbEmployeeDomain.Id,
                Name = DbEmployeeDomain.Name,
                Email = DbEmployeeDomain.Email,
                Phone = DbEmployeeDomain.Phone,
                Department = await _employeeRepository.GetDepartmentAsync(DbEmployeeDomain.DepartmentId)

            };
            return Ok(employeeDTO);

        }
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody]AddEmployeeDTO addemployeeDTO)
        {
            //Employee employeeModelDomain = mapper.Map<Employee>(addemployeeDTO);
            
            Employee employeeModelDomain = new Employee()
            {
                Name = addemployeeDTO.Name,
                DepartmentId = addemployeeDTO.DepartmentId,
                Email = addemployeeDTO.Email,
                Phone = addemployeeDTO.Phone,

            };

           await _employeeRepository.CreateAsync(employeeModelDomain);


            //EmployeeDTO employeeDTO = mapper.Map<EmployeeDTO>(employeeModelDomain);

            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = employeeModelDomain.Id,
                Name = employeeModelDomain.Name,
                Email = employeeModelDomain.Email,
                Phone = employeeModelDomain.Phone,
                Department = await _employeeRepository.GetDepartmentAsync(employeeModelDomain.DepartmentId)

            };
         
            return CreatedAtAction(nameof(GetAt) ,  new { Id = employeeDTO.Id }  , employeeDTO );
            
         
        }
        [HttpPut]
        [Route("{Id}")]
        [ValidateModel]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateEmployeeDTO updateEmployeeDTO)
        {
            //var employeeModelDomain = mapper.Map<Employee>(updateEmployeeDTO);
            Employee employeeModelDomain = new Employee()
            {
                Name = updateEmployeeDTO.Name, 
                DepartmentId = updateEmployeeDTO.DepartmentId,
                Email = updateEmployeeDTO.Email,
                Phone = updateEmployeeDTO.Phone,

            };
            var newemployeeModelDomain =await _employeeRepository.UpdateAsync(Id,employeeModelDomain);

            if (newemployeeModelDomain == null)
            {
                return NotFound();
            }

            //EmployeeDTO employeeDTO = mapper.Map<EmployeeDTO>(newemployeeModelDomain);
            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = newemployeeModelDomain.Id,
                Name = newemployeeModelDomain.Name,
                Email = newemployeeModelDomain.Email,
                Phone = newemployeeModelDomain.Phone,
                Department = await _employeeRepository.GetDepartmentAsync(newemployeeModelDomain.DepartmentId)

            };


            return Ok(employeeDTO);
        }
        [HttpDelete]
        [Route("{Id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
             var employeeModelDomain = await _employeeRepository.DeleteAsync(Id);
             if(employeeModelDomain == null)
            {
                return NotFound();
            }

            EmployeeDTO employeeDTO = new EmployeeDTO()
            {
                Id = employeeModelDomain.Id,
                Name = employeeModelDomain.Name,
                Email = employeeModelDomain.Email,
                Phone = employeeModelDomain.Phone,
                Department = await _employeeRepository.GetDepartmentAsync(employeeModelDomain.DepartmentId)

            };

            return Ok(employeeDTO);

        }

    }
}