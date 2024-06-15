using EmployeeManagementSystemDI.DTOs;
using EmployeeManagementSystemDI.Entities;
using EmployeeManagementSystemDI.Interface;

namespace EmployeeManagementSystemDI.Services
{
    public class EmployeeAdditionalDetailsService : IEmployeeAdditionalDetailsService
    {

        private readonly ICosmosDbService<EmployeeAdditionalDetails> _cosmosDbService;

        public EmployeeAdditionalDetailsService(ICosmosDbService<EmployeeAdditionalDetails> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetailsAsync()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        }

        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByIdAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddEmployeeAdditionalDetailsAsync(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            var employeeAdditionalDetails = new EmployeeAdditionalDetails
            {
                Id = employeeAdditionalDetailsDTO.Id,
                EmployeeBasicDetailsUId = employeeAdditionalDetailsDTO.EmployeeBasicDetailsUId,
                AlternateEmail = employeeAdditionalDetailsDTO.AlternateEmail,
                AlternateMobile = employeeAdditionalDetailsDTO.AlternateMobile,
                WorkInformation = employeeAdditionalDetailsDTO.WorkInformation,
                PersonalDetails = employeeAdditionalDetailsDTO.PersonalDetails,
                IdentityInformation = employeeAdditionalDetailsDTO.IdentityInformation
            };
            await _cosmosDbService.AddItemAsync(employeeAdditionalDetails);
        }

        public async Task UpdateEmployeeAdditionalDetailsAsync(string id, EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            var employeeAdditionalDetails = new EmployeeAdditionalDetails
            {
                Id = employeeAdditionalDetailsDTO.Id,
                EmployeeBasicDetailsUId = employeeAdditionalDetailsDTO.EmployeeBasicDetailsUId,
                AlternateEmail = employeeAdditionalDetailsDTO.AlternateEmail,
                AlternateMobile = employeeAdditionalDetailsDTO.AlternateMobile,
                WorkInformation = employeeAdditionalDetailsDTO.WorkInformation,
                PersonalDetails = employeeAdditionalDetailsDTO.PersonalDetails,
                IdentityInformation = employeeAdditionalDetailsDTO.IdentityInformation
            };
            await _cosmosDbService.UpdateItemAsync(id, employeeAdditionalDetails);
        }

        public async Task DeleteEmployeeAdditionalDetailsAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }

    }
}
