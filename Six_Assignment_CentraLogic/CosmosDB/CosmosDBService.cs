using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Data;

namespace EmployeeManagementSystem.CosmosDB
{
    public class CosmosDBService : ICosmosDBInterface
    {
        public readonly Container container;

        public CosmosDBService()
        {
            container = GetContainer();
        }

        private Container GetContainer()
        {
            string databaseName = Credentials.DatabaseName;
            string containerName = Credentials.ContainerName;
            string cosmosEndPointURI = Credentials.CosmosEndPointURI;
            string primaryKey = Credentials.PrimaryKey;
            CosmosClient cosmosClient = new CosmosClient(cosmosEndPointURI, primaryKey);
            return cosmosClient.GetContainer(databaseName, containerName);
        }


        public async Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            employeeBasicDetailsEntity.Id = Guid.NewGuid().ToString();

            var response = await container.CreateItemAsync(employeeBasicDetailsEntity);
            return response.Resource;
        }


        public async Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id)
        { 
           var employee=container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true)
                                  .Where(x => x.Id == id).FirstOrDefault();
            return employee;
            
        }



        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees()
        {
            var activeEmployees = container.GetItemLinqQueryable<EmployeeBasicDetailsEntity>(true)
                                            .Where(e => e.Active)
                                            .ToList();

            return await Task.FromResult(activeEmployees);
        }

        public async Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity)
        {
            
                var response = await container.ReplaceItemAsync(employeeBasicDetailsEntity, id, new PartitionKey(id));
                return response.Resource;
            
        }

        public async Task<bool> DeleteEmployeeBasicDetails(string id)
        {
           
                var response = await container.DeleteItemAsync<EmployeeBasicDetailsEntity>(id, new PartitionKey(id));
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            
        }


        public async Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {
            employeeAdditionalDetailsEntity.Id = Guid.NewGuid().ToString();
            employeeAdditionalDetailsEntity.UId = Guid.NewGuid().ToString();
            var response = await container.CreateItemAsync(employeeAdditionalDetailsEntity);
            return response.Resource;
        }

        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsById(string id)
        {
            var employee = container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true)
                                   .Where(x => x.Id == id).FirstOrDefault();
            return employee;

        }

        
      
        public async Task<EmployeeAdditionalDetailsEntity> GetEmployeeAdditionalDetailsByUId(string uId)
        {
            var employee = container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(a => a.UId == uId && !a.Archived && a.Active && a.DocumnetType == Credentials.EmployeeDocumentType).FirstOrDefault();
            return employee;
        }
        
        

       public async Task<List<EmployeeAdditionalDetailsEntity>>GetAllEmployeesAdditioanlDetails()
        {
            var activeEmployees = container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true)
                                            .Where(e => e.Active).ToList();

            return await Task.FromResult(activeEmployees);
        }

        public async Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {

            var response = await container.ReplaceItemAsync(employeeAdditionalDetailsEntity, id, new PartitionKey(id));
            return response.Resource;

        }

        public async Task<bool> DeleteEmployeeAdditionalDetails(string id)
        {

            var response = await container.DeleteItemAsync<EmployeeAdditionalDetailsEntity>(id, new PartitionKey(id));
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;

        }

        public async Task<List<EmployeeBasicDetailsEntity>> GetAllEmployeeByRole(string role)
        {
            var allEmployees = await GetAllEmployees();

            var filteredList = allEmployees.FindAll(a => a.Role == role);
            return filteredList;

        }


        public async Task<EmployeeBasicFilterCriteria> GetAllEmployeeByPagination(EmployeeBasicFilterCriteria employeeBasicFilterCriteria)
        {
            EmployeeBasicFilterCriteria responseObject = new EmployeeBasicFilterCriteria();
            //filter ==> status
            var checkFilter = employeeBasicFilterCriteria.Filters.Any(a => a.FieldName == "status");
            var status = "";
            if (checkFilter)
            {
                status = employeeBasicFilterCriteria.Filters.Find(a => a.FieldName == "status").FieldValue;

            }

            var employees = await GetAllEmployees();
            var filteredRecords = employees.FindAll(a => a.Role == status);
            responseObject.TotalCount = employees.Count;
            responseObject.Page = employeeBasicFilterCriteria.Page;
            responseObject.PageSize = employeeBasicFilterCriteria.PageSize;

            var skip = employeeBasicFilterCriteria.PageSize * (employeeBasicFilterCriteria.Page - 1);

            filteredRecords = filteredRecords.Skip(skip).Take(employeeBasicFilterCriteria.PageSize).ToList();
            foreach (var item in filteredRecords)
            {
                responseObject.Employees.Add(item);

            }

            return responseObject;
        }
        public async Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid)
        {
            var employee = container.GetItemLinqQueryable<EmployeeAdditionalDetailsEntity>(true).Where(a => a.EmployeeBasicDetailsUId == employeeBasicDetailsUid).FirstOrDefault();
            return employee;
        }



    }
}
