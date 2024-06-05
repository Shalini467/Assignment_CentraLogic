using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public class SecurityCosmosDBService : ISecurityCosmosDBService
    {

        public CosmosClient _cosmosClient;

        private readonly Container _container;

        public SecurityCosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);

        }

        public async Task AddSecurityAsync(Security security)
        {
            await this._container.CreateItemAsync<Security>(security, new PartitionKey(security.Id));
        }

        public async Task DeleteSecurityAsync(string id)
        {
            await this._container.DeleteItemAsync<Security>(id, new PartitionKey(id));
        }

        public async Task<Security> GetSecurityAsync(string id)
        {
            try
            {
                ItemResponse<Security> response = await this._container.ReadItemAsync<Security>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Security>> GetSecuritiesAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Security>(new QueryDefinition(queryString));
            List<Security> results = new List<Security>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateSecurityAsync(string id, Security security)
        {
            await this._container.UpsertItemAsync<Security>(security, new PartitionKey(id));
        }
    }
}
