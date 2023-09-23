using FirstWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly RepositoryEmployee _repositoryEmployee;
        public EmployeeController(RepositoryEmployee repository)
        {
            _repositoryEmployee = repository;
        }
        // GET: api/Employee
        [HttpGet]
        public IEnumerable<EmpViewModel> GetEmployees()
        {
            List<Employee> employees = _repositoryEmployee.GetAllEmployees();
         //  return Ok(employees); // Return employees as JSON
         var empList=(
                from emp in employees
                select new EmpViewModel()
                {
                    EmpId = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Birthdate = (DateTime)emp.BirthDate,
                    HireDate = (DateTime)emp.HireDate,
                    Title = emp.Title,
                    City= emp.City,
                    ReportsTo= (int)emp.ReportsTo,
                }
                ).ToList();
            return empList;
        }
        // GET: api/Employee/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            Employee employee = _repositoryEmployee.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound(); // Return a 404 Not Found response if the employee is not found
            }
            return Ok(employee); // Return employee as JSON
        }
        // POST: api/Employee
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest(); // Return a 400 Bad Request response if the request body is empty
            }
            _repositoryEmployee.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee); // Return the newly created employee as JSON
        }
        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest(); // Return a 400 Bad Request response if the request body is empty or the IDs do not match
            }
            _repositoryEmployee.UpdateEmployee(employee);
            return NoContent(); // Return a 204 No Content response
        }
        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = _repositoryEmployee.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound(); // Return a 404 Not Found response if the employee is not found
            }
            _repositoryEmployee.DeleteEmployee(id);
            return NoContent(); // Return a 204 No Content response
        }
    }
}
