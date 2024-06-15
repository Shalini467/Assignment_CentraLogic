using EmployeeManagementSystemDI.DTOs;
using EmployeeManagementSystemDI.Entities;
using EmployeeManagementSystemDI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBasicDetailsController : Controller
    {

        private readonly IEmployeeBasicDetailsService _employeeBasicDetailsService;

        public EmployeeBasicDetailsController(IEmployeeBasicDetailsService employeeBasicDetailsService)
        {
            _employeeBasicDetailsService = employeeBasicDetailsService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeBasicDetails>> Get()
        {
            return await _employeeBasicDetailsService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBasicDetails>> Get(string id)
        {
            var employee = await _employeeBasicDetailsService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            employeeBasicDetails.Id = Guid.NewGuid().ToString();
            await _employeeBasicDetailsService.AddAsync(employeeBasicDetails);
            return CreatedAtAction(nameof(Get), new { id = employeeBasicDetails.Id }, employeeBasicDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EmployeeBasicDetailsDTO employeeBasicDetails)
        {
            await _employeeBasicDetailsService.UpdateAsync(id, employeeBasicDetails);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeBasicDetailsService.DeleteAsync(id);
            return NoContent();
        }


    }
}
