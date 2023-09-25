// See https://aka.ms/new-console-template for more information
using WebAPIClientConsole;

Console.WriteLine("API Client!");
EmployeeAPIClient.CallGetAllEmployee().Wait();
Console.ReadLine();
EmployeeAPIClient.AddnewEmployee().Wait();
Console.WriteLine();
EmployeeAPIClient.DeleteEmployee(10).Wait();
Console.WriteLine();

EmpViewModel empToUpdate = new EmpViewModel()
{
    EmpId = 10,
    FirstName = "Update",
    LastName = "Update",
    City = "Nyc",
    Birthdate = new DateTime(1980, 01, 01),
    HireDate = new DateTime(2000, 01, 01),
    Title = "Manager"

};

EmployeeAPIClient.UpdateEmployee(10, empToUpdate).Wait();
