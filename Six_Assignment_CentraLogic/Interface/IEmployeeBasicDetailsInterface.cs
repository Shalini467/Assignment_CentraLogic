using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeBasicDetailsInterface
    {
        Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity);
        Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity);
        Task<bool> DeleteEmployeeBasicDetails(string id);
        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees();
        Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id);
        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeeByRole(string role);
        Task<EmployeeBasicFilterCriteria> GetAllEmployeeByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria);
        Task<EmployeeBasicDetailsDTO> AddBasicDetailsByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO);
        Task<List<EmployeeBasicDetailsDTO>> GetBasicDetailsByMakeRequest();
    }
}
