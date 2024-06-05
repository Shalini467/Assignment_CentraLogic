using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : Controller
    {
        private Container _container;

        private readonly IOfficeService _officeService;

        public OfficesController(IOfficeService OfficeService)
        {
            _officeService = OfficeService;
        }


        [HttpGet]
        public async Task<IEnumerable<Office>> Get()
        {
            return await _officeService.GetAllOfficesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Office>> Get(string id)
        {
            var office = await _officeService.GetOfficeByIdAsync(id);
            if (office == null)
            {
                return NotFound();
            }

            return office;
        }

        [HttpPost]
        public async Task<ActionResult<Office>> Post([FromBody] OfficeDTO officeDto)
        {
            var office = new Office
            {
                Name = officeDto.Name,
                Email = officeDto.Email
            };

            await _officeService.CreateOfficeAsync(office);

            return CreatedAtAction(nameof(Get), new { id = office.Id }, office);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] OfficeDTO officeDto)
        {
            var office = new Office
            {
                Id = id,
                Name = officeDto.Name,
                Email = officeDto.Email
            };

            await _officeService.UpdateOfficeAsync(id, office);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _officeService.DeleteOfficeAsync(id);
            return NoContent();
        }


    }
}
