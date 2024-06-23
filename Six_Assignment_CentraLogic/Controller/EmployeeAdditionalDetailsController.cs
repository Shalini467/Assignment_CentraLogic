using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeAdditionalDetailsController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeAdditionalDetailsController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _employeeService.GetAllEmployeeAdditionalDetailsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _employeeService.GetEmployeeAdditionalDetailsByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            await _employeeService.AddEmployeeAdditionalDetailsAsync(employeeAdditionalDetailsDTO);
            return CreatedAtAction(nameof(Get), new { id = employeeAdditionalDetailsDTO.Id }, employeeAdditionalDetailsDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            await _employeeService.UpdateEmployeeAdditionalDetailsAsync(id, employeeAdditionalDetailsDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeService.DeleteEmployeeAdditionalDetailsAsync(id);
            return NoContent();
        }
    }
}
