using EmployeeManagementSystemDI.DTOs;
using EmployeeManagementSystemDI.Entities;
using EmployeeManagementSystemDI.Interface;

namespace EmployeeManagementSystemDI.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {

        private readonly ICosmosDbService<EmployeeBasicDetails> _cosmosDbService;

        public EmployeeBasicDetailsService(ICosmosDbService<EmployeeBasicDetails> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<EmployeeBasicDetails>> GetAllAsync()
        {
            return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
        }

        public async Task<EmployeeBasicDetails> GetByIdAsync(string id)
        {
            return await _cosmosDbService.GetItemAsync(id);
        }

        public async Task AddAsync(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var employeeBasicDetails = new EmployeeBasicDetails
            {
                
                Id = employeeBasicDetailsDTO.Id,
                Salutory = employeeBasicDetailsDTO.Salutory,
                FirstName = employeeBasicDetailsDTO.FirstName,
                MiddleName = employeeBasicDetailsDTO.MiddleName,
                LastName = employeeBasicDetailsDTO.LastName,
                NickName = employeeBasicDetailsDTO.NickName,
                Email = employeeBasicDetailsDTO.Email,
                Mobile = employeeBasicDetailsDTO.Mobile,
                EmployeeID = employeeBasicDetailsDTO.EmployeeID,
                Role = employeeBasicDetailsDTO.Role,
                ReportingManagerUId = employeeBasicDetailsDTO.ReportingManagerUId,
                ReportingManagerName = employeeBasicDetailsDTO.ReportingManagerName,
                Address = employeeBasicDetailsDTO.Address
            };
            await _cosmosDbService.AddItemAsync(employeeBasicDetails);
        }

        public async Task UpdateAsync(string id, EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var employeeBasicDetails = new EmployeeBasicDetails
            {
                Id = employeeBasicDetailsDTO.Id,
                Salutory = employeeBasicDetailsDTO.Salutory,
                FirstName = employeeBasicDetailsDTO.FirstName,
                MiddleName = employeeBasicDetailsDTO.MiddleName,
                LastName = employeeBasicDetailsDTO.LastName,
                NickName = employeeBasicDetailsDTO.NickName,
                Email = employeeBasicDetailsDTO.Email,
                Mobile = employeeBasicDetailsDTO.Mobile,
                EmployeeID = employeeBasicDetailsDTO.EmployeeID,
                Role = employeeBasicDetailsDTO.Role,
                ReportingManagerUId = employeeBasicDetailsDTO.ReportingManagerUId,
                ReportingManagerName = employeeBasicDetailsDTO.ReportingManagerName,
                Address = employeeBasicDetailsDTO.Address
            };
            await _cosmosDbService.UpdateItemAsync(id, employeeBasicDetails);
        }

        public async Task DeleteAsync(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
        }

    }
}
