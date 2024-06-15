using EmployeeManagementSystemDI.DTOs;
using EmployeeManagementSystemDI.Entities;
using EmployeeManagementSystemDI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemDI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : Controller
    {

        private readonly IEmployeeAdditionalDetailsService _employeeAdditionalDetailsService;

        public EmployeeAdditionalDetailsController(IEmployeeAdditionalDetailsService employeeAdditionalDetailsService)
        {
            _employeeAdditionalDetailsService = employeeAdditionalDetailsService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeAdditionalDetails>> Get()
        {
            return await _employeeAdditionalDetailsService.GetAllEmployeeAdditionalDetailsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAdditionalDetails>> Get(string id)
        {
            var employee = await _employeeAdditionalDetailsService.GetEmployeeAdditionalDetailsByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
        {

            employeeAdditionalDetails.Id = Guid.NewGuid().ToString();
            await _employeeAdditionalDetailsService.AddEmployeeAdditionalDetailsAsync(employeeAdditionalDetails);
            return CreatedAtAction(nameof(Get), new { id = employeeAdditionalDetails.Id }, employeeAdditionalDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
        {
            await _employeeAdditionalDetailsService.UpdateEmployeeAdditionalDetailsAsync(id, employeeAdditionalDetails);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeAdditionalDetailsService.DeleteEmployeeAdditionalDetailsAsync(id);
            return NoContent();
        }

    }
}
