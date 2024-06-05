using VisitorSecurityClearanceSystem.Model;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public interface ISecurityCosmosDBService
    {
        Task<IEnumerable<Security>> GetSecuritiesAsync(string query);
        Task<Security> GetSecurityAsync(string id);
        Task AddSecurityAsync(Security security);
        Task UpdateSecurityAsync(string id, Security security);
        Task DeleteSecurityAsync(string id);
    }
}
