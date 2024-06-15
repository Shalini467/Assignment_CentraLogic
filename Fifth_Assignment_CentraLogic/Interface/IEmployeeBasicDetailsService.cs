using EmployeeManagementSystemDI.DTOs;
using EmployeeManagementSystemDI.Entities;

namespace EmployeeManagementSystemDI.Interface
{
    public interface IEmployeeBasicDetailsService
    {

        Task<IEnumerable<EmployeeBasicDetails>> GetAllAsync();
        Task<EmployeeBasicDetails> GetByIdAsync(string id);
        Task AddAsync(EmployeeBasicDetailsDTO employeeBasicDetails);
        Task UpdateAsync(string id, EmployeeBasicDetailsDTO employeeBasicDetails);
        Task DeleteAsync(string id);
    }
}
