using Microsoft.Azure.Cosmos;
using VisitorSecurityClearanceSystem.Common;

using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public class VisitorCosmosDBService : IVisitorCosmosDBService
    {
        public CosmosClient _cosmosClient;

        private readonly Container _container;

        public VisitorCosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);

        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await this._container.CreateItemAsync<Visitor>(visitor, new PartitionKey(visitor.Id));
        }

        public async Task DeleteVisitorAsync(string id)
        {
            await this._container.DeleteItemAsync<Visitor>(id, new PartitionKey(id));
        }

        public async Task<Visitor> GetVisitorAsync(string id)
        {
            try
            {
                ItemResponse<Visitor> response = await this._container.ReadItemAsync<Visitor>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Visitor>> GetVisitorsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Visitor>(new QueryDefinition(queryString));
            List<Visitor> results = new List<Visitor>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateVisitorAsync(string id, Visitor visitor)
        {
            await this._container.UpsertItemAsync<Visitor>(visitor, new PartitionKey(id));
        }

    }
}
