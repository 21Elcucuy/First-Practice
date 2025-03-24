using Microsoft.AspNetCore.Mvc;
using PracticeUI.Models.DTO;

namespace PracticeUI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
                var response = new List<EmployeeDTO>(); 
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponse = await client.GetAsync("https://localhost:7175/api/Employees");
                httpResponse.EnsureSuccessStatusCode();

                response.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<EmployeeDTO>>());
            }
            catch (Exception ex)
            {

               
            }
             

            return View(response);

        }
    }
}
