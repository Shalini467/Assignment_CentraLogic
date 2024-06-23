using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeAdditionalDetailsInterface
    {
        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id);

        Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesAdditioanlDetails();

        Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);


        Task<bool> DeleteEmployeeAdditionalDetails(string id);

       


    }
}
