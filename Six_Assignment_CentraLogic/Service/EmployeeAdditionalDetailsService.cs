using AutoMapper;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeAdditionalDetailsService:IEmployeeAdditionalDetailsInterface
    {

        public ICosmosDBInterface _cosmosService;
        public readonly IMapper _mapper;
        public EmployeeAdditionalDetailsService(ICosmosDBInterface cosmosService,IMapper mapper)
        {
            _cosmosService = cosmosService;
            _mapper = mapper;
        }

        public async Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {
            employeeAdditionalDetailsEntity.Id = Guid.NewGuid().ToString();
            employeeAdditionalDetailsEntity.UId = Guid.NewGuid().ToString();

            employeeAdditionalDetailsEntity.DocumnetType = "EmployeeAdditionalDetails";
            employeeAdditionalDetailsEntity.CreatedBy = "shalini";
            employeeAdditionalDetailsEntity.CreatedOn = DateTime.Now;
            employeeAdditionalDetailsEntity.UpdatedBy = "shalini";
            employeeAdditionalDetailsEntity.UpdatedOn = DateTime.Now;
            employeeAdditionalDetailsEntity.Version = 1;
            employeeAdditionalDetailsEntity.Active = true;
            employeeAdditionalDetailsEntity.Archived = false;

            EmployeeAdditionalDetailsEntity respons = await _cosmosService.AddEmployeeAdditionalDetails(employeeAdditionalDetailsEntity);
            return respons;
        }

        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id)
        {
            return await _cosmosService.GetEmployeeAdditionalDetailsById(id);
        }

        
        
        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsByUId(string uId)
        {
            var response = await _cosmosService.GetAdditionalDetailsByBasicDetailsUId(uId);

            
            if(response != null)
            {
                var employeeAdditionalDetailsEntity = new EmployeeAdditionalDetailsEntity();

                employeeAdditionalDetailsEntity.UId = response.UId;
                employeeAdditionalDetailsEntity.AlternateEmail = response.AlternateEmail;
                employeeAdditionalDetailsEntity.AlternateMobile = response.AlternateMobile;
                employeeAdditionalDetailsEntity.WorkInformation = response.WorkInformation;
                employeeAdditionalDetailsEntity.PersonalDetails = response.PersonalDetails;
                employeeAdditionalDetailsEntity.IdentityInformation = response.IdentityInformation;

                return employeeAdditionalDetailsEntity;
            }
            else
            {
                throw new Exception("Response object is null.");
            }

       
        
        }

        


        public async Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesAdditioanlDetails()
        {
            return await _cosmosService.GetAllEmployeesAdditioanlDetails();
        }

        public async Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {
            return await _cosmosService.UpdateEmployeeAdditionalDetails(id, employeeAdditionalDetailsEntity);
        }

        public async Task<bool> DeleteEmployeeAdditionalDetails(string id)
        {
            return await _cosmosService.DeleteEmployeeAdditionalDetails(id);
        }

        public async Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid)
        {
            return await _cosmosService.GetAdditionalDetailsByBasicDetailsUId(employeeBasicDetailsUid);
        }

    }
}
