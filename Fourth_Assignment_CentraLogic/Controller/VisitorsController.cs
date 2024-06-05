using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : Controller
    {

        private readonly IVisitorService _visitorService;

        public VisitorsController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }


        [HttpGet]
        public async Task<IEnumerable<Visitor>> Get()
        {
            return await _visitorService.GetAllVisitorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> Get(string id)
        {
            var visitor = await _visitorService.GetVisitorByIdAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        [HttpPost]
        public async Task<ActionResult<Visitor>> Post([FromBody] VisitorDTO visitorDto)
        {
            var visitor = new Visitor
            {
                Name = visitorDto.Name,
                Email = visitorDto.Email,
                PhoneNumber = visitorDto.PhoneNumber,
                Address = visitorDto.Address,
                Company = visitorDto.Company,
                Purpose = visitorDto.Purpose,
                EntryTime = visitorDto.EntryTime,
                ExitTime = visitorDto.ExitTime,
                Status = "Pending"
            };

            await _visitorService.CreateVisitorAsync(visitor);

            return CreatedAtAction(nameof(Get), new { id = visitor.Id }, visitor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] VisitorDTO visitorDto)
        {
            var visitor = new Visitor
            {
                Id = id,
                Name = visitorDto.Name,
                Email = visitorDto.Email,
                PhoneNumber = visitorDto.PhoneNumber,
                Address = visitorDto.Address,
                Company = visitorDto.Company,
                Purpose = visitorDto.Purpose,
                EntryTime = visitorDto.EntryTime,
                ExitTime = visitorDto.ExitTime,
                Status = "Pending"
            };

            await _visitorService.UpdateVisitorAsync(id, visitor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _visitorService.DeleteVisitorAsync(id);
            return NoContent();
        }



    }
}
