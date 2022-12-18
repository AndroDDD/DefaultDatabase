using Microsoft.AspNetCore.Mvc;
using DefaultDatabase.Services;
using DefaultDatabase.Models;

namespace DefaultDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _employeeService = service;
        }

        /// <summary>
        /// get all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllEmployees() {
            try {
                var employees = _employeeService.GetEmployeesList();
                if (employees == null) return NotFound();
                return Ok(employees);
            } catch (Exception) {
                return BadRequest();
            }
        }

        /// <summary>
        /// get employee details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetEmployeeById(int id){
            try {
                var employee = _employeeService.GetEmployeeDetailsById(id);
                if (employee == null) return NotFound();
                return Ok(employee);
            } catch (Exception) {
                return BadRequest();
            }
        }

        /// <summary>
        /// save employee
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveEmployee(Employee employeeModel) {
            try {
                var model = _employeeService.SaveEmployee(employeeModel);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }

        /// <summary>
        /// delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteEmployee(int id) {
            try {
                var model = _employeeService.DeleteEmployee(id);
                return Ok(model);
            } catch {
                return BadRequest();
            }
        }
    }
}