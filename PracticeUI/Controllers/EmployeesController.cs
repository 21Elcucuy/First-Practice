using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PracticeUI.Models;
using PracticeUI.Models;
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
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployee employee)
        {
            var client = httpClientFactory.CreateClient();
         
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7175/api/employees"),
                Content =  new StringContent(JsonSerializer.Serialize(employee),Encoding.UTF8 , "application/json")
            };

            var responseMessage = await client.SendAsync(request);
            responseMessage.EnsureSuccessStatusCode();
            var response = responseMessage.Content.ReadFromJsonAsync<EmployeeDTO>();
            if(response != null)
            {
               return RedirectToAction("Index", "Employees");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id )
        {
            var client = httpClientFactory.CreateClient();

            var responseMessage = await client.GetFromJsonAsync<UpdateEmployee>($"https://localhost:7175/api/Employees/{id}");
            if (responseMessage != null)
            {
                return View(responseMessage);
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployee employee)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7175/api/employees/{employee.Id}"),
                    Content = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json")
                };
                var responseMessage = await client.SendAsync(request);
                responseMessage.EnsureSuccessStatusCode();
                var response = request.Content.ReadFromJsonAsync<EmployeeDTO>();
                if (response != null)
                {
                    return RedirectToAction("Index", "Employees");
                }
                }
            catch (Exception ex)
            {

                
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployee employee)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"https://localhost:7175/api/employees/{employee.Id}"),
                    Content = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json")
                };
                var responseMessage = await client.SendAsync(request);
                responseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Employees");

            }
            catch (Exception ex)
            {

               
            }
            return View("Edit");

        }
    }
  }
