using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {

        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet]
        public async Task<IEnumerable<Security>> Get()
        {
            return await _securityService.GetAllSecuritiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Security>> Get(string id)
        {
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security == null)
            {
                return NotFound();
            }

            return security;
        }

        [HttpPost]
        public async Task<ActionResult<Security>> Post([FromBody] SecurityDTO securityDto)
        {
            var security = new Security
            {
                Name = securityDto.Name,
                Email = securityDto.Email
            };

            await _securityService.CreateSecurityAsync(security);

            return CreatedAtAction(nameof(Get), new { id = security.Id }, security);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] SecurityDTO securityDto)
        {
            var security = new Security
            {
                Id = id,
                Name = securityDto.Name,
                Email = securityDto.Email
            };

            await _securityService.UpdateSecurityAsync(id, security);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _securityService.DeleteSecurityAsync(id);
            return NoContent();
        
        }

    }
}
