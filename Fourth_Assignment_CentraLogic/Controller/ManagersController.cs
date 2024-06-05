using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagersController : Controller
    {

        private readonly IManagerService _managerService;

        public ManagersController(IManagerService ManagerService)
        {
            _managerService = ManagerService;
        }


        [HttpGet]
        public async Task<IEnumerable<Manager>> Get()
        {
            return await _managerService.GetAllManagersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> Get(string id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            return manager;
        }

        [HttpPost]
        public async Task<ActionResult<Manager>> Post([FromBody] ManagerDTO managerDto)
        {
            var manager = new Manager
            {
                Name = managerDto.Name,
                Email = managerDto.Email
            };

            await _managerService.CreateManagerAsync(manager);

            return CreatedAtAction(nameof(Get), new { id = manager.Id }, manager);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ManagerDTO managerDto)
        {
            var manager = new Manager
            {
                Id = id,
                Name = managerDto.Name,
                Email = managerDto.Email
            };

            await _managerService.UpdateManagerAsync(id, manager);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _managerService.DeleteManagerAsync(id);
            return NoContent();
        }

    }
}
