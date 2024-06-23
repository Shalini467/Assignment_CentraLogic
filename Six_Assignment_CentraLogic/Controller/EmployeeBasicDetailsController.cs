using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeBasicDetailsController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeBasicDetailsController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _employeeService.GetAllEmployeeBasicDetailsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _employeeService.GetEmployeeBasicDetailsByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            await _employeeService.AddEmployeeBasicDetailsAsync(employeeBasicDetailsDTO);
            return CreatedAtAction(nameof(Get), new { id = employeeBasicDetailsDTO.Id }, employeeBasicDetailsDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            await _employeeService.UpdateEmployeeBasicDetailsAsync(id, employeeBasicDetailsDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeService.DeleteEmployeeBasicDetailsAsync(id);
            return NoContent();
        }

    }
}
