using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : ControllerBase
    {
        public IEmployeeAdditionalDetailsInterface employeeAdditionalDetailsInterface;
        private readonly IMapper _mapper;



        public EmployeeAdditionalDetailsController(IEmployeeAdditionalDetailsInterface employeeAdditionalDetails,IMapper mapper)
        {

            employeeAdditionalDetailsInterface = employeeAdditionalDetails;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAdditionalDetails(EmployeeAdditionDetailsDTO employeeDTO)
        {
            var employeeEntity = _mapper.Map<EmployeeAdditionalDetailsEntity>(employeeDTO);

            var response = await employeeAdditionalDetailsInterface.AddEmployeeAdditionalDetails(employeeEntity);

            var resultDTO = _mapper.Map<EmployeeAdditionDetailsDTO>(response);
            return Ok(resultDTO);
        }



        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesAdditioanlDetails()
        {
            var employees = await employeeAdditionalDetailsInterface.GetAllEmployeesAdditioanlDetails();
            var resultDTOs = _mapper.Map<IEnumerable<EmployeeAdditionDetailsDTO>>(employees);
            return Ok(resultDTOs);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAdditionalDetailsById(string id)
        {
            var employee = await employeeAdditionalDetailsInterface.GetEmployeeAdditionalDetailsById(id);

            var resultDTO = _mapper.Map<EmployeeAdditionDetailsDTO>(employee);
            return Ok(resultDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAdditionalDetails(string id, [FromBody] EmployeeAdditionalDetailsEntity employee)
        {
            var updatedEmployee = await employeeAdditionalDetailsInterface.UpdateEmployeeAdditionalDetails(id, employee);
            return Ok(updatedEmployee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAdditionalDetails(string id)
        {
            var isDeleted = await employeeAdditionalDetailsInterface.DeleteEmployeeAdditionalDetails(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
