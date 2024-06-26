using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.CosmosDB
{
    public interface ICosmosDBInterface
    {
        Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

      
        Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id);
      

        Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<bool> DeleteEmployeeBasicDetails(string id);

        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees();

        Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<bool> DeleteEmployeeAdditionalDetails(string id);

        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id);

        Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsByUId(string uId);

        Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesAdditioanlDetails();

        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeeByRole(string role);


        Task<EmployeeBasicFilterCriteria> GetAllEmployeeByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid);

    }

}

