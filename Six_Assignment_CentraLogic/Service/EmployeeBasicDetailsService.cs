using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using HttpClientHelper = EmployeeManagementSystem.Comman.HttpClientHelper;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsInterface
    {

        public ICosmosDBInterface _cosmosService;
        public EmployeeBasicDetailsService(ICosmosDBInterface cosmosService)
        {
            _cosmosService = cosmosService;
        }

        public async Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            employeeBasicDetailsEntity.Id = Guid.NewGuid().ToString();
            employeeBasicDetailsEntity.DocumnetType = "EmployeeBasicsDetails";
            employeeBasicDetailsEntity.CreatedBy = "shalini";
            employeeBasicDetailsEntity.CreatedOn = DateTime.Now;
            employeeBasicDetailsEntity.UpdatedBy = "shalini";
            employeeBasicDetailsEntity.UpdatedOn = DateTime.Now;
            employeeBasicDetailsEntity.Version = 1;
            employeeBasicDetailsEntity.Active = true;
            employeeBasicDetailsEntity.Archived = false;

            EmployeeBasicDetailsEntity respons = await _cosmosService.AddEmployeeBasicDetails(employeeBasicDetailsEntity);
            return respons;

        }

        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees()
        {
            return await _cosmosService.GetAllEmployees();
        }

        public async Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id)
        {
            return await _cosmosService.GetEmployeeById(id);
        }
        public async Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            return await _cosmosService.UpdateEmployeeBasicDetails(id, employeeBasicDetailsEntity);
        }


        public async Task<bool> DeleteEmployeeBasicDetails(string id)
        {
            return await _cosmosService.DeleteEmployeeBasicDetails(id);
        }

        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeeByRole(string role)
        {
            return await _cosmosService.GetAllEmployeeByRole(role);
        }

        public async Task<EmployeeBasicFilterCriteria> GetAllEmployeeByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria)
        {
            return await _cosmosService.GetAllEmployeeByPagination(employeeBasicFilterCriteria);
        }

        public async Task<List<EmployeeBasicDetailsDTO>> GetBasicDetailsByMakeRequest()
        {
            var request = await HttpClientHelper.MakeGetRequest(Credentials.BasicDetailsUrl, Credentials.GetBasicDetailsEndPoint);
            return JsonConvert.DeserializeObject<List<EmployeeBasicDetailsDTO>>(request);
        }

        public async Task<EmployeeBasicDetailsDTO> AddBasicDetailsByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            //add basic details api by makePostRequest
            var serializable= JsonConvert.SerializeObject(employeeBasicDetailsDTO);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.BasicDetailsUrl, Credentials.AddBasicDetailsEndPoint, serializable);
            var model=JsonConvert.DeserializeObject<EmployeeBasicDetailsDTO>(requestObj);
            return model;
        }


       
    }
}
