// See https://aka.ms/new-console-template for more information
using WebAPIClientConsole;

Console.WriteLine("API Client!");
EmployeeAPIClient.CallGetAllEmployee().Wait();
Console.ReadLine();
