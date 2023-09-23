namespace WebAPIClientConsole
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Title {  get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public DateTime HireDate { get; set; }
        public String City { get; set; }= String.Empty;
        public int ReportsTo { get; set; }
    }
}
