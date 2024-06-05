using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystem.Common;

using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public class ManagerCosmosDBService : IManagerCosmosDBService
    {

        public readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        public ManagerCosmosDBService()

        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }


        public async Task AddManagerAsync(Manager manager)
        {
            await this._container.CreateItemAsync<Manager>(manager, new PartitionKey(manager.Id));
        }

        public async Task DeleteManagerAsync(string id)
        {
            await this._container.DeleteItemAsync<Manager>(id, new PartitionKey(id));
        }

        public async Task<Manager> GetManagerAsync(string id)
        {
            try
            {
                ItemResponse<Manager> response = await this._container.ReadItemAsync<Manager>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Manager>> GetManagersAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Manager>(new QueryDefinition(queryString));
            List<Manager> results = new List<Manager>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateManagerAsync(string id, Manager manager)
        {
            await this._container.UpsertItemAsync<Manager>(manager, new PartitionKey(id));
        }


    }
}
