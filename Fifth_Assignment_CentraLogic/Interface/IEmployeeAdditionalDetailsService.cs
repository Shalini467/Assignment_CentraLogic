using EmployeeManagementSystemDI.DTOs;
using EmployeeManagementSystemDI.Entities;

namespace EmployeeManagementSystemDI.Interface
{
    public interface IEmployeeAdditionalDetailsService
    {

        Task<IEnumerable<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetailsAsync();
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByIdAsync(string id);
        Task AddEmployeeAdditionalDetailsAsync(EmployeeAdditionalDetailsDTO employeeAdditionalDetails);
        Task UpdateEmployeeAdditionalDetailsAsync(string id, EmployeeAdditionalDetailsDTO employeeAdditionalDetails);
        Task DeleteEmployeeAdditionalDetailsAsync(string id);

    }
}
