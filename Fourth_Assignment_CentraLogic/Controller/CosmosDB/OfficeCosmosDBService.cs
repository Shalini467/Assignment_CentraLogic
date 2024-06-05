using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeCosmosDBService : IOfficeCosmosDBService
    {

        public CosmosClient _cosmosClient;

        private readonly Container _container;

        public OfficeCosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);

        }


        public async Task AddOfficeAsync(Office office)
        {
            await this._container.CreateItemAsync<Office>(office, new PartitionKey(office.Id));
        }

        public async Task DeleteOfficeAsync(string id)
        {
            await this._container.DeleteItemAsync<Office>(id, new PartitionKey(id));
        }

        public async Task<Office> GetOfficeAsync(string id)
        {
            try
            {
                ItemResponse<Office> response = await this._container.ReadItemAsync<Office>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Office>> GetOfficesAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Office>(new QueryDefinition(queryString));
            List<Office> results = new List<Office>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateOfficeAsync(string id, Office office)
        {
            await this._container.UpsertItemAsync<Office>(office, new PartitionKey(id));
        }

       
    }
}
