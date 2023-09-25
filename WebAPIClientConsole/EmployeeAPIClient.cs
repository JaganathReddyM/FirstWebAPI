using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPIClientConsole
{
    internal class EmployeeAPIClient
    {
        static Uri uri = new Uri("http://localhost:5200/");
        public static async Task CallGetAllEmployee()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                List<EmpViewModel> employees = new List<EmpViewModel>();
                client.DefaultRequestHeaders
                    .Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //HttpGet:
                HttpResponseMessage response = await client.GetAsync("GetAllEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    //install Newtonsoft.Json using package installer
                    employees = JsonConvert.DeserializeObject<List<EmpViewModel>>(json);
                    foreach(EmpViewModel emp in employees)
                    {
                        await Console.Out.WriteLineAsync($"{emp.EmpId},{emp.FirstName},{emp.LastName}");
                    }
                }
            }
        }
        public static async Task AddnewEmployee()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue ("application/json"));
                EmpViewModel employee = new EmpViewModel()
                {
                    FirstName ="William",
                    LastName = "John",
                    City = "Nyc",
                    Birthdate = new DateTime(1980,01,01),
                    HireDate = new DateTime(2000,01,01),
                    Title = "Manager"
                };
                var myContent = JsonConvert.SerializeObject(employee);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = 
                    new MediaTypeHeaderValue("application/json");
                //HttpPost:
                HttpResponseMessage response =
                    await client.PostAsync("AddNewEmployees", byteContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                }
            }
        }
        public static async Task UpdateEmployee(int id, EmpViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue ("application/json"));
                var myContent = JsonConvert.SerializeObject(employee);   //Serialzing object to JSON
                var buffer = Encoding.UTF8.GetBytes(myContent);          // Convert JSON string to byte array
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType =
                    new MediaTypeHeaderValue("application/json");
                //HttpPost:
                HttpResponseMessage response =
                    await client.PutAsync($"UpdateEmployee/{id}", byteContent);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee updated Successfully");
                }
                else
                {
                    Console.WriteLine($"Failed to update the employee. Status Code:{response.StatusCode}.Reason:{response.Content}");
                }
            }
        }
        public static async Task EditEmployee(int id, EmpViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var myContent = JsonConvert.SerializeObject(employee);   //Serialzing object to JSON
                var buffer = Encoding.UTF8.GetBytes(myContent);          // Convert JSON string to byte array
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType =
                    new MediaTypeHeaderValue("application/json");
                //HttpPost:
                HttpResponseMessage response =
                    await client.PutAsync($"EditEmployee/{id}", byteContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee edited Successfully");
                }
                else
                {
                    Console.WriteLine($"Error editing the employee. Status Code:{response.StatusCode}.Reason:{response.Content}");
                }

            }
        }
        public static async Task DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                EmpViewModel emp = new EmpViewModel();
                emp.EmpId = id;
                //HttpPost:
                HttpResponseMessage response =
                    await client.DeleteAsync($"DeleteEmployee/{id}");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee deleted Successfully");
                }
                else
                {
                    Console.WriteLine($"Error deleting the employee. Status Code:{response.StatusCode}.Reason:{response.Content}");
                }
            }
        }
    }
}
